using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFogOfWar : IPlayerFogOfWar
{

  private readonly IReposition _reposition;
  private readonly IGameMap _gameMap;

  public PlayerFogOfWar(IReposition reposition, IGameMap gameMap)
  {
    _reposition = reposition;
    _gameMap = gameMap;
  }

  public void RevealPlayersRefineries(Player player)
  {
    foreach (var refinery in player.refineries)
    {
      var visionPositions = _gameMap.GetAllRefineryVisionPositions(player);
      var overallParent = refinery.transform.parent.gameObject.transform.parent.gameObject;
      var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
      foreach (var tilemap in tilemaps)
      {
        foreach (var visionPosition in visionPositions)
        {
          ChangeAlphaLevelOfTile(tilemap, GetTileLocation(visionPosition), player.revealAlphaLevel);
        }
      }
    }
  }

  public void ChangeFogOfWar(Player player, float alphaLevel)
  {
    var visionPositions = _gameMap.GetVisionPositions(player);
    var overallParent = player.transform.parent.gameObject.transform.parent.gameObject;
    var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
    foreach (var tilemap in tilemaps)
    {
      foreach (var visionPosition in visionPositions)
      {
        ChangeAlphaLevelOfTile(tilemap, GetTileLocation(visionPosition), alphaLevel);
      }
    }
  }

  private Vector3Int GetTileLocation(Vector3 pos)
  {
    var startingTileLocation = _reposition.GetStartingTileLocation();

    return new Vector3Int(
      startingTileLocation.x + (int) pos.x,
      startingTileLocation.y + (int) pos.y,
      startingTileLocation.z);
  }

  private void ChangeAlphaLevelOfTile(Tilemap tilemap, Vector3Int location, float alphaLevel)
  {
    if (tilemap.tag != "Fog of War")
    {
      tilemap.RemoveTileFlags(
        location,
        TileFlags.LockColor
      );
      var tileColor = tilemap.GetColor(location);
      var revealedColor = new Color(
        tileColor.r,
        tileColor.g,
        tileColor.b,
        alphaLevel
      );
      tilemap.SetColor(location, revealedColor);
    }
  }
}