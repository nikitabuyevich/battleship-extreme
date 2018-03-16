using UnityEngine;

public class PlayerSpriteRenderer : IPlayerSpriteRenderer
{
	public Direction GetDirection(Player player)
	{
		var playerRenderer = player.GetComponentInChildren<SpriteRenderer>();
		var directionSprite = playerRenderer.sprite;

		if (directionSprite == player.northSprite)
		{
			return Direction.North;
		}
		else if (directionSprite == player.southSprite)
		{
			return Direction.South;
		}
		else if (directionSprite == player.westSprite)
		{
			return Direction.West;
		}

		return Direction.East;
	}

	public void RenderDirection(Player player, Direction direction)
	{
		SpriteRenderer playerComponent;

		switch (direction)
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