using UnityEngine;
using UnityEngine.Tilemaps;

public class Turn : ITurn
{
  readonly private IFogOfWar _fogOfWar;
  readonly private IPlayerFogOfWar _playerFogOfWar;

  public Turn(IFogOfWar fogOfWar, IPlayerFogOfWar playerFogOfWar)
  {
    _fogOfWar = fogOfWar;
    _playerFogOfWar = playerFogOfWar;
  }

  private Player[] players;
  private GameSceneManager gameSceneManager;

  public void Init(Player[] players, GameSceneManager gameSceneManager)
  {
    this.players = players;
    this.gameSceneManager = gameSceneManager;
  }

  public void ResetAll()
  {
    for (int i = 0; i < players.Length; i++)
    {
      var player = players[i];
      if (i == 0)
      {
        player.isAllowedToMove = true;
        player.OnPlayerMovement += OnPlayerMovement;
      }
      else
      {
        player.isAllowedToMove = false;
        player.OnPlayerMovement -= OnPlayerMovement;
      }
    }

    gameSceneManager.numberOfMoves = CurrentPlayer().numberOfMovesPerTurn;
  }

  public void NextPlayer()
  {
    CurrentPlayer().OnPlayerMovement -= OnPlayerMovement;
    CurrentPlayer().isAllowedToMove = false;

    if ((gameSceneManager.currentPlayersTurn + 1) != players.Length)
    {
      gameSceneManager.currentPlayersTurn += 1;
    }
    else
    {
      gameSceneManager.currentPlayersTurn = 0;
    }

    CurrentPlayer().isAllowedToMove = true;
    CurrentPlayer().OnPlayerMovement += OnPlayerMovement;
    gameSceneManager.numberOfMoves = CurrentPlayer().numberOfMovesPerTurn;

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
    return players[gameSceneManager.currentPlayersTurn];
  }

  private void OnPlayerMovement()
  {
    gameSceneManager.numberOfMoves -= 1;
    if (gameSceneManager.numberOfMoves == 0)
    {
      CurrentPlayer().isAllowedToMove = false;
    }
  }
}