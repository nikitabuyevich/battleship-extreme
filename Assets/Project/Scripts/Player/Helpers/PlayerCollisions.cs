using UnityEngine;

public class PlayerCollisions : IPlayerCollisions
{
	public bool IsOnMoney(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);

		return IsOnMoney(colliders);
	}

	public Money GetMoneyStandingOn(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);
		var collider = GetMoneyCollider(colliders);
		if (collider != null)
		{
			return collider.GetComponent<Money>();
		}

		return null;
	}

	public bool IsGameEntity(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);

		return IsGameEntity(colliders);
	}

	public GameEntity GetGameEntity(Vector3 pos)
	{
		var colliders = Physics2D.OverlapCircleAll(new Vector2(pos.x, pos.y), 0.1f);
		var collider = GetGameEntityCollider(colliders);
		if (collider != null)
		{
			return collider.GetComponent<GameEntity>();
		}

		return null;
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

	private bool IsOnMoney(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			if (collider.tag == "Money")
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

			if (collider.tag != "Money" && (parentTransform.tag == "Game" || parentTransform.tag == "Block"))
			{
				return false;
			}
		}

		return true;
	}

	private Collider2D GetMoneyCollider(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			var moneyEntity = collider.GetComponent<Money>();
			if (moneyEntity != null)
			{
				return collider;
			}
		}

		return null;
	}

	private Collider2D GetGameEntityCollider(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			var gameEntity = collider.GetComponent<GameEntity>();
			if (gameEntity != null)
			{
				return collider;
			}
		}

		return null;
	}

	private bool IsGameEntity(Collider2D[] colliders)
	{
		foreach (var collider in colliders)
		{
			var gameEntity = collider.GetComponent<GameEntity>();
			if (gameEntity != null)
			{
				return true;
			}
		}

		return false;
	}
}