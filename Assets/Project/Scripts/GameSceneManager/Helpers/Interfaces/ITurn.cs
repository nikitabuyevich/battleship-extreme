public interface ITurn
{
  void Init(Player[] players, GameSceneManager gameSceneManager);
  void ResetAll();
  void NextPlayer();
  Player CurrentPlayer();
}