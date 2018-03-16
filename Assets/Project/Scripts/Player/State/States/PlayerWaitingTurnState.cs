using UnityEngine;

public class PlayerWaitingTurnState : IPlayerWaitingTurnState
{
	public void Enter(Player player)
	{
		Debug.Log(player.name + " entering wait state");
	}

	public void Execute(Player player) { }

	public void Exit(Player player) { }

	public void AbilityRotate(Player player) { }
	public void AbilityAttack(Player player) { }
}