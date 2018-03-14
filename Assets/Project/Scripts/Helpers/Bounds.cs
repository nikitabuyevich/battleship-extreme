using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bounds : IBounds
{
  private readonly ITurn _turn;
  private readonly BoundsInt _bounds;

  public Bounds(ITurn turn)
  {
    _turn = turn;
    _bounds = Get();
  }

  public bool ClickIsValid(Vector3 pos)
  {
    // clicking outside map
    if (pos.x < 0f || pos.x > (_bounds.size.x - 3) || pos.y < 0f || pos.y > (_bounds.size.y - 3))
    {
      return false;
    }

    var validPositions = GetValidPositions();
    foreach (var validPosition in validPositions)
    {
      if (validPosition == pos)
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

  public List<Vector3> GetValidPositions()
  {
    var validPositions = new List<Vector3>();
    for (int i = 1; i < _turn.CurrentPlayer().numberOfSpacesPerTurn + 1; i++)
    {
      // North
      validPositions.Add(
        new Vector3(
          _turn.CurrentPlayer().transform.position.x,
          _turn.CurrentPlayer().transform.position.y + i,
          _turn.CurrentPlayer().transform.position.z
        ));

      // South
      validPositions.Add(
        new Vector3(
          _turn.CurrentPlayer().transform.position.x,
          _turn.CurrentPlayer().transform.position.y - i,
          _turn.CurrentPlayer().transform.position.z
        ));

      // West
      validPositions.Add(
        new Vector3(
          _turn.CurrentPlayer().transform.position.x - i,
          _turn.CurrentPlayer().transform.position.y,
          _turn.CurrentPlayer().transform.position.z
        ));

      // East
      validPositions.Add(
        new Vector3(
          _turn.CurrentPlayer().transform.position.x + i,
          _turn.CurrentPlayer().transform.position.y,
          _turn.CurrentPlayer().transform.position.z
        ));

      if (_turn.CurrentPlayer().canMoveAcross)
      {
        // North West
        validPositions.Add(
          new Vector3(
            _turn.CurrentPlayer().transform.position.x - i,
            _turn.CurrentPlayer().transform.position.y + i,
            _turn.CurrentPlayer().transform.position.z
          ));

        // North East
        validPositions.Add(
          new Vector3(
            _turn.CurrentPlayer().transform.position.x + i,
            _turn.CurrentPlayer().transform.position.y + i,
            _turn.CurrentPlayer().transform.position.z
          ));

        // South West
        validPositions.Add(
          new Vector3(
            _turn.CurrentPlayer().transform.position.x - i,
            _turn.CurrentPlayer().transform.position.y - i,
            _turn.CurrentPlayer().transform.position.z
          ));

        // South East
        validPositions.Add(
          new Vector3(
            _turn.CurrentPlayer().transform.position.x + i,
            _turn.CurrentPlayer().transform.position.y - i,
            _turn.CurrentPlayer().transform.position.z
          ));
      }
    }

    return validPositions;
  }
}