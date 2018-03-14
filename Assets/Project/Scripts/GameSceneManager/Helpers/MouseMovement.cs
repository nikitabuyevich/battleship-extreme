using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class MouseMovement : MonoBehaviour
{
	private ITurn _turn;
	private IReposition _reposition;
	private IBounds _bounds;

	[Inject]
	public void Construct(ITurn turn, IReposition reposition, IBounds bounds)
	{
		_turn = turn;
		_reposition = reposition;
		_bounds = bounds;
	}

	[Header("Icon Suggestions")]
	public TileBase moveSuggestion;
	public TileBase attackSuggestion;

	public void DrawPossibleMoves()
	{
		var returnedCameraPos = _turn.CurrentPlayer().gameCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
		var mousePos = new Vector3Int(
			Mathf.FloorToInt(returnedCameraPos.x - 0.5f) + 1,
			Mathf.FloorToInt(returnedCameraPos.y - 0.5f) + 1,
			(int) _turn.CurrentPlayer().transform.position.z
		);

		var validMoves = _bounds.GetValidPositions();
		if (validMoves.Contains(mousePos))
		{
			DisplaySuggestion(mousePos, moveSuggestion);
		}
		else
		{
			ClearSuggestions();
		}
	}

	private Color GetTileColor(Vector3 pos)
	{
		var startingTileLocation = _reposition.GetStartingTileLocation();

		var location = new Vector3Int(
			startingTileLocation.x + (int) pos.x,
			startingTileLocation.y + (int) pos.y,
			startingTileLocation.z);
		var overallParent = _turn.CurrentPlayer().transform.parent.gameObject.transform.parent.gameObject;
		var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
		foreach (var tilemap in tilemaps)
		{
			if (tilemap.transform.parent.name == "Free")
			{
				return tilemap.GetColor(location);
			}
		}

		return new Color(0, 0, 0, 0);
	}

	private void ClearSuggestions()
	{
		var overallParent = _turn.CurrentPlayer().transform.parent.gameObject.transform.parent.gameObject;
		var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
		foreach (var tilemap in tilemaps)
		{
			if (tilemap.transform.parent.name == "UI")
			{
				tilemap.ClearAllTiles();
			}
		}
	}

	private void DisplaySuggestion(Vector3 pos, TileBase suggestion)
	{
		var startingTileLocation = _reposition.GetStartingTileLocation();

		var location = new Vector3Int(
			startingTileLocation.x + (int) pos.x,
			startingTileLocation.y + (int) pos.y,
			startingTileLocation.z);
		var overallParent = _turn.CurrentPlayer().transform.parent.gameObject.transform.parent.gameObject;
		var tilemaps = overallParent.GetComponentsInChildren<Tilemap>();
		foreach (var tilemap in tilemaps)
		{
			if (tilemap.transform.parent.name == "UI")
			{
				tilemap.RemoveTileFlags(
					location,
					TileFlags.LockColor
				);
				tilemap.SetTile(location, suggestion);
			}
		}
	}
}