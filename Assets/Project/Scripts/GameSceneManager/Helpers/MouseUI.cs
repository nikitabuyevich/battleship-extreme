using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class MouseUI : MonoBehaviour
{
	private ITurn _turn;
	private IReposition _reposition;
	private IPlayerMovement _playerMovement;
	private IPlayerCollisions _playerCollisions;
	private IBounds _bounds;

	[Inject]
	public void Construct(
		ITurn turn,
		IReposition reposition,
		IBounds bounds,
		IPlayerCollisions playerCollisions)
	{
		_turn = turn;
		_reposition = reposition;
		_bounds = bounds;
		_playerCollisions = playerCollisions;
	}

	[Header("Icon Suggestions")]
	public TileBase canMoveHere;
	public TileBase thinkingAboutMovingHere;
	public TileBase attackSuggestion;

	public Texture2D moveCursor;
	public Texture2D attackCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 moveCursorHotSpot = Vector2.zero;
	public Vector2 attackCursorHotSpot = Vector2.zero;

	private Vector3Int lastMousePos;

	public void DrawPossibleMoves()
	{
		var validPositions = _bounds.GetValidPositions();
		foreach (var validPosition in validPositions)
		{
			if (!_playerCollisions.SpaceIsBlocked(_turn.CurrentPlayer()))
			{
				PlaceTile(validPosition, canMoveHere, "Move Suggestions");
			}
		}
	}

	public void DrawSuggestionOverMouse()
	{
		var returnedCameraPos = _turn.CurrentPlayer().gameCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
		var mousePos = new Vector3Int(
			Mathf.FloorToInt(returnedCameraPos.x - 0.5f) + 1,
			Mathf.FloorToInt(returnedCameraPos.y - 0.5f) + 1,
			(int) _turn.CurrentPlayer().transform.position.z
		);

		if (lastMousePos != mousePos)
		{
			if (_bounds.ClickIsValid(mousePos) && !_playerCollisions.SpaceIsBlocked(_turn.CurrentPlayer()))
			{
				ClearMouseUI();
				Cursor.SetCursor(moveCursor, moveCursorHotSpot, cursorMode);
				PlaceTile(mousePos, thinkingAboutMovingHere, "Mouse UI");
			}
			else
			{
				ClearMouseUI();
			}

			lastMousePos = mousePos;
		}
	}

	public void Clear()
	{
		ClearMouseUI();
		GameObject.Find("Move Suggestions").GetComponent<Tilemap>().ClearAllTiles();
	}

	public void ClearMouseUI()
	{
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
		GameObject.Find("Mouse UI").GetComponent<Tilemap>().ClearAllTiles();
	}

	private TileBase GetTileAtPos(Vector3 pos)
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
				return tilemap.GetTile(location);
			}
		}

		return null;
	}

	private void ColorTile(Vector3 pos, Color color)
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
			if (tilemap.name == "Mouse UI")
			{
				tilemap.RemoveTileFlags(
					location,
					TileFlags.LockColor
				);
				tilemap.SetColor(location, color);
			}
		}
	}

	private void PlaceTile(Vector3 pos, TileBase tile, string tilemapName)
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
			if (tilemap.name == tilemapName)
			{
				tilemap.RemoveTileFlags(
					location,
					TileFlags.LockColor
				);
				tilemap.SetTile(location, tile);
			}
		}
	}
}