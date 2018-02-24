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

  // public IEnumerator FadeTiles(Player player)
  // {
  //   var startPos = player.transform.position;
  //   var t = 0f;

  //   var endPos = new Vector3(startPos.x + System.Math.Sign(player._input.x), startPos.y + System.Math.Sign(player._input.y), startPos.z);

  //   var currentAlphaLevel = player.revealAlphaLevel;
  //   var endingAlphaLevel = player.visitedAlphaLevel;

  //   while (player.transform.position != endPos)
  //   {
  //     currentAlphaLevel -= Time.deltaTime * 0.5f;
  //     if (currentAlphaLevel > endingAlphaLevel)
  //     {
  //       _playerFogOfWar.LeavingFogOfWar(player, currentAlphaLevel);
  //     }
  //     t += Time.deltaTime * player.moveSpeed;
  //     player.transform.position = Vector3.Lerp(startPos, endPos, t);
  //     yield return null;
  //   }

  //   // _playerFogOfWar.ChangeFogOfWar(player, player.revealAlphaLevel);
  //   player._isMoving = false;
  //   yield return 0;
  // }
}