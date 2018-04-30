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

    _gameSceneManager.numberOfMovesThisTurn = 0;
    _gameSceneManager.numberOfAttacksThisTurn = 0;
    _gameSceneManager.SetPlayerStats();
  }

  public void UpdatePlayerUI()
  {
    _gameSceneManager.SetPlayerStats();
  }

  private void ClearCurrentPlayer()
  {
    CurrentPlayer().money += CurrentPlayer().income;
    var mouseUI = CurrentPlayer().mouseUI.GetComponent<MouseUI>();
    _mouse.Clear(mouseUI);

    CurrentPlayer().OnPlayerMovement -= OnPlayerMovement;
    CurrentPlayer().ChangeState(typeof(IPlayerWaitingTurnState));
  }

  public Player GetNextPlayer()
  {
    var nextPlayersTurn = GetNextPlayersTurn(_gameSceneManager.currentPlayersTurn);
    return GetPlayer(nextPlayersTurn);
  }

  private int GetNextPlayersTurn(int playersTurn)
  {
    if (playersTurn + 1 >= _gameSceneManager.players.Length)
    {
      playersTurn = 0;
    }
    else
    {
      playersTurn += 1;
    }

    if (_gameSceneManager.players[playersTurn] == null)
    {
      return GetNextPlayersTurn(playersTurn);
    }

    return playersTurn;
  }

  public void NextPlayer()
  {
    ClearCurrentPlayer();

    var nextPlayersTurn = GetNextPlayersTurn(_gameSceneManager.currentPlayersTurn);
    _gameSceneManager.currentPlayersTurn = nextPlayersTurn;
    var nextPlayer = GetPlayer(nextPlayersTurn);

    nextPlayer.ChangeState(typeof(IPlayerMoveState));
    nextPlayer.OnPlayerMovement += OnPlayerMovement;
    _gameSceneManager.numberOfMovesThisTurn = 0;
    _gameSceneManager.numberOfAttacksThisTurn = 0;
    _gameSceneManager.SetPlayerStats();
  }

  public Player CurrentPlayer()
  {
    return _gameSceneManager.players[_gameSceneManager.currentPlayersTurn];
  }

  private Player GetPlayer(int playersTurn)
  {
    return _gameSceneManager.players[playersTurn];
  }

  private void OnPlayerMovement()
  {
    _onPlayer.Movement(CurrentPlayer());
  }
}