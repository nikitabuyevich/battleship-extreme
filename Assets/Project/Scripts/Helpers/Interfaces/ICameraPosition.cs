using UnityEngine;

public interface ICameraPosition
{
  Vector3 GetCameraPoisition(float zPos);
  Vector3 GetGameBounds(Vector3 pos);
}