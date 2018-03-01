using UnityEngine;

public interface IFogOfWar
{
  void SetFogOfWar();
  string GetFogOfWarKey(string tilemapName, Vector3Int position);
  void SetPlayersFogOfWar(Player player);
}