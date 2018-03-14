using UnityEngine;

public interface IBounds
{
  BoundsInt Get();
  bool ClickIsValid(Vector3 pos);
}