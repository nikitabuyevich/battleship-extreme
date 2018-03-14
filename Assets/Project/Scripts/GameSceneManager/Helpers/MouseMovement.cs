using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseMovement : IMouseMovement
{

	private readonly ITurn _turn;
	private readonly IReposition _reposition;

	public MouseMovement(ITurn turn, IReposition reposition)
	{
		_turn = turn;
		_reposition = reposition;
	}

	public void CanMove()
	{
		var returnedCameraPos = _turn.CurrentPlayer().gameCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
		var mousePos = new Vector3Int(
			Mathf.FloorToInt(returnedCameraPos.x - 0.5f) + 1,
			Mathf.FloorToInt(returnedCameraPos.y - 0.5f) + 1,
			(int) _turn.CurrentPlayer().transform.position.z
		);

		var startingTileLocation = _reposition.GetStartingTileLocation();

		var location = new Vector3Int(
			startingTileLocation.x + (int) mousePos.x,
			startingTileLocation.y + (int) mousePos.y,
			startingTileLocation.z);
		var overallParent = _turn.CurrentPlayer().transform.parent.gameObject.transform.parent.gameObject;
		var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
		foreach (var tilemap in tilemaps)
		{

			if (tilemap.transform.parent.name == "Free")
			{
				tilemap.RemoveTileFlags(
					location,
					TileFlags.LockColor
				);
				var tileColor = tilemap.GetColor(location);
				var newColor = Color.red;
				tilemap.SetColor(location, newColor);
			}
		}
	}
}