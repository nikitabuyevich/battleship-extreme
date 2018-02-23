using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction
{
	North,
	East,
	South,
	West
}

public class PlayerMovementHelper : MonoBehaviour
{
	public void GetInput(Player player)
	{
		player._input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (Mathf.Abs(player._input.x) > Mathf.Abs(player._input.y))
		{
			player._input.y = 0;
		}
		else
		{
			player._input.x = 0;
		}
	}

	public Direction GetDirection(Vector2 input)
	{
		if (input.x < 0)
		{
			return Direction.West;
		}

		else if (input.x > 0)
		{
			return Direction.East;
		}

		else if (input.y < 0)
		{
			return Direction.South;
		}

		// Otherwise moving North
		return Direction.North;
	}

	public IEnumerator Move(Player player)
	{
		ChangeFogOfWar(player, player.visitedAlphaLevel);
		player._isMoving = true;
		var startPos = player.transform.position;
		var t = 0f;

		var endPos = new Vector3(startPos.x + System.Math.Sign(player._input.x), startPos.y + System.Math.Sign(player._input.y), startPos.z);

		while (player.transform.position != endPos)
		{
			t += Time.deltaTime * player.moveSpeed;
			player.transform.position = Vector3.Lerp(startPos, endPos, t);
			yield return null;
		}

		ChangeFogOfWar(player, player.revealAlphaLevel);
		player._isMoving = false;
		yield return 0;
	}

	private Vector3Int GetTileLocationOfPlayer(Player player, Tilemap tilemap, Vector3 pos)
	{
		var startingTileLocation = player.level.GetComponent<LevelPosition>().GetStartingTileLocation();

		return new Vector3Int(
			startingTileLocation.x + (int) pos.x,
			startingTileLocation.y + (int) pos.y,
			startingTileLocation.z);
	}

	public void ChangeFogOfWar(Player player, float alphaLevel)
	{
		var overallParent = player.transform.parent.gameObject;
		var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();

		foreach (var tilemap in tilemaps)
		{
			// Reveal PLayer tile
			ChangeAlphaLevelOfTile(player, tilemap, GetTileLocationOfPlayer(player, tilemap, player.transform.position), alphaLevel);

			for (int i = 1; i < player.visionRadius + 1; i++)
			{
				// North
				ChangeAlphaLevelOfTile(player, tilemap, GetTileLocationOfPlayer(player, tilemap,
					new Vector3(
						player.transform.position.x,
						player.transform.position.y + i,
						player.transform.position.z
					)), alphaLevel);

				// South
				ChangeAlphaLevelOfTile(player, tilemap, GetTileLocationOfPlayer(player, tilemap,
					new Vector3(
						player.transform.position.x,
						player.transform.position.y - i,
						player.transform.position.z
					)), alphaLevel);

				// West
				ChangeAlphaLevelOfTile(player, tilemap, GetTileLocationOfPlayer(player, tilemap,
					new Vector3(
						player.transform.position.x - i,
						player.transform.position.y,
						player.transform.position.z
					)), alphaLevel);

				// East
				ChangeAlphaLevelOfTile(player, tilemap, GetTileLocationOfPlayer(player, tilemap,
					new Vector3(
						player.transform.position.x + i,
						player.transform.position.y,
						player.transform.position.z
					)), alphaLevel);
			}
		}

	}

	private void ChangeAlphaLevelOfTile(Player player, Tilemap tilemap, Vector3Int location, float alphaLevel)
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