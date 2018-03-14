using UnityEngine;

public class PlayerMoveState : IState
{
	private readonly Player _player;

	public PlayerMoveState(Player player)
	{
		_player = player;
	}

	public void Enter()
	{
		Debug.Log(_player.name + " entering move state");
	}

	public void Execute()
	{
		_player.Move();
		_player.moveSuggestion.GetComponent<MouseMovement>().DrawPossibleMoves();
	}

	public void Exit()
	{
		Debug.Log(_player.name + " exiting state");
	}
}