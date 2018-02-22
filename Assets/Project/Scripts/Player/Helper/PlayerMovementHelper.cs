using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
	North,
	East,
	South,
	West
}

public class PlayerMovementHelper : MonoBehaviour
{
	public void GetInput(Player player)
	{
		player._input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (Mathf.Abs(player._input.x) > Mathf.Abs(player._input.y))
		{
			player._input.y = 0;
		}
		else
		{
			player._input.x = 0;
		}
	}

	public Direction GetDirection(Vector2 input)
	{
		if (input.x < 0)
		{
			return Direction.West;
		}

		else if (input.x > 0)
		{
			return Direction.East;
		}

		else if (input.y < 0)
		{
			return Direction.South;
		}

		// Otherwise moving North
		return Direction.North;
	}

	public bool PlayerCanMoveThere(Player player)
	{
		if (player._input != Vector2.zero)
		{
			var direction = GetDirection(player._input);

			if (direction == Direction.West)
			{
				if ((player.transform.position.x - 1) < 0)
				{
					return false;
				}
			}

			else if (direction == Direction.East)
			{
				if ((player.transform.position.x + 1) > (player.gridWidth - 1))
				{
					return false;
				}
			}

			else if (direction == Direction.South)
			{
				if ((player.transform.position.y - 1) < 0)
				{
					return false;
				}
			}

			else if (direction == Direction.North)
			{
				if ((player.transform.position.y + 1) > (player.gridHeight - 1))
				{
					return false;
				}
			}

			return true;
		}

		return false;
	}

	public IEnumerator Move(Player player)
	{
		player._isMoving = true;
		var startPos = player.transform.position;
		var t = 0f;

		var endPos = new Vector3(startPos.x + System.Math.Sign(player._input.x), startPos.y + System.Math.Sign(player._input.y), startPos.z);

		while (player.transform.position != endPos)
		{
			t += Time.deltaTime * player.moveSpeed;
			player.transform.position = Vector3.Lerp(startPos, endPos, t);
			yield return null;
		}

		player._isMoving = false;
		yield return 0;
	}
}