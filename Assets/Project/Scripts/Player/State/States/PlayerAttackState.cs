using UnityEngine;

public class PlayerAttackState : IPlayerAttackState
{
	private readonly IMouse _mouse;
	private readonly IAbility _ability;

	public PlayerAttackState(
		IMouse mouse,
		IAbility ability)
	{
		_mouse = mouse;
		_ability = ability;
	}

	public void Enter(Player player)
	{
		Debug.Log(player.name + " entered attack state!");
		var mouseUI = player.mouseUI.GetComponent<MouseUI>();
		_mouse.DrawAttackSuggestions(player);
		_mouse.SetAttackCursor(mouseUI);
	}

	public void Execute(Player player)
	{
		RotateOnMouseWheel(player);
	}

	public void Exit(Player player) { }

	public void AbilityRotate(Player player) { }
	public void AbilityAttack(Player player) { }

	private void RotateOnMouseWheel(Player player)
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			_ability.Rotate(player, false);
			var mouseUI = player.mouseUI.GetComponent<MouseUI>();
			_mouse.DrawAttackSuggestions(player);
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			_ability.Rotate(player, true);
			var mouseUI = player.mouseUI.GetComponent<MouseUI>();
			_mouse.DrawAttackSuggestions(player);
		}
	}
}