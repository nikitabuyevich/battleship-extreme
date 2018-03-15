using System.Collections.Generic;
using UnityEngine;

public interface IGameMap
{
  BoundsInt Get();
  bool MoveIsValid(Player player, Vector3 pos);
  List<Vector3> GetValidPositions(Player player);
}