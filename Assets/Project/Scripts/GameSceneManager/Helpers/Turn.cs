using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class Turn : ITurn
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;

  readonly private IFogOfWar _fogOfWar;
  readonly private IPlayerFogOfWar _playerFogOfWar;
  readonly private IPlayerMoveState _playerMoveState;
  readonly private IPlayerWaitingTurnState _playerWaitingTurnState;

  public Turn(
    IFogOfWar fogOfWar,
    IPlayerFogOfWar playerFogOfWar,
    IPlayerMoveState playerMoveState,
    IPlayerWaitingTurnState playerWaitingTurnState)
  {
    _fogOfWar = fogOfWar;
    _playerFogOfWar = playerFogOfWar;
    _playerMoveState = playerMoveState;
    _playerWaitingTurnState = playerWaitingTurnState;
  }

  public void ResetAll()
  {
    _gameSceneManager.currentPlayersTurn = 0;

    for (int i = 0; i < _gameSceneManager.players.Length; i++)
    {
      var player = _gameSceneManager.players[i];
      if (i == 0)
      {
        player.OnPlayerMovement += OnPlayerMovement;
        player.ChangeState(_playerMoveState);
      }
      else
      {
        player.OnPlayerMovement -= OnPlayerMovement;
        player.ChangeState(_playerWaitingTurnState);
      }
    }

    _gameSceneManager.numberOfMoves = CurrentPlayer().numberOfMovesPerTurn;
  }

  public void NextPlayer()
  {
    CurrentPlayer().OnPlayerMovement -= OnPlayerMovement;
    CurrentPlayer().ChangeState(_playerWaitingTurnState);

    if ((_gameSceneManager.currentPlayersTurn + 1) != _gameSceneManager.players.Length)
    {
      _gameSceneManager.currentPlayersTurn += 1;
    }
    else
    {
      _gameSceneManager.currentPlayersTurn = 0;
    }

    CurrentPlayer().ChangeState(_playerMoveState);
    CurrentPlayer().OnPlayerMovement += OnPlayerMovement;
    _gameSceneManager.numberOfMoves = CurrentPlayer().numberOfMovesPerTurn;
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
      CurrentPlayer().ChangeState(_playerWaitingTurnState);
    }
  }
}