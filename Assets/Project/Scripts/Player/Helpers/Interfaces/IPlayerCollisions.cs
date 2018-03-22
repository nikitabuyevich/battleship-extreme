using UnityEngine;

public interface IPlayerCollisions
{
  GameEntity GetGameEntity(Vector3 pos);
  bool IsGameEntity(Vector3 pos);
  bool CanMoveToSpace(Vector3 pos);
  bool SpaceIsBlocked(Vector3 pos);
}