using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class FogOfWar : IFogOfWar
{
  [Inject]
  private readonly GameSceneManager _game;

  public string GetFogOfWarKey(string tilemapName, Vector3Int position)
  {
    return string.Format("{0}_{1}", tilemapName, position);
  }

  public void SetFogOfWar()
  {
    // Set tilemaps to 50% transparency 
    var tilemaps = GameObject.Find("Floor").GetComponentsInChildren<Tilemap>();
    foreach (var tilemap in tilemaps)
    {
      if (tilemap.tag != "Fog of War")
      {
        BoundsInt bounds = tilemap.cellBounds;
        var tilePosition = bounds.allPositionsWithin;
        while (tilePosition.MoveNext())
        {
          tilemap.RemoveTileFlags(
            tilePosition.Current,
            TileFlags.LockColor
          );

          // set black background for fog of war tile
          var fogOfWarTile = FindTilemap(tilemaps, "Fog of War");
          fogOfWarTile.SetTile(tilePosition.Current, _game.blackTile);

          tilemap.RemoveTileFlags(tilePosition.Current, TileFlags.LockColor);
          var tileColor = tilemap.GetColor(tilePosition.Current);
          var revealedColor = new Color(
            tileColor.r,
            tileColor.g,
            tileColor.b,
            _game.fogOfWarAlphaLevel
          );
          tilemap.SetColor(tilePosition.Current, revealedColor);
        }
      }
    }
  }

  public void SetPlayersFogOfWar(Player player)
  {
    var overallParent = GameObject.FindGameObjectWithTag("Overall Parent");
    var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
    foreach (var tilemap in tilemaps)
    {
      BoundsInt bounds = tilemap.cellBounds;
      var tilePosition = bounds.allPositionsWithin;
      while (tilePosition.MoveNext())
      {
        tilemap.SetColor(tilePosition.Current,
          player.fogOfWar[GetFogOfWarKey(tilemap.name, tilePosition.Current)]);
      }
    }
  }

  private Tilemap FindTilemap(Tilemap[] tilemaps, string tag)
  {
    foreach (var tilemap in tilemaps)
    {
      if (tilemap.tag == tag)
      {
        return tilemap;
      }
    }

    return null;
  }
}