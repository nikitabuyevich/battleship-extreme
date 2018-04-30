using UnityEngine;

public class PlayerWaitingTurnState : IPlayerWaitingTurnState
{
	private readonly IMouse _mouse;

	public PlayerWaitingTurnState(IMouse mouse)
	{
		_mouse = mouse;
	}

	public void Enter(Player player)
	{
		if (player != null)
		{
			Debug.Log(player.name + " entered waiting state!");
			var mouseUI = player.mouseUI.GetComponent<MouseUI>();
			_mouse.Clear(mouseUI);
		}
	}

	public void Execute(Player player) { }

	public void Exit(Player player) { }

	public void AbilityRotate(Player player) { }
	public void AbilityAttack(Player player) { }
}