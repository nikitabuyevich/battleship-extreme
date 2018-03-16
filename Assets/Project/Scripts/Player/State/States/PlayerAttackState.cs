using UnityEngine;

public class PlayerAttackState : IPlayerAttackState
{
	private readonly IMouse _mouse;

	public PlayerAttackState(
		IMouse mouse)
	{
		_mouse = mouse;
	}

	public void Enter(Player player)
	{
		var mouseUI = player.mouseUI.GetComponent<MouseUI>();
		_mouse.SetAttackCursor(mouseUI);
	}

	public void Execute(Player player)
	{
		player.Move();
		_mouse.DrawSuggestionOverMouse(player);
	}

	public void Exit(Player player) { }

	public void AbilityRotate(Player player) { }
	public void AbilityAttack(Player player) { }
}