using UnityEngine;
using UnityEngine.Tilemaps;

public class Mouse : IMouse
{
  private readonly IBounds _bounds;
  private readonly IReposition _reposition;

  public Mouse(
    IBounds bounds,
    IReposition reposition)
  {
    _bounds = bounds;
    _reposition = reposition;
  }

  public Vector3 GetMousePos(Player player)
  {
    var returnedCameraPos = player.gameCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    return new Vector3Int(
      Mathf.FloorToInt(returnedCameraPos.x - 0.5f) + 1,
      Mathf.FloorToInt(returnedCameraPos.y - 0.5f) + 1,
      (int) player.transform.position.z);
  }

  public void DrawPossibleMoves(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();
    var validPositions = _bounds.GetValidPositions(player);
    foreach (var validPosition in validPositions)
    {
      PlaceTile(player, validPosition, mouseUI.canMoveHere, "Move Suggestions");
    }
  }

  public void DrawSuggestionOverMouse(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();
    var returnedCameraPos = player.gameCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    var mousePos = new Vector3Int(
      Mathf.FloorToInt(returnedCameraPos.x - 0.5f) + 1,
      Mathf.FloorToInt(returnedCameraPos.y - 0.5f) + 1,
      (int) player.transform.position.z
    );

    if (mouseUI.lastMousePos != mousePos)
    {
      var validPositions = _bounds.GetValidPositions(player);
      if (validPositions.Contains(mousePos))
      {
        ClearMouseUI(mouseUI);
        Cursor.SetCursor(mouseUI.moveCursor, mouseUI.moveCursorHotSpot, mouseUI.cursorMode);
        PlaceTile(player, mousePos, mouseUI.thinkingAboutMovingHere, "Mouse UI");
      }
      else
      {
        ClearMouseUI(mouseUI);
      }

      mouseUI.lastMousePos = mousePos;
    }
  }

  public void Clear(MouseUI mouseUI)
  {
    ClearMouseUI(mouseUI);
    GameObject.Find("Move Suggestions").GetComponent<Tilemap>().ClearAllTiles();
  }

  public void ClearMouseUI(MouseUI mouseUI)
  {
    Cursor.SetCursor(null, Vector2.zero, mouseUI.cursorMode);
    GameObject.Find("Mouse UI").GetComponent<Tilemap>().ClearAllTiles();
  }

  private TileBase GetTileAtPos(Player player, Vector3 pos)
  {
    var startingTileLocation = _reposition.GetStartingTileLocation();

    var location = new Vector3Int(
      startingTileLocation.x + (int) pos.x,
      startingTileLocation.y + (int) pos.y,
      startingTileLocation.z);
    var overallParent = player.transform.parent.gameObject.transform.parent.gameObject;
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

  private void ColorTile(Player player, Vector3 pos, Color color)
  {
    var startingTileLocation = _reposition.GetStartingTileLocation();

    var location = new Vector3Int(
      startingTileLocation.x + (int) pos.x,
      startingTileLocation.y + (int) pos.y,
      startingTileLocation.z);

    var overallParent = player.transform.parent.gameObject.transform.parent.gameObject;
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

  private void PlaceTile(Player player, Vector3 pos, TileBase tile, string tilemapName)
  {
    var startingTileLocation = _reposition.GetStartingTileLocation();

    var location = new Vector3Int(
      startingTileLocation.x + (int) pos.x,
      startingTileLocation.y + (int) pos.y,
      startingTileLocation.z);
    var overallParent = player.transform.parent.gameObject.transform.parent.gameObject;
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