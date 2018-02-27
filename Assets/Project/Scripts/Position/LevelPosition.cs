using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class LevelPosition : MonoBehaviour
{

	private Vector3 leftMostCorner;

	private IPlayerFogOfWar _playerFogOfWar;

	[Inject]
	public void Construct(IPlayerFogOfWar playerFogOfWar)
	{
		_playerFogOfWar = playerFogOfWar;
	}

	// Use this for initialization
	void Start()
	{
		transform.position = GetRepositionVector3(transform.position);

		// reveal fog of starting location
		var players = transform.parent.GetComponentsInChildren<Player>();
		foreach (var player in players)
		{
			// move player to starting pos
			player.transform.position = new Vector3(player.startingX, player.startingY, transform.position.z);

			if (player.isAllowedToMove)
			{
				_playerFogOfWar.ChangeFogOfWar(player, player.revealAlphaLevel);
			}
		}
	}

	public Vector3Int GetStartingTileLocation()
	{
		var startingFloorLocation = GetRepositionVector3(transform.position);
		return new Vector3Int(
			(-1 * (int) (startingFloorLocation.x + 0.5f)),
			(-1 * (int) (startingFloorLocation.y + 0.5f)),
			(int) startingFloorLocation.z
		);
	}

	public Vector3 GetRepositionVector3(Vector3 pos)
	{
		var children = GetComponentsInChildren<Transform>();
		foreach (var child in children)
		{
			if (child.transform.name == "Base")
			{
				leftMostCorner = child.GetComponent<Tilemap>().origin;
			}
		}

		// Subtract by 1.5 units to position objects in bottom left corner
		var moveUnits = 1.5f;

		return new Vector3(
			Mathf.Abs(leftMostCorner.x + moveUnits),
			Mathf.Abs(leftMostCorner.y + moveUnits),
			pos.z
		);
	}
}