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

    var possiblePositions = GetPossibleMovePositions(player);
    foreach (var possiblePosition in possiblePositions)
    {
      if (_playerCollisions.CanMoveToSpace(possiblePosition))
      {
        validPositions.Add(possiblePosition);
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
        if (_playerCollisions.CanDamage(west) && !westFound)
        {
          validPositions.Add(west);
          westFound = true;
        }
        if (!eastFound)
        {
          validPositions.Add(east);
        }
        if (_playerCollisions.CanDamage(east) && !eastFound)
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
        if (_playerCollisions.CanDamage(north) && !northFound)
        {
          validPositions.Add(north);
          northFound = true;
        }
        if (!southFound)
        {
          validPositions.Add(south);
        }
        if (_playerCollisions.CanDamage(south) && !southFound)
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