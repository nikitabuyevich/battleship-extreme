using UnityEngine;
using Zenject;

public class PlayerAttackState : IPlayerAttackState
{
	[Inject]
	private readonly GameSceneManager _gameSceneManager;

	private readonly IMouse _mouse;
	private readonly IAbility _ability;

	public PlayerAttackState(
		IMouse mouse,
		IAbility ability)
	{
		_mouse = mouse;
		_ability = ability;
	}

	private bool drewAttackSuggestions = false;

	public void Enter(Player player)
	{
		Debug.Log(player.name + " entered attack state!");
		var mouseUI = player.mouseUI.GetComponent<MouseUI>();
		_mouse.Clear(mouseUI);
		_mouse.SetAttackCursor(mouseUI);
		drewAttackSuggestions = false;
	}

	public void Execute(Player player)
	{
		if (!drewAttackSuggestions && _gameSceneManager.numberOfAttacks > 0)
		{
			_mouse.DrawAttackSuggestions(player);
			drewAttackSuggestions = true;
		}

		if (_gameSceneManager.numberOfAttacks > 0)
		{
			player.isAbleToAttack = true;
		}

		if (player.isAbleToAttack)
		{
			RotateOnMouseWheel(player);
			_mouse.DrawAttackSuggestionsHover(player);
			_ability.Attack(player);
		}
	}

	public void Exit(Player player) { }

	public void AbilityRotate(Player player) { }
	public void AbilityAttack(Player player) { }

	private void RotateOnMouseWheel(Player player)
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			_ability.Rotate(player, false);
			_mouse.DrawAttackSuggestions(player);
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			_ability.Rotate(player, true);
			_mouse.DrawAttackSuggestions(player);
		}
	}
}