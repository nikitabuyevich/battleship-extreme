using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class PlayerMovement : IPlayerMovement
{
  readonly private IFogOfWar _fogOfWar;
  readonly private IPlayerFogOfWar _playerFogOfWar;
  readonly private IBounds _bounds;

  public PlayerMovement(IFogOfWar fogOfWar, IPlayerFogOfWar playerFogOfWar, IBounds bounds)
  {
    _fogOfWar = fogOfWar;
    _playerFogOfWar = playerFogOfWar;
    _bounds = bounds;
  }

  public bool ClickIsValid(Player player)
  {
    if (Input.GetMouseButtonDown(0))
    {
      player.moveSuggestion.GetComponent<MouseMovement>().ClearSuggestions();
      var mousePos = GetMousePos(player);
      if (_bounds.ClickIsValid(mousePos))
      {
        return true;
      }
    }

    return false;
  }

  public Vector3 GetMousePos(Player player)
  {
    var returnedCameraPos = player.gameCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    return new Vector3(
      Mathf.Floor(returnedCameraPos.x - 0.5f) + 1f,
      Mathf.Floor(returnedCameraPos.y - 0.5f) + 1f,
      player.transform.position.z
    );
  }

  public Direction GetMoveDirection(Vector3 mousePos, Player player)
  {
    var startPos = player.transform.position;

    if (startPos.x - mousePos.x > 0)
    {
      return Direction.West;
    }

    else if (startPos.x - mousePos.x < 0)
    {
      return Direction.East;
    }

    else if (startPos.y - mousePos.y > 0)
    {
      return Direction.South;
    }

    // Otherwise moving North
    return Direction.North;
  }

  public Direction GetDirection(Player player)
  {
    var startPos = player.transform.position;

    if (startPos.x - GetMousePos(player).x > 0)
    {
      return Direction.West;
    }

    else if (startPos.x - GetMousePos(player).x < 0)
    {
      return Direction.East;
    }

    else if (startPos.y - GetMousePos(player).y < 0)
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

    var endPos = new Vector3(GetMousePos(player).x, GetMousePos(player).y, startPos.z);

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