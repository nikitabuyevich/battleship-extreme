using UnityEngine;

public interface IPlayerCollisions
{
  Collider2D GetHit(Vector3 pos);
  bool CanDamage(Vector3 pos);
  bool SpaceIsBlocked(Vector3 pos);
}