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
		SpriteRenderer playerComponent;

		switch (currentDir)
		{
			case Direction.North:
				playerComponent = player.GetComponentInChildren<SpriteRenderer>();
				playerComponent.sprite = player.northSprite;
				playerComponent.transform.position = new Vector3(
					player.transform.position.x,
					player.transform.position.y,
					player.transform.position.z
				);
				break;
			case Direction.East:
				playerComponent = player.GetComponentInChildren<SpriteRenderer>();
				playerComponent.sprite = player.eastSprite;
				playerComponent.transform.position = new Vector3(
					player.transform.position.x + player.eastWestOffsetX,
					player.transform.position.y + player.eastWestOffsetY,
					player.transform.position.z
				);
				break;
			case Direction.South:
				playerComponent = player.GetComponentInChildren<SpriteRenderer>();
				playerComponent.sprite = player.southSprite;
				playerComponent.transform.position = new Vector3(
					player.transform.position.x,
					player.transform.position.y,
					player.transform.position.z
				);
				break;
			case Direction.West:
				playerComponent = player.GetComponentInChildren<SpriteRenderer>();
				playerComponent.sprite = player.westSprite;
				playerComponent.transform.position = new Vector3(
					player.transform.position.x - player.eastWestOffsetX,
					player.transform.position.y + player.eastWestOffsetY,
					player.transform.position.z
				);
				break;
		}
	}
}