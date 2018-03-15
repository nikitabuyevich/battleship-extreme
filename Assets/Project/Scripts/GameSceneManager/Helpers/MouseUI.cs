using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class MouseUI : MonoBehaviour
{
	[Header("Icon Suggestions")]
	public TileBase canMoveHere;
	public TileBase thinkingAboutMovingHere;
	public TileBase attackSuggestion;

	public Texture2D moveCursor;
	public Texture2D attackCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 moveCursorHotSpot = Vector2.zero;
	public Vector2 attackCursorHotSpot = Vector2.zero;

	internal Vector3Int lastMousePos;
}