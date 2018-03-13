using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraPosition : ICameraPosition
{

  private readonly ITurn _turn;
  private readonly BoundsInt _bounds;

  public CameraPosition(ITurn turn)
  {
    _turn = turn;
    _bounds = GetBounds();
  }

  // Basically in order to get a 16:9 perfect pixel ratio we need this
  private float magicNumber = 0.26f;

  public Vector3 GetCameraPoisition(float zPos)
  {
    return new Vector3(
      Mathf.Clamp(_turn.CurrentPlayer().transform.position.x, 4.76f, GetBounds().xMax + 1 + magicNumber),
      Mathf.Clamp(_turn.CurrentPlayer().transform.position.y, 2f, GetBounds().yMax),
      zPos);
  }

  private BoundsInt GetBounds()
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
}