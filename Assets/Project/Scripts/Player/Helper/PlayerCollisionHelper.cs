using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHelper : MonoBehaviour
{

	public bool SpaceIsNotBlocked(Player player)
	{
		if (player._input != Vector2.zero)
		{
			var direction = player._movementHelper.GetDirection(player._input);

			if (direction == Direction.North)
			{
				var northBlocked = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.up, 1f);
				if (northBlocked.collider != null && (SpaceIsBlocked(northBlocked.collider.gameObject.GetComponentsInParent<Transform>())))
				{
					return false;
				}
			}
			else if (direction == Direction.South)
			{
				var southBlocked = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.down, 1f);
				if (southBlocked.collider != null && (SpaceIsBlocked(southBlocked.collider.gameObject.GetComponentsInParent<Transform>())))
				{
					return false;
				}
			}
			else if (direction == Direction.West)
			{
				var westBlocked = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.left, 1f);
				if (westBlocked.collider != null && (SpaceIsBlocked(westBlocked.collider.gameObject.GetComponentsInParent<Transform>())))
				{
					return false;
				}
			}
			// otherwise check East
			else
			{
				var eastBlocked = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.right, 1f);
				if (eastBlocked.collider != null && (SpaceIsBlocked(eastBlocked.collider.gameObject.GetComponentsInParent<Transform>())))
				{
					return false;
				}
			}

			return true;
		}

		return false;
	}

	private bool SpaceIsBlocked(Transform[] transforms)
	{
		foreach (var transform in transforms)
		{
			if (transform.tag == "Block")
			{
				return true;
			}
		}

		return false;
	}
}