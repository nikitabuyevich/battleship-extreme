using UnityEngine;

public class PlayerShopingState : IPlayerShopingState
{
  private readonly IPlayerFogOfWar _playerFogOfWar;
  private readonly IGameMap _gameMap;

  public PlayerShopingState(
    IPlayerFogOfWar playerFogOfWar,
    IGameMap gameMap)
  {
    _playerFogOfWar = playerFogOfWar;
    _gameMap = gameMap;
  }
  public void Enter(Player player) { }
  public void Execute(Player player) { }
  public void Exit(Player player)
  {
    _gameMap.CheckAndHideGameEntities(player);
    _playerFogOfWar.ChangeFogOfWar(player, player.revealAlphaLevel);
    _playerFogOfWar.RevealPlayersRefineries(player);
  }
  public void AbilityRotate(Player player) { }
  public void AbilityAttack(Player player) { }
}