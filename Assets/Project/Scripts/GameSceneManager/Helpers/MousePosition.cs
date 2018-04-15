using System.Collections;
using UnityEngine;

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
      // damage effect
      player.SpawnMainHitEffect(mousePos);

      var gameEntity = _playerCollisions.GetGameEntity(mousePos);
      if (gameEntity != null)
      {
        gameEntity.health -= player.attackPower;
      }

      var sideAttackPositions = _gameMap.GetValidSideAttackPositions(player, mousePos);
      foreach (var sideAttackPosition in sideAttackPositions)
      {
        // damage effect
        player.SpawnSideHitEffect(sideAttackPosition);

        var sideHitGameEntity = _playerCollisions.GetGameEntity(sideAttackPosition);
        if (sideHitGameEntity != null)
        {
          sideHitGameEntity.health -= player.sideHitAttackPower;
        }
      }

      _onPlayer.Attack(player);
      player.StartCoroutine(WaitUntilParticlesFade(player));
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

  private IEnumerator WaitUntilParticlesFade(Player player)
  {
    var gameSceneManager = player.gameSceneManager.GetComponent<GameSceneManager>();
    var t = 0f;
    player.ChangeState(typeof(IPlayerWaitingTurnState));

    while (t < 1f)
    {
      t += Time.deltaTime;

      yield return null;
    }

    if (gameSceneManager.numberOfAttacks <= 0)
    {
      player.ChangeState(typeof(IPlayerMoveState));
    }
    else
    {
      player.ChangeState(typeof(IPlayerAttackState));
    }

    yield return 0;
  }
}