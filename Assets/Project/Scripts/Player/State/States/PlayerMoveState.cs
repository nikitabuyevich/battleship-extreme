using UnityEngine;

public class PlayerMoveState : IPlayerMoveState
{
	private readonly IPlayerFogOfWar _playerFogOfWar;
	private readonly IFogOfWar _fogOfWar;

	public PlayerMoveState(IFogOfWar fogOfWar, IPlayerFogOfWar playerFogOfWar)
	{
		_fogOfWar = fogOfWar;
		_playerFogOfWar = playerFogOfWar;
	}

	public void Enter(Player player)
	{
		Debug.Log(player.name + " entering move state");
		LoadPlayersFogOfWar(player);
		player.mouseUI.GetComponent<MouseUI>().DrawPossibleMoves();
	}

	public void Execute(Player player)
	{
		player.Move();
		player.mouseUI.GetComponent<MouseUI>().DrawSuggestionOverMouse();
	}

	public void Exit(Player player)
	{
		Debug.Log(player.name + " exiting state");
	}

	private void LoadPlayersFogOfWar(Player player)
	{
		if (player.fogOfWar.Count == 0)
		{
			_fogOfWar.SetFogOfWar();
			_playerFogOfWar.ChangeFogOfWar(player, player.revealAlphaLevel);
		}
		else
		{
			_fogOfWar.SetPlayersFogOfWar(player);
		}
	}
}