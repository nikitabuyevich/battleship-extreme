using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerFogOfWar : IPlayerFogOfWar
{

  public void ChangeFogOfWar(Player player, float alphaLevel)
  {
    var overallParent = player.transform.parent.gameObject.transform.parent.gameObject;
    var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
    foreach (var tilemap in tilemaps)
    {
      if (player.squareVision)
      {
        for (int i = -(player.visionRadius * 2 - player.visionRadius); i < (player.visionRadius + 1); i++)
        {
          for (int j = -(player.visionRadius * 2 - player.visionRadius); j < (player.visionRadius + 1); j++)
          {
            ChangeAlphaLevelOfTile(tilemap, GetTileLocationOfPlayer(player,
              new Vector3(
                player.transform.position.x + j,
                player.transform.position.y + i,
                player.transform.position.z
              )), alphaLevel);
          }
        }
      }
      else
      {
        // Reveal PLayer tile
        ChangeAlphaLevelOfTile(tilemap, GetTileLocationOfPlayer(player, player.transform.position), alphaLevel);

        for (int i = 1; i < player.visionRadius + 1; i++)
        {
          // North
          ChangeAlphaLevelOfTile(tilemap, GetTileLocationOfPlayer(player,
            new Vector3(
              player.transform.position.x,
              player.transform.position.y + i,
              player.transform.position.z
            )), alphaLevel);

          // South
          ChangeAlphaLevelOfTile(tilemap, GetTileLocationOfPlayer(player,
            new Vector3(
              player.transform.position.x,
              player.transform.position.y - i,
              player.transform.position.z
            )), alphaLevel);

          // West
          ChangeAlphaLevelOfTile(tilemap, GetTileLocationOfPlayer(player,
            new Vector3(
              player.transform.position.x - i,
              player.transform.position.y,
              player.transform.position.z
            )), alphaLevel);

          // East
          ChangeAlphaLevelOfTile(tilemap, GetTileLocationOfPlayer(player,
            new Vector3(
              player.transform.position.x + i,
              player.transform.position.y,
              player.transform.position.z
            )), alphaLevel);
        }
      }
    }
  }

  private Vector3Int GetTileLocationOfPlayer(Player player, Vector3 pos)
  {
    var startingTileLocation = player.level.GetComponent<LevelPosition>().GetStartingTileLocation();

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