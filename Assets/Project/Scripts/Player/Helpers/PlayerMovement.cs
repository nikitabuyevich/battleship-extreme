using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class PlayerMovement : IPlayerMovement
{
  readonly private IFogOfWar _fogOfWar;
  readonly private IPlayerFogOfWar _playerFogOfWar;
  readonly private ICameraPosition _cameraPosition;

  public PlayerMovement(IFogOfWar fogOfWar, IPlayerFogOfWar playerFogOfWar, ICameraPosition cameraPosition)
  {
    _fogOfWar = fogOfWar;
    _playerFogOfWar = playerFogOfWar;
    _cameraPosition = cameraPosition;
  }

  public Vector3 GetMouseClick(Player player)
  {
    var returnedCameraPos = player.gameCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    var test = player.transform.position - returnedCameraPos;
    Debug.Log(returnedCameraPos);
    var clickPos = new Vector3(
      Mathf.Floor(returnedCameraPos.x) + 1,
      Mathf.Floor(returnedCameraPos.y) + 1,
      player.transform.position.z
    );
    // Debug.Log("before round x: " + clickPos.x);
    var xRounded = Mathf.RoundToInt(returnedCameraPos.x);
    var yRounded = Mathf.FloorToInt(returnedCameraPos.y);
    var clickedTile = new Vector3(
      clickPos.x,
      clickPos.y,
      returnedCameraPos.z
    );

    Debug.Log(clickedTile);
    return _cameraPosition.GetGameBounds(clickedTile);
  }

  public Direction GetDirection(Player player)
  {
    var startPos = player.transform.position;

    if (startPos.x - GetMouseClick(player).x > 0)
    {
      return Direction.West;
    }

    else if (startPos.x - GetMouseClick(player).x < 0)
    {
      return Direction.East;
    }

    else if (startPos.y - GetMouseClick(player).y < 0)
    {
      return Direction.South;
    }

    // Otherwise moving North
    return Direction.North;
  }

  public IEnumerator Move(Player player)
  {
    _playerFogOfWar.ChangeFogOfWar(player, player.visitedAlphaLevel);

    player._isMoving = true;
    var startPos = player.transform.position;
    var t = 0f;

    var endPos = new Vector3(GetMouseClick(player).x, GetMouseClick(player).y, startPos.z);

    while (player.transform.position != endPos)
    {
      t += Time.deltaTime * player.moveSpeed;
      player.transform.position = Vector3.Lerp(startPos, endPos, t);
      yield return null;
    }

    _playerFogOfWar.ChangeFogOfWar(player, player.revealAlphaLevel);
    player._isMoving = false;
    GetAllTiles(player);
    yield return 0;
  }

  private void GetAllTiles(Player player)
  {
    // Empty list every time you move
    player.fogOfWar.Clear();

    // Get all tiles and save them in fogOfWar object
    var overallParent = GameObject.FindGameObjectWithTag("Overall Parent");
    var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
    foreach (var tilemap in tilemaps)
    {
      BoundsInt bounds = tilemap.cellBounds;
      var tilePosition = bounds.allPositionsWithin;
      while (tilePosition.MoveNext())
      {
        var currentTileColor = tilemap.GetColor(tilePosition.Current);
        player.fogOfWar.Add(_fogOfWar.GetFogOfWarKey(tilemap.name, tilePosition.Current), currentTileColor);
      }
    }
  }
}