using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class Turn : ITurn
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;

  private readonly IMouse _mouse;
  private readonly IOnPlayer _onPlayer;

  public Turn(
    IMouse mouse,
    IOnPlayer onPlayer)
  {
    _mouse = mouse;
    _onPlayer = onPlayer;
  }

  public void ResetAll()
  {
    _gameSceneManager.currentPlayersTurn = 0;

    for (int i = _gameSceneManager.players.Length - 1; i >= 0; i--)
    {
      var player = _gameSceneManager.players[i];
      if (i == 0)
      {
        player.OnPlayerMovement += OnPlayerMovement;
        player.ChangeState(typeof(IPlayerMoveState));
      }
      else
      {
        player.OnPlayerMovement -= OnPlayerMovement;
        player.ChangeState(typeof(IPlayerWaitingTurnState));
      }
    }

    _gameSceneManager.numberOfMoves = CurrentPlayer().numberOfMovesPerTurn;
    _gameSceneManager.numberOfAttacks = CurrentPlayer().numberOfAttacksPerTurn;
  }

  public void NextPlayer()
  {
    var mouseUI = CurrentPlayer().mouseUI.GetComponent<MouseUI>();
    _mouse.Clear(mouseUI);

    CurrentPlayer().OnPlayerMovement -= OnPlayerMovement;
    CurrentPlayer().ChangeState(typeof(IPlayerWaitingTurnState));

    if ((_gameSceneManager.currentPlayersTurn + 1) != _gameSceneManager.players.Length)
    {
      _gameSceneManager.currentPlayersTurn += 1;
      if (_gameSceneManager.players[_gameSceneManager.currentPlayersTurn] == null)
      {
        if ((_gameSceneManager.currentPlayersTurn + 1) != _gameSceneManager.players.Length)
        {
          _gameSceneManager.currentPlayersTurn += 1;
        }
        else
        {
          _gameSceneManager.currentPlayersTurn = 0;
        }
      }
    }
    else
    {
      _gameSceneManager.currentPlayersTurn = 0;
    }

    CurrentPlayer().ChangeState(typeof(IPlayerMoveState));
    CurrentPlayer().OnPlayerMovement += OnPlayerMovement;
    _gameSceneManager.numberOfMoves = CurrentPlayer().numberOfMovesPerTurn;
    _gameSceneManager.numberOfAttacks = CurrentPlayer().numberOfAttacksPerTurn;
  }

  public Player GetNextPlayer()
  {
    var currentPlayersTurn = _gameSceneManager.currentPlayersTurn;
    if ((currentPlayersTurn + 1) != _gameSceneManager.players.Length)
    {
      currentPlayersTurn += 1;
      if (_gameSceneManager.players[currentPlayersTurn] == null)
      {
        if ((currentPlayersTurn + 1) != _gameSceneManager.players.Length)
        {
          currentPlayersTurn += 1;
        }
        else
        {
          currentPlayersTurn = 0;
        }
      }
    }
    else
    {
      currentPlayersTurn = 0;
    }

    return _gameSceneManager.players[currentPlayersTurn];
  }

  public Player CurrentPlayer()
  {
    return _gameSceneManager.players[_gameSceneManager.currentPlayersTurn];
  }

  private void OnPlayerMovement()
  {
    _onPlayer.Movement(CurrentPlayer());
  }
}