using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class PlayerMovement : IPlayerMovement
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;

  private readonly IFogOfWar _fogOfWar;
  private readonly IPlayerFogOfWar _playerFogOfWar;
  private readonly IPlayerMoney _playerMoney;
  private readonly IMouse _mouse;
  private readonly IGameMap _gameMap;

  public PlayerMovement(
    IFogOfWar fogOfWar,
    IPlayerFogOfWar playerFogOfWar,
    IPlayerMoney playerMoney,
    IMouse mouse,
    IGameMap gameMap)
  {
    _fogOfWar = fogOfWar;
    _playerFogOfWar = playerFogOfWar;
    _playerMoney = playerMoney;
    _mouse = mouse;
    _gameMap = gameMap;
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
    PlayMoveSoundEffect();
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();
    _mouse.Clear(mouseUI);
    _playerFogOfWar.ChangeFogOfWar(player, player.visitedAlphaLevel);
    _playerFogOfWar.RevealPlayersRefineries(player);

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
    AddAllTilesOf(player);
    _playerMoney.CheckIfOnMoney(player);
    _gameMap.CheckAndHideGameEntities(player);

    // check if more moves are available
    if (_gameSceneManager.numberOfMoves > 0)
    {
      _mouse.DrawMoveSuggestions(player);
    }

    yield return 0;
  }

  private void AddAllTilesOf(Player player)
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

  private void PlayMoveSoundEffect()
  {
    var soundEffectsManager = GameObject.Find("SoundEffectsManager").GetComponent<SoundEffectsManager>();
    soundEffectsManager.musicSource.clip = soundEffectsManager.moveSoundEffect;
    soundEffectsManager.musicSource.Play();
  }
}