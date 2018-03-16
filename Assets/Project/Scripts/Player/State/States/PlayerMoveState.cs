using UnityEngine;

public class PlayerMoveState : IPlayerMoveState
{
	private readonly IPlayerFogOfWar _playerFogOfWar;
	private readonly IFogOfWar _fogOfWar;
	private readonly IMouse _mouse;
	private readonly IAbility _ability;

	public PlayerMoveState(
		IFogOfWar fogOfWar,
		IPlayerFogOfWar playerFogOfWar,
		IMouse mouse,
		IAbility ability)
	{
		_fogOfWar = fogOfWar;
		_playerFogOfWar = playerFogOfWar;
		_mouse = mouse;
		_ability = ability;
	}

	public void Enter(Player player)
	{
		Debug.Log(player.name + " entering move state");
		LoadPlayersFogOfWar(player);
		_mouse.DrawPossibleMoves(player);
	}

	public void Execute(Player player)
	{
		player.Move();
		_mouse.DrawSuggestionOverMouse(player);
	}

	public void Exit(Player player) { }

	public void AbilityRotate(Player player)
	{
		_ability.Rotate(player, false);
	}

	public void AbilityAttack(Player player) { }

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