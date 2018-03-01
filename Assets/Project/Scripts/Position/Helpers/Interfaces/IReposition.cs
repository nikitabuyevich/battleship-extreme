using UnityEngine;

public interface IReposition
{
  Vector3Int GetStartingTileLocation();
  Vector3 GetRepositionVector3(Vector3 pos);
}