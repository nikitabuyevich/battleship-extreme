using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : IPlayerMovement
{
  readonly private IFogOfWar _fogOfWar;
  readonly private IPlayerFogOfWar _playerFogOfWar;

  public PlayerMovement(IFogOfWar fogOfWar, IPlayerFogOfWar playerFogOfWar)
  {
    _fogOfWar = fogOfWar;
    _playerFogOfWar = playerFogOfWar;
  }

  public void GetInput(Player player)
  {
    player._input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    if (Mathf.Abs(player._input.x) > Mathf.Abs(player._input.y))
    {
      player._input.y = 0;
    }
    else
    {
      player._input.x = 0;
    }
  }

  public Direction GetDirection(Vector2 input)
  {
    if (input.x < 0)
    {
      return Direction.West;
    }

    else if (input.x > 0)
    {
      return Direction.East;
    }

    else if (input.y < 0)
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

    var endPos = new Vector3(startPos.x + System.Math.Sign(player._input.x), startPos.y + System.Math.Sign(player._input.y), startPos.z);

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