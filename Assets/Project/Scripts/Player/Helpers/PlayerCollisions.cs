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
		var mousePos = _playerMovement.GetMousePos(player);
		var colliders = Physics2D.OverlapCircleAll(new Vector2(mousePos.x, mousePos.y), 0.1f);

		return IsBlocked(colliders);
	}

	private bool IsBlocked(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			var parentTransform = collider.transform.parent;

			if (parentTransform.tag == "Game" || parentTransform.tag == "Block")
			{
				return true;
			}
		}

		return false;
	}
}