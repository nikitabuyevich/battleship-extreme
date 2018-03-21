using UnityEngine;

public interface IPlayerCollisions
{
  bool CanDamage(Vector3 pos);
  bool SpaceIsBlocked(Vector3 pos);
}