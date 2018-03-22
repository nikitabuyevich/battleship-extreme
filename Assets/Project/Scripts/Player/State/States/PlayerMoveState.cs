using UnityEngine;
using Zenject;

public class PlayerMoveState : IPlayerMoveState
{
	[Inject]
	private readonly GameSceneManager _gameSceneManager;

	private readonly IPlayerFogOfWar _playerFogOfWar;
	private readonly IFogOfWar _fogOfWar;
	private readonly IMouse _mouse;
	private readonly IAbility _ability;

	private bool drewMouseSuggestions = false;

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
		var mouseUI = player.mouseUI.GetComponent<MouseUI>();
		_mouse.Clear(mouseUI);
		drewMouseSuggestions = false;
	}

	public void Execute(Player player)
	{
		if (!drewMouseSuggestions && _gameSceneManager.numberOfMoves > 0)
		{
			_mouse.DrawMoveSuggestions(player);
			drewMouseSuggestions = true;
		}

		if (_gameSceneManager.numberOfMoves > 0)
		{
			player.isAbleToMove = true;
		}

		if (player.isAbleToMove)
		{
			player.Move();
			_mouse.DrawMoveSuggestionsHover(player);
		}
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