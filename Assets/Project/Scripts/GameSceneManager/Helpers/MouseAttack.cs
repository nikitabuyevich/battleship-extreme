public class MouseAttack : IMouseAttack
{
  private readonly IMouse _mouse;
  private readonly IGameMap _gameMap;
  private readonly IPlayerCollisions _playerCollisions;

  public MouseAttack(IMouse mouse, IGameMap gameMap, IPlayerCollisions playerCollisions)
  {
    _mouse = mouse;
    _gameMap = gameMap;
    _playerCollisions = playerCollisions;
  }

  public void AttackPosition(Player player)
  {
    var mousePos = _mouse.GetMousePos(player);
    var hit = _playerCollisions.GetHit(mousePos);
    if (hit != null)
    {
      var hitObject = hit.gameObject;
    }
  }

}