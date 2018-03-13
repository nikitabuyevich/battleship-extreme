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
      Mathf.Clamp(_turn.CurrentPlayer().transform.position.x, 4.76f, _bounds.xMax + 1 + magicNumber),
      Mathf.Clamp(_turn.CurrentPlayer().transform.position.y, 2f, _bounds.yMax),
      zPos);
  }

  public Vector3 GetGameBounds(Vector3 pos)
  {
    return new Vector3(
      Mathf.Clamp(pos.x, 0f, _bounds.size.x - 3),
      Mathf.Clamp(pos.y, 0f, _bounds.size.y - 3),
      pos.z);
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