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
    var hitSomething = false;
    var mousePos = _mouse.GetMousePos(player);
    var validAttackPositions = _gameMap.GetValidAttackPositions(player);
    if (validAttackPositions.Contains(mousePos))
    {
      // damage effect
      player.SpawnMainHitEffect(mousePos);

      var gameEntity = _playerCollisions.GetGameEntity(mousePos);
      if (gameEntity != null)
      {
        hitSomething = true;
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
          hitSomething = true;
          sideHitGameEntity.health -= player.sideHitAttackPower;
        }
      }

      _onPlayer.Attack(player);
      player.WaitUntilParticlesFade(1f);
      if (hitSomething)
      {
        PlayHitSoundEffect();
      }
      else
      {
        PlayAttackSoundEffect();
      }
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

  private void PlayAttackSoundEffect()
  {
    var soundEffectsManager = GameObject.Find("SoundEffectsManager").GetComponent<SoundEffectsManager>();
    soundEffectsManager.musicSource.clip = soundEffectsManager.attackSoundEffect;
    soundEffectsManager.musicSource.Play();
  }

  private void PlayHitSoundEffect()
  {
    var soundEffectsManager = GameObject.Find("SoundEffectsManager").GetComponent<SoundEffectsManager>();
    soundEffectsManager.musicSource.clip = soundEffectsManager.hitSoundEffect;
    soundEffectsManager.musicSource.Play();
  }
}