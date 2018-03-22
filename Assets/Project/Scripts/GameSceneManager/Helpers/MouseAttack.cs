public class MouseAttack : IMouseAttack
{
  private readonly IMouse _mouse;
  private readonly IGameMap _gameMap;
  private readonly IPlayerCollisions _playerCollisions;
  private readonly IOnPlayer _onPlayer;

  public MouseAttack(
    IMouse mouse,
    IGameMap gameMap,
    IPlayerCollisions playerCollisions,
    IOnPlayer onPlayer)
  {
    _mouse = mouse;
    _gameMap = gameMap;
    _playerCollisions = playerCollisions;
    _onPlayer = onPlayer;
  }

  public void AttackPosition(Player player)
  {
    var mousePos = _mouse.GetMousePos(player);
    var validAttackPositions = _gameMap.GetValidAttackPositions(player);
    if (validAttackPositions.Contains(mousePos))
    {
      var hit = _playerCollisions.GetHit(mousePos);
      if (hit != null)
      {
        var gameEntity = hit.gameObject.GetComponent<GameEntity>();
        gameEntity.health -= player.attackPower;

      }

      var sideAttackPositions = _gameMap.GetValidSideAttackPositions(player, mousePos);
      foreach (var sideAttackPosition in sideAttackPositions)
      {
        var sideHit = _playerCollisions.GetHit(sideAttackPosition);
        if (sideHit != null)
        {
          var sideEntity = sideHit.gameObject.GetComponent<GameEntity>();
          sideEntity.health -= player.sideHitAttackPower;
        }
      }

      _onPlayer.Attack(player);
    }
  }

}