using System.Collections.Generic;
using UnityEngine;

public interface IBounds
{
  BoundsInt Get();
  bool ClickIsValid(Vector3 pos);
  List<Vector3> GetValidPositions();
}