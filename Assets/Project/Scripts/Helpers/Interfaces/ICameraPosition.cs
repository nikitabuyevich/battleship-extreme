using UnityEngine;

public interface ICameraPosition
{
  Vector3 GetCameraPoisition(float zPos);
  bool ClickIsBoundValid(Vector3 pos);
}