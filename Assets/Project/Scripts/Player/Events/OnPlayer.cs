using Zenject;

public class OnPlayer : IOnPlayer
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;

  private readonly IMouse _mouse;

  public OnPlayer(IMouse mouse)
  {
    _mouse = mouse;
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
}