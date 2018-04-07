using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMap : IGameMap
{
  private readonly BoundsInt _bounds;
  private readonly IPlayerCollisions _playerCollisions;
  private readonly IPlayerSpriteRenderer _playerSpriteRenderer;

  public GameMap(IPlayerCollisions playerCollisions, IPlayerSpriteRenderer playerSpriteRenderer)
  {
    _playerCollisions = playerCollisions;
    _playerSpriteRenderer = playerSpriteRenderer;
    _bounds = Get();
  }

  public bool MoveIsValid(Player player, Vector3 pos)
  {
    // clicking outside map
    if (pos.x < 0f || pos.x > (_bounds.size.x - 3) || pos.y < 0f || pos.y > (_bounds.size.y - 3))
    {
      return false;
    }

    var possiblePositions = GetPossibleMovePositions(player);
    foreach (var possiblePosition in possiblePositions)
    {
      if (possiblePosition == pos && _playerCollisions.CanMoveToSpace(pos))
      {
        return true;
      }
    }

    return false;
  }

  public BoundsInt Get()
  {
    var tilemaps = GameObject.Find("Floor").GetComponentsInChildren<Tilemap>();
    foreach (var tilemap in tilemaps)
    {
      if (tilemap.name == "Walls")
      {
        return tilemap.cellBounds;
      }
    }

    return new BoundsInt();
  }

  public void CheckAndHideGameEntities(Player player)
  {
    var game = GameObject.Find("Game");
    var children = game.GetComponentsInChildren<GameEntity>();
    var visionPositions = GetVisionPositions(player);
    var refineryVisions = GetAllRefineryVisionPositions(player);

    foreach (var child in children)
    {
      var refinery = child.GetComponent<Refinery>();
      if (refinery == null || refinery.ownedBy != player.gameObject)
      {
        child.GetComponentInChildren<Renderer>().enabled = false;
      }
    }

    foreach (var visionPosition in visionPositions)
    {
      if (_playerCollisions.IsGameEntity(visionPosition))
      {
        var gameEntity = _playerCollisions.GetGameEntity(visionPosition);
        gameEntity.GetComponentInChildren<Renderer>().enabled = true;
      }
    }

    foreach (var refineryVision in refineryVisions)
    {
      if (_playerCollisions.IsGameEntity(refineryVision))
      {
        var gameEntity = _playerCollisions.GetGameEntity(refineryVision);
        gameEntity.GetComponentInChildren<Renderer>().enabled = true;
      }
    }
  }

  public List<Vector3> GetBuildPositions(Player player)
  {
    var buildPositions = new List<Vector3>();

    // Reveal square vision
    for (int i = -(player.buildRange * 2 - player.buildRange); i < (player.buildRange + 1); i++)
    {
      for (int j = -(player.buildRange * 2 - player.buildRange); j < (player.buildRange + 1); j++)
      {
        var buildPosition = new Vector3(
          player.transform.position.x + j,
          player.transform.position.y + i,
          player.transform.position.z
        );

        if (_playerCollisions.CanMoveToSpace(buildPosition) && player.transform.position != buildPosition)
        {
          buildPositions.Add(buildPosition);
        }
      }
    }

    return buildPositions;
  }

  public List<Vector3> GetAllRefineryVisionPositions(Player player)
  {
    var visionPositions = new List<Vector3>();
    foreach (var refinery in player.refineries)
    {
      var refineryComp = refinery.GetComponent<Refinery>();
      // Reveal square vision
      for (int i = -(refineryComp.visionRadius * 2 - refineryComp.visionRadius); i < (refineryComp.visionRadius + 1); i++)
      {
        for (int j = -(refineryComp.visionRadius * 2 - refineryComp.visionRadius); j < (refineryComp.visionRadius + 1); j++)
        {
          visionPositions.Add(new Vector3(
            refineryComp.transform.position.x + j,
            refineryComp.transform.position.y + i,
            refineryComp.transform.position.z
          ));
        }
      }
    }

    return visionPositions;
  }

  public List<Vector3> GetVisionPositions(Player player)
  {
    var visionPositions = new List<Vector3>();

    // Reveal cross vision
    for (int i = 1; i < player.visionRadius + 2; i++)
    {
      // North
      visionPositions.Add(new Vector3(
        player.transform.position.x,
        player.transform.position.y + i,
        player.transform.position.z
      ));

      // South
      visionPositions.Add(new Vector3(
        player.transform.position.x,
        player.transform.position.y - i,
        player.transform.position.z
      ));

      // West
      visionPositions.Add(new Vector3(
        player.transform.position.x - i,
        player.transform.position.y,
        player.transform.position.z
      ));

      // East
      visionPositions.Add(new Vector3(
        player.transform.position.x + i,
        player.transform.position.y,
        player.transform.position.z
      ));
    }

    // Reveal square vision
    for (int i = -(player.visionRadius * 2 - player.visionRadius); i < (player.visionRadius + 1); i++)
    {
      for (int j = -(player.visionRadius * 2 - player.visionRadius); j < (player.visionRadius + 1); j++)
      {
        visionPositions.Add(new Vector3(
          player.transform.position.x + j,
          player.transform.position.y + i,
          player.transform.position.z
        ));
      }
    }

    return visionPositions;
  }

  public List<Vector3> GetValidSideAttackPositions(Player player, Vector3 mousePos)
  {
    var sidePositions = new List<Vector3>();
    Vector3 sideLeft;
    Vector3 sideRight;

    for (int i = 1; i < player.sideHitRange + 1; i++)
    {
      // West
      if (player.transform.position.x - mousePos.x > 0)
      {
        sideLeft = new Vector3(
          mousePos.x - i,
          mousePos.y - i,
          mousePos.z
        );
        sideRight = new Vector3(
          mousePos.x - i,
          mousePos.y + i,
          mousePos.z
        );

      }
      // East
      else if (player.transform.position.x - mousePos.x < 0)
      {
        sideLeft = new Vector3(
          mousePos.x + i,
          mousePos.y - i,
          mousePos.z
        );
        sideRight = new Vector3(
          mousePos.x + i,
          mousePos.y + i,
          mousePos.z
        );
      }
      // North
      else if (player.transform.position.y - mousePos.y > 0)
      {
        sideLeft = new Vector3(
          mousePos.x + i,
          mousePos.y - i,
          mousePos.z
        );
        sideRight = new Vector3(
          mousePos.x - i,
          mousePos.y - i,
          mousePos.z
        );
      }
      // South
      else
      {
        sideLeft = new Vector3(
          mousePos.x + i,
          mousePos.y + i,
          mousePos.z
        );
        sideRight = new Vector3(
          mousePos.x - i,
          mousePos.y + i,
          mousePos.z
        );
      }

      sidePositions.Add(sideLeft);
      sidePositions.Add(sideRight);
    }

    return sidePositions;
  }

  public List<Vector3> GetValidMovePositions(Player player)
  {
    var validPositions = new List<Vector3>();
    var westBlocked = false;
    var eastBlocked = false;
    var northBlocked = false;
    var southBlocked = false;
    var northWestBlocked = false;
    var northEastBlocked = false;
    var southWestBlocked = false;
    var southEastBlocked = false;

    for (int i = 1; i < player.numberOfMoveSpacesPerTurn + 1; i++)
    {
      var west = new Vector3(
        player.transform.position.x - i,
        player.transform.position.y,
        player.transform.position.z
      );
      var east = new Vector3(
        player.transform.position.x + i,
        player.transform.position.y,
        player.transform.position.z
      );
      var north = new Vector3(
        player.transform.position.x,
        player.transform.position.y - i,
        player.transform.position.z
      );
      var south = new Vector3(
        player.transform.position.x,
        player.transform.position.y + i,
        player.transform.position.z
      );

      if (!westBlocked)
      {
        if (_playerCollisions.CanMoveToSpace(west))
        {
          validPositions.Add(west);
        }
        else
        {
          westBlocked = true;
        }
      }

      if (!eastBlocked)
      {
        if (_playerCollisions.CanMoveToSpace(east))
        {
          validPositions.Add(east);
        }
        else
        {
          eastBlocked = true;
        }
      }

      if (!northBlocked)
      {
        if (_playerCollisions.CanMoveToSpace(north))
        {
          validPositions.Add(north);
        }
        else
        {
          northBlocked = true;
        }
      }

      if (!southBlocked)
      {
        if (_playerCollisions.CanMoveToSpace(south))
        {
          validPositions.Add(south);
        }
        else
        {
          southBlocked = true;
        }
      }

      if (player.canMoveAcross)
      {
        var northWest = new Vector3(
          player.transform.position.x - i,
          player.transform.position.y + i,
          player.transform.position.z
        );

        var northEast = new Vector3(
          player.transform.position.x + i,
          player.transform.position.y + i,
          player.transform.position.z
        );

        var southWest = new Vector3(
          player.transform.position.x - i,
          player.transform.position.y - i,
          player.transform.position.z
        );

        var southEast = new Vector3(
          player.transform.position.x + i,
          player.transform.position.y - i,
          player.transform.position.z
        );

        if (!northWestBlocked)
        {
          if (_playerCollisions.CanMoveToSpace(northWest))
          {
            validPositions.Add(northWest);
          }
          else
          {
            northWestBlocked = true;
          }
        }

        if (!northEastBlocked)
        {
          if (_playerCollisions.CanMoveToSpace(northEast))
          {
            validPositions.Add(northEast);
          }
          else
          {
            northEastBlocked = true;
          }
        }

        if (!southWestBlocked)
        {
          if (_playerCollisions.CanMoveToSpace(southWest))
          {
            validPositions.Add(southWest);
          }
          else
          {
            southWestBlocked = true;
          }
        }

        if (!southEastBlocked)
        {
          if (_playerCollisions.CanMoveToSpace(southEast))
          {
            validPositions.Add(southEast);
          }
          else
          {
            southEastBlocked = true;
          }
        }
      }
    }

    return validPositions;
  }

  public List<Vector3> GetValidAttackPositions(Player player)
  {
    var validPositions = new List<Vector3>();
    var direction = _playerSpriteRenderer.GetDirection(player);
    var westFound = false;
    var eastFound = false;
    var northFound = false;
    var southFound = false;

    for (int i = 1; i < player.numberOfAttackSpacesPerTurn + 1; i++)
    {
      var west = new Vector3(
        player.transform.position.x - i,
        player.transform.position.y,
        player.transform.position.z
      );
      var east = new Vector3(
        player.transform.position.x + i,
        player.transform.position.y,
        player.transform.position.z
      );
      var north = new Vector3(
        player.transform.position.x,
        player.transform.position.y - i,
        player.transform.position.z
      );
      var south = new Vector3(
        player.transform.position.x,
        player.transform.position.y + i,
        player.transform.position.z
      );

      if (direction == Direction.North || direction == Direction.South)
      {

        if (!westFound)
        {
          validPositions.Add(west);
        }
        if (!_playerCollisions.CanMoveToSpace(west) && !westFound)
        {
          validPositions.Add(west);
          westFound = true;
        }
        if (!eastFound)
        {
          validPositions.Add(east);
        }
        if (!_playerCollisions.CanMoveToSpace(east) && !eastFound)
        {
          validPositions.Add(east);
          eastFound = true;
        }
      }

      if (direction == Direction.West || direction == Direction.East)
      {

        if (!northFound)
        {
          validPositions.Add(north);
        }
        if (!_playerCollisions.CanMoveToSpace(north) && !northFound)
        {
          validPositions.Add(north);
          northFound = true;
        }
        if (!southFound)
        {
          validPositions.Add(south);
        }
        if (!_playerCollisions.CanMoveToSpace(south) && !southFound)
        {
          validPositions.Add(south);
          southFound = true;
        }
      }
    }
    return validPositions;
  }

  private List<Vector3> GetPossibleMovePositions(Player player)
  {
    var validPositions = new List<Vector3>();
    for (int i = 1; i < player.numberOfMoveSpacesPerTurn + 1; i++)
    {
      // North
      validPositions.Add(
        new Vector3(
          player.transform.position.x,
          player.transform.position.y + i,
          player.transform.position.z
        ));

      // South
      validPositions.Add(
        new Vector3(
          player.transform.position.x,
          player.transform.position.y - i,
          player.transform.position.z
        ));

      // West
      validPositions.Add(
        new Vector3(
          player.transform.position.x - i,
          player.transform.position.y,
          player.transform.position.z
        ));

      // East
      validPositions.Add(
        new Vector3(
          player.transform.position.x + i,
          player.transform.position.y,
          player.transform.position.z
        ));

      if (player.canMoveAcross)
      {
        // North West
        validPositions.Add(
          new Vector3(
            player.transform.position.x - i,
            player.transform.position.y + i,
            player.transform.position.z
          ));

        // North East
        validPositions.Add(
          new Vector3(
            player.transform.position.x + i,
            player.transform.position.y + i,
            player.transform.position.z
          ));

        // South West
        validPositions.Add(
          new Vector3(
            player.transform.position.x - i,
            player.transform.position.y - i,
            player.transform.position.z
          ));

        // South East
        validPositions.Add(
          new Vector3(
            player.transform.position.x + i,
            player.transform.position.y - i,
            player.transform.position.z
          ));
      }
    }

    return validPositions;
  }
}