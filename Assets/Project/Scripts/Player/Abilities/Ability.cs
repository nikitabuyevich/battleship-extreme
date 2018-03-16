using Zenject;

public class Ability : IAbility
{
  private readonly IPlayerSpriteRenderer _playerSpriteRenderer;
  private readonly IMouse _mouse;
  private readonly IOnPlayer _onPlayer;

  public Ability(
    IPlayerSpriteRenderer playerSpriteRenderer,
    IMouse mouse,
    IOnPlayer onPlayer)
  {
    _playerSpriteRenderer = playerSpriteRenderer;
    _mouse = mouse;
    _onPlayer = onPlayer;
  }

  public void Rotate(Player player)
  {
    var playersCurrentDir = _playerSpriteRenderer.GetDirection(player);

    // Render new direction
    if (playersCurrentDir == Direction.West)
    {
      _playerSpriteRenderer.RenderDirection(player, Direction.North);
    }
    else if (playersCurrentDir == Direction.North)
    {
      _playerSpriteRenderer.RenderDirection(player, Direction.East);
    }
    else if (playersCurrentDir == Direction.East)
    {
      _playerSpriteRenderer.RenderDirection(player, Direction.South);
    }
    else
    {
      _playerSpriteRenderer.RenderDirection(player, Direction.West);
    }

    _onPlayer.Movement(player);
  }

  public void Attack(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();
    _mouse.SetAttackCursor(mouseUI);
  }
}