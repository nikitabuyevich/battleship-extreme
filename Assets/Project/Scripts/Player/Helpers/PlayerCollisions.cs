using UnityEngine;

public class PlayerCollisions : IPlayerCollisions
{
	readonly private IPlayerMovement _playerMovement;

	public PlayerCollisions(IPlayerMovement playerMovement)
	{
		_playerMovement = playerMovement;
	}

	public bool SpaceIsBlocked(Player player)
	{
		var direction = _playerMovement.GetDirection(player);

		if (direction == Direction.North)
		{
			var northCollisions = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.up, 1f);
			return IsBlocked(northCollisions);
		}
		else if (direction == Direction.South)
		{
			var southCollisions = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.down, 1f);
			return IsBlocked(southCollisions);
		}
		else if (direction == Direction.West)
		{
			var westCollisions = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.left, 1f);
			return IsBlocked(westCollisions);
		}
		// otherwise check East
		else
		{
			var eastCollisions = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.right, 1f);
			return IsBlocked(eastCollisions);
		}
	}

	private bool IsBlocked(RaycastHit2D[] collisions)
	{
		foreach (var collision in collisions)
		{
			var parentTransform = collision.transform.parent;

			if (parentTransform.tag == "Game" || parentTransform.tag == "Block")
			{
				return true;
			}
		}

		return false;
	}
}