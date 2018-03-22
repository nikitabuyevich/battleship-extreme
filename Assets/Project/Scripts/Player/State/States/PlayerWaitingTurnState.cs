using UnityEngine;

public class PlayerWaitingTurnState : IPlayerWaitingTurnState
{
	private readonly IMouse _mouse;
	private readonly IAbility _ability;

	public PlayerWaitingTurnState(
		IMouse mouse,
		IAbility ability)
	{
		_mouse = mouse;
		_ability = ability;
	}

	public void Enter(Player player)
	{
		Debug.Log(player.name + " entered waiting state!");
		var mouseUI = player.mouseUI.GetComponent<MouseUI>();
		_mouse.Clear(mouseUI);
	}

	public void Execute(Player player) { }

	public void Exit(Player player) { }

	public void AbilityRotate(Player player)
	{
		_ability.Rotate(player, false);
	}
	public void AbilityAttack(Player player) { }
}