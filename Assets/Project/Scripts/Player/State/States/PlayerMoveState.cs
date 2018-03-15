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
		_player.mouseUI.GetComponent<MouseUI>().DrawPossibleMoves();
	}

	public void Execute()
	{
		_player.Move();
		_player.mouseUI.GetComponent<MouseUI>().DrawSuggestionOverMouse();
	}

	public void Exit()
	{
		Debug.Log(_player.name + " exiting state");
	}
}