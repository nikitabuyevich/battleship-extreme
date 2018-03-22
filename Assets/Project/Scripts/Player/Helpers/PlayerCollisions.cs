﻿using UnityEngine;

public class PlayerCollisions : IPlayerCollisions
{
	public bool CanDamage(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);

		return CanDamage(colliders);
	}

	public Collider2D GetHit(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);
		return GetHit(colliders);
	}

	public bool SpaceIsBlocked(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);

		return SpaceIsBlocked(colliders);
	}

	public bool CanMoveToSpace(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);

		return CanMove(colliders);
	}

	private bool SpaceIsBlocked(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			var parentTransform = collider.transform.parent;

			if (parentTransform.tag == "Block")
			{
				return true;
			}
		}

		return false;
	}

	private bool CanMove(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			var parentTransform = collider.transform.parent;

			if (parentTransform.tag == "Game" || parentTransform.tag == "Block")
			{
				return false;
			}
		}

		return true;
	}

	private Collider2D GetHit(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			if (collider.tag == "Can Damage")
			{
				return collider;
			}
		}

		return null;
	}
	private bool CanDamage(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			if (collider.tag == "Can Damage")
			{
				return true;
			}
		}

		return false;
	}
}