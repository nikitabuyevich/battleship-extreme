using Zenject;

public class OnPlayer : IOnPlayer
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;

  private readonly IMouse _mouse;
  private readonly IPlayerFogOfWar _playerFogOfWar;

  public OnPlayer(IMouse mouse, IPlayerFogOfWar playerFogOfWar)
  {
    _mouse = mouse;
    _playerFogOfWar = playerFogOfWar;
  }

  public void Movement(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();

    _gameSceneManager.numberOfMoves -= 1;
    if (_gameSceneManager.numberOfMoves == 0)
    {
      _mouse.Clear(mouseUI);
      player.isAbleToMove = false;
    }
  }

  public void Attack(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();

    _gameSceneManager.numberOfAttacks -= 1;
    if (_gameSceneManager.numberOfAttacks == 0)
    {
      _mouse.Clear(mouseUI);
      player.isAbleToAttack = false;
      player.SetInitialState();
    }
  }

  public void Build(Player player)
  {
    player.numberOfRefineries -= 1;
    player.SetInitialState();
    _gameSceneManager.SetPlayerStats();
    _playerFogOfWar.RevealPlayersRefineries(player);
  }
}