public class MousePosition : IMousePosition
{
  private readonly IMouse _mouse;
  private readonly IGameMap _gameMap;
  private readonly IPlayerCollisions _playerCollisions;
  private readonly IOnPlayer _onPlayer;

  public MousePosition(
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

  public void Attack(Player player)
  {
    var mousePos = _mouse.GetMousePos(player);
    var validAttackPositions = _gameMap.GetValidAttackPositions(player);
    if (validAttackPositions.Contains(mousePos))
    {
      var gameEntity = _playerCollisions.GetGameEntity(mousePos);
      if (gameEntity != null)
      {
        gameEntity.health -= player.attackPower;
      }

      var sideAttackPositions = _gameMap.GetValidSideAttackPositions(player, mousePos);
      foreach (var sideAttackPosition in sideAttackPositions)
      {
        var sideHitGameEntity = _playerCollisions.GetGameEntity(sideAttackPosition);
        if (sideHitGameEntity != null)
        {
          sideHitGameEntity.health -= player.sideHitAttackPower;
        }
      }

      _onPlayer.Attack(player);
    }
  }

  public void Build(Player player)
  {
    var mousePos = _mouse.GetMousePos(player);
    var buildPositions = _gameMap.GetBuildPositions(player);
    if (buildPositions.Contains(mousePos))
    {
      player.CreateRefinery(mousePos);

      _onPlayer.Build(player);
    }
  }
}