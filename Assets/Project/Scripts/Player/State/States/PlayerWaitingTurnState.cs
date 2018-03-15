using UnityEngine;

public class PlayerWaitingTurnState : IPlayerWaitingTurnState
{
	public void Enter(Player player)
	{
		Debug.Log(player.name + " entering waiting turn state");
	}

	public void Execute(Player player)
	{
		// Debug.Log(player.name + " execute waiting state");
	}

	public void Exit(Player player)
	{
		Debug.Log(player.name + " exiting waiting turn state");
	}
}