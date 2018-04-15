public class PlayerMoney : IPlayerMoney
{
  private readonly IPlayerCollisions _playerCollisions;

  public PlayerMoney(IPlayerCollisions playerCollisions)
  {
    _playerCollisions = playerCollisions;
  }

  public void CheckIfOnMoney(Player player)
  {
    if (player != null)
    {
      var playerPos = player.transform.position;
      if (_playerCollisions.IsOnMoney(playerPos))
      {
        var money = _playerCollisions.GetMoneyStandingOn(playerPos);
        player.money += money.money;
        var moneyFactory = money.spawner.GetComponent<MoneyFactory>();
        moneyFactory.moneySpawned = false;
        var gameSceneManager = player.gameSceneManager.GetComponent<GameSceneManager>();
        moneyFactory.modifiedRespawnTime = gameSceneManager.numberOfTurns + (moneyFactory.respawnTime * gameSceneManager.numberOfPlayers);
        money.MoneyCollected();
        gameSceneManager.SetPlayerStats();
      }
    }
  }
}