using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : IPlayerSpriteRenderer
{

	readonly private IPlayerMovement _playerMovement;

	public PlayerSpriteRenderer(IPlayerMovement playerMovement)
	{
		_playerMovement = playerMovement;
	}

	public void RenderDirection(Player player)
	{
		var currentDir = _playerMovement.GetDirection(player._input);

		switch (currentDir)
		{
			case Direction.North:
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.northSprite;
				break;
			case Direction.East:
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.eastSprite;
				break;
			case Direction.South:
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.southSprite;
				break;
			case Direction.West:
				player.gameObject.GetComponent<SpriteRenderer>().sprite = player.westSprite;
				break;
		}
	}
}