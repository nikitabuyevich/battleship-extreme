using UnityEngine;

public class PlayerMoveState : IPlayerMoveState
{
	private readonly IPlayerFogOfWar _playerFogOfWar;
	private readonly IFogOfWar _fogOfWar;
	private readonly IMouse _mouse;

	public PlayerMoveState(
		IFogOfWar fogOfWar,
		IPlayerFogOfWar playerFogOfWar,
		IMouse mouse)
	{
		_fogOfWar = fogOfWar;
		_playerFogOfWar = playerFogOfWar;
		_mouse = mouse;
	}

	public void Enter(Player player)
	{
		LoadPlayersFogOfWar(player);
		_mouse.DrawPossibleMoves(player);
	}

	public void Execute(Player player)
	{
		player.Move();
		_mouse.DrawSuggestionOverMouse(player);
	}

	public void Exit(Player player) { }

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