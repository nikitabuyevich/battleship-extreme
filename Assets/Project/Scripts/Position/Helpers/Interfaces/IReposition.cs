using UnityEngine;

public interface IReposition
{
  void Init(LevelPosition levelPosition);
  Vector3Int GetStartingTileLocation();
  Vector3 GetRepositionVector3(Vector3 pos);
}