using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class Turn : ITurn
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;

  readonly private IFogOfWar _fogOfWar;
  readonly private IPlayerFogOfWar _playerFogOfWar;

  public Turn(IFogOfWar fogOfWar, IPlayerFogOfWar playerFogOfWar)
  {
    _fogOfWar = fogOfWar;
    _playerFogOfWar = playerFogOfWar;
  }

  public void ResetAll()
  {
    _gameSceneManager.currentPlayersTurn = 0;

    for (int i = 0; i < _gameSceneManager.players.Length; i++)
    {
      var player = _gameSceneManager.players[i];
      if (i == 0)
      {
        player.isAllowedToMove = true;
        player.OnPlayerMovement += OnPlayerMovement;
        player.GetComponent<PlayerStateMachine>().ChangeState(new PlayerMoveState(player));
      }
      else
      {
        player.isAllowedToMove = false;
        player.OnPlayerMovement -= OnPlayerMovement;
        player.GetComponent<PlayerStateMachine>().ChangeState(new PlayerWaitingTurnState(player));
      }
    }

    _gameSceneManager.numberOfMoves = CurrentPlayer().numberOfMovesPerTurn;
  }

  public void NextPlayer()
  {
    CurrentPlayer().OnPlayerMovement -= OnPlayerMovement;
    CurrentPlayer().isAllowedToMove = false;

    if ((_gameSceneManager.currentPlayersTurn + 1) != _gameSceneManager.players.Length)
    {
      _gameSceneManager.currentPlayersTurn += 1;
    }
    else
    {
      _gameSceneManager.currentPlayersTurn = 0;
    }

    CurrentPlayer().isAllowedToMove = true;
    CurrentPlayer().OnPlayerMovement += OnPlayerMovement;
    _gameSceneManager.numberOfMoves = CurrentPlayer().numberOfMovesPerTurn;

    LoadPlayersFogOfWar();
  }

  private void LoadPlayersFogOfWar()
  {
    if (CurrentPlayer().fogOfWar.Count == 0)
    {
      _fogOfWar.SetFogOfWar();
      _playerFogOfWar.ChangeFogOfWar(CurrentPlayer(), CurrentPlayer().revealAlphaLevel);
    }
    else
    {
      _fogOfWar.SetPlayersFogOfWar(CurrentPlayer());
    }
  }

  public Player CurrentPlayer()
  {
    return _gameSceneManager.players[_gameSceneManager.currentPlayersTurn];
  }

  private void OnPlayerMovement()
  {
    _gameSceneManager.numberOfMoves -= 1;
    if (_gameSceneManager.numberOfMoves == 0)
    {
      CurrentPlayer().isAllowedToMove = false;
    }
  }
}