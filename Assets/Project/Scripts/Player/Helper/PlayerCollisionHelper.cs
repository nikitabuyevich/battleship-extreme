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
				if (northBlocked.collider != null && (northBlocked.collider.gameObject.tag == "Block" || northBlocked.collider.gameObject.tag == "Player"))
				{
					return false;
				}
			}
			else if (direction == Direction.South)
			{
				var southBlocked = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.down, 1f);
				if (southBlocked.collider != null && (southBlocked.collider.gameObject.tag == "Block" || southBlocked.collider.gameObject.tag == "Player"))
				{
					return false;
				}
			}
			else if (direction == Direction.West)
			{
				var westBlocked = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.left, 1f);
				if (westBlocked.collider != null && (westBlocked.collider.gameObject.tag == "Block" || westBlocked.collider.gameObject.tag == "Player"))
				{
					return false;
				}
			}
			// otherwise check East
			else
			{
				var eastBlocked = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y), Vector2.right, 1f);
				if (eastBlocked.collider != null && (eastBlocked.collider.gameObject.tag == "Block" || eastBlocked.collider.gameObject.tag == "Player"))
				{
					return false;
				}
			}

			return true;
		}

		return false;
	}
}