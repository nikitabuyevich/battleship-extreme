using UnityEngine;

public class PlayerWaitingTurnState : IState
{
	private readonly Player _player;

	public PlayerWaitingTurnState(Player player)
	{
		_player = player;
	}
	public void Enter()
	{
		Debug.Log(_player.name + " entering waiting turn state");
	}

	public void Execute()
	{
		// Debug.Log(_player.name + " execute waiting state");
	}

	public void Exit()
	{
		Debug.Log(_player.name + " exiting waiting turn state");
	}
}