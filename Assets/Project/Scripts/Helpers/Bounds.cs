using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bounds : IBounds
{
  private readonly BoundsInt _bounds;
  private readonly IPlayerCollisions _playerCollisions;

  public Bounds(IPlayerCollisions playerCollisions)
  {
    _playerCollisions = playerCollisions;
    _bounds = Get();
  }

  public bool MoveIsValid(Player player, Vector3 pos)
  {
    // clicking outside map
    if (pos.x < 0f || pos.x > (_bounds.size.x - 3) || pos.y < 0f || pos.y > (_bounds.size.y - 3))
    {
      return false;
    }

    var possiblePositions = GetPossiblePositions(player);
    foreach (var possiblePosition in possiblePositions)
    {
      if (possiblePosition == pos && !_playerCollisions.SpaceIsBlocked(pos))
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

  public List<Vector3> GetValidPositions(Player player)
  {
    var validPositions = new List<Vector3>();

    var possiblePositions = GetPossiblePositions(player);
    foreach (var possiblePosition in possiblePositions)
    {
      if (!_playerCollisions.SpaceIsBlocked(possiblePosition))
      {
        validPositions.Add(possiblePosition);
      }
    }

    return validPositions;
  }

  private List<Vector3> GetPossiblePositions(Player player)
  {
    var validPositions = new List<Vector3>();
    for (int i = 1; i < player.numberOfSpacesPerTurn + 1; i++)
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