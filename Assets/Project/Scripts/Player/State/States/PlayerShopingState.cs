using UnityEngine;

public class PlayerShopingState : IPlayerShopingState
{
  private readonly IPlayerFogOfWar _playerFogOfWar;
  private readonly IGameMap _gameMap;
  private readonly IMouse _mouse;

  public PlayerShopingState(
    IPlayerFogOfWar playerFogOfWar,
    IMouse mouse,
    IGameMap gameMap)
  {
    _playerFogOfWar = playerFogOfWar;
    _gameMap = gameMap;
    _mouse = mouse;
  }
  public void Enter(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();
    _mouse.SetDefaultCursor(mouseUI);
  }
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