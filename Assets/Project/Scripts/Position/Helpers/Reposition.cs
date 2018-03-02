using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class Reposition : IReposition
{
  [Inject]
  private readonly GameObject _levelPosition;

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
    Vector3 leftMostCorner = new Vector3();

    var children = _levelPosition.GetComponentsInChildren<Transform>();
    foreach (var child in children)
    {
      if (child.transform.name == "Base")
      {
        leftMostCorner = child.GetComponent<Tilemap>().origin;
      }
    }

    // Subtract by 1.5 units to position objects in bottom left corner
    var moveUnits = 1.5f;

    return new Vector3(
      Mathf.Abs(leftMostCorner.x + moveUnits),
      Mathf.Abs(leftMostCorner.y + moveUnits),
      pos.z
    );
  }

  public void SetLevel()
  {
    _levelPosition.transform.position = GetRepositionVector3(_levelPosition.transform.position);

    // reveal fog of starting location
    var players = GameObject.FindGameObjectWithTag("Overall Parent").GetComponentsInChildren<Player>();
    foreach (var player in players)
    {
      // move player to starting pos
      player.transform.position = new Vector3(player.startingX, player.startingY, _levelPosition.transform.position.z);
    }
  }
}