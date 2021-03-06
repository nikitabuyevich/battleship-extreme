using System.Collections.Generic;
using UnityEngine;

public interface IGameMap
{
  BoundsInt Get();
  bool MoveIsValid(Player player, Vector3 pos);
  void CheckAndHideGameEntities(Player player);
  List<Vector3> GetAllRefineryVisionPositions(Player player);
  List<Vector3> GetValidAttackPositions(Player player);
  List<Vector3> GetBuildPositions(Player player);
  List<Vector3> GetValidMovePositions(Player player);
  List<Vector3> GetValidSideAttackPositions(Player player, Vector3 mousePos);
  List<Vector3> GetVisionPositions(Player player);
}