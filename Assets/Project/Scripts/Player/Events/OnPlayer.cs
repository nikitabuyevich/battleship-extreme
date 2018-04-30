using Zenject;

public class OnPlayer : IOnPlayer
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;
  [Inject]
  private readonly ShopManager _shopManager;

  private readonly IMouse _mouse;

  public OnPlayer(IMouse mouse)
  {
    _mouse = mouse;
  }

  public void Movement(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();

    _gameSceneManager.numberOfMoves -= 1;
    _gameSceneManager.numberOfMovesThisTurn += 1;
    if (_gameSceneManager.numberOfMoves == 0)
    {
      _mouse.Clear(mouseUI);
      player.isAbleToMove = false;
    }
    _gameSceneManager.SetAbilityButtons();
  }

  public void Attack(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();

    _gameSceneManager.numberOfAttacks -= 1;
    _gameSceneManager.numberOfAttacksThisTurn += 1;
    if (_gameSceneManager.numberOfAttacks == 0)
    {
      _mouse.Clear(mouseUI);
      player.isAbleToAttack = false;
      player.SetInitialState();
    }
    _gameSceneManager.SetAbilityButtons();
  }

  public void Build(Player player)
  {
    _shopManager.PurchaseLevel1Refinery();
    player.numberOfRefineries += 1;
    _gameSceneManager.SetPlayerStats();
    player.SetInitialState();
  }
}