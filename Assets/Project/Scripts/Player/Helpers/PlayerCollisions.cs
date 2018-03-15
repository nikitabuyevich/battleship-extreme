using UnityEngine;

public class PlayerCollisions : IPlayerCollisions
{
	public bool SpaceIsBlocked(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);

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