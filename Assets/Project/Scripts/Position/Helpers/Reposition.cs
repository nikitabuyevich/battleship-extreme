using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class Reposition : IReposition
{
  [Inject]
  private readonly LevelPosition _levelPosition;

  public Vector3Int GetStartingTileLocation()
  {
    var startingFloorLocation = GetRepositionVector3(_levelPosition.transform.position);
    return new Vector3Int(
      (-1 * (int) (startingFloorLocation.x + 0.5f)),
      (-1 * (int) (startingFloorLocation.y + 0.5f)),
      (int) startingFloorLocation.z
    );
  }

  public Vector3 GetRepositionVector3(Vector3 pos)
  {
    var children = _levelPosition.GetComponentsInChildren<Transform>();
    foreach (var child in children)
    {
      if (child.transform.name == "Base")
      {
        _levelPosition.leftMostCorner = child.GetComponent<Tilemap>().origin;
      }
    }

    // Subtract by 1.5 units to position objects in bottom left corner
    var moveUnits = 1.5f;

    return new Vector3(
      Mathf.Abs(_levelPosition.leftMostCorner.x + moveUnits),
      Mathf.Abs(_levelPosition.leftMostCorner.y + moveUnits),
      pos.z
    );
  }
}