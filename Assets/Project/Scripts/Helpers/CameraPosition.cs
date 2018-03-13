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

  public Vector3 GetCameraPoisition(float zPos)
  {
    return new Vector3(
      _turn.CurrentPlayer().transform.position.x + 0.5f,
      _turn.CurrentPlayer().transform.position.y + 0.5f,
      zPos
    );
    // return new Vector3(
    //   Mathf.Clamp(_turn.CurrentPlayer().transform.position.x, 5f, _bounds.xMax + 1),
    //   // Mathf.Clamp(_turn.CurrentPlayer().transform.position.x, 4.76f, _bounds.xMax),
    //   Mathf.Clamp(_turn.CurrentPlayer().transform.position.y, 2f, _bounds.yMax),
    //   zPos);
  }

  public bool ClickIsBoundValid(Vector3 pos)
  {
    // if (pos.x < 0f || pos.x > (_bounds.size.x - 3))
    // {
    //   return false;
    // }
    // if (pos.y < 0f || pos.y > (_bounds.size.y - 3))
    // {
    //   return false;
    // }

    return true;
  }

  public BoundsInt GetBounds()
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