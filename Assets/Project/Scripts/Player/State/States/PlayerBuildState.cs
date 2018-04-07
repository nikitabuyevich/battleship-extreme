using UnityEngine;
using Zenject;

public class PlayerBuildState : IPlayerBuildState
{
  [Inject]
  private readonly GameSceneManager _gameSceneManager;

  private readonly IMouse _mouse;
  private readonly IAbility _ability;

  public PlayerBuildState(
    IMouse mouse,
    IAbility ability)
  {
    _mouse = mouse;
    _ability = ability;
  }
  private bool drewBuildSuggestions = false;

  public void Enter(Player player)
  {
    Debug.Log(player.name + " entered build state!");
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();
    _mouse.Clear(mouseUI);
    drewBuildSuggestions = false;
  }

  public void Execute(Player player)
  {
    if (!drewBuildSuggestions)
    {
      _mouse.DrawBuildRefinerySuggestions(player);
      drewBuildSuggestions = true;
    }

    _mouse.DrawBuildRefinerySuggestionHover(player);
    _ability.Build(player);
  }

  public void Exit(Player player) { }
  public void AbilityRotate(Player player) { }
  public void AbilityAttack(Player player) { }
}