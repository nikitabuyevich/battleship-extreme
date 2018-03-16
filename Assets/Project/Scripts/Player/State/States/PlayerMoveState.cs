using UnityEngine;

public class PlayerMoveState : IPlayerMoveState
{
	private readonly IPlayerFogOfWar _playerFogOfWar;
	private readonly IFogOfWar _fogOfWar;
	private readonly IMouse _mouse;
	private readonly IPlayerSpriteRenderer _playerSpriteRenderer;

	public PlayerMoveState(
		IFogOfWar fogOfWar,
		IPlayerFogOfWar playerFogOfWar,
		IMouse mouse,
		IPlayerSpriteRenderer playerSpriteRenderer)
	{
		_fogOfWar = fogOfWar;
		_playerFogOfWar = playerFogOfWar;
		_mouse = mouse;
		_playerSpriteRenderer = playerSpriteRenderer;
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

	public void AbilityRotate(Player player)
	{
		var playersCurrentDir = _playerSpriteRenderer.GetDirection(player);

		// Render new direction
		if (playersCurrentDir == Direction.West)
		{
			_playerSpriteRenderer.RenderDirection(player, Direction.North);
		}
		else if (playersCurrentDir == Direction.North)
		{
			_playerSpriteRenderer.RenderDirection(player, Direction.East);
		}
		else if (playersCurrentDir == Direction.East)
		{
			_playerSpriteRenderer.RenderDirection(player, Direction.South);
		}
		else
		{
			_playerSpriteRenderer.RenderDirection(player, Direction.West);
		}

		player.UseMoveTurn();
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