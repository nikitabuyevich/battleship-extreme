using System;

public interface ITurn
{
  void ResetAll();
  void NextPlayer();
  Player CurrentPlayer();
  Player GetNextPlayer();
  void UpdatePlayerUI();
}