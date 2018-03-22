using UnityEngine;
using UnityEngine.Tilemaps;

public class Mouse : IMouse
{
  private readonly IGameMap _gameMap;
  private readonly IReposition _reposition;

  public Mouse(
    IGameMap gameMap,
    IReposition reposition
  )
  {
    _gameMap = gameMap;
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

  public void SetAttackCursor(MouseUI mouseUI)
  {
    Cursor.SetCursor(mouseUI.attackCursor, mouseUI.attackCursorHotSpot, mouseUI.cursorMode);
  }

  public void DrawAttackSuggestions(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();
    Clear(mouseUI);
    var validPositions = _gameMap.GetValidAttackPositions(player);
    foreach (var validPosition in validPositions)
    {
      PlaceTile(player, validPosition, mouseUI.canAttackHere, "Suggestions");
    }

    SetAttackCursor(mouseUI);
  }

  public void DrawMoveSuggestions(Player player)
  {
    var mouseUI = player.mouseUI.GetComponent<MouseUI>();
    Clear(mouseUI);
    var validPositions = _gameMap.GetValidMovePositions(player);
    foreach (var validPosition in validPositions)
    {
      PlaceTile(player, validPosition, mouseUI.canMoveHere, "Suggestions");
    }
  }

  public void DrawMoveSuggestionsHover(Player player)
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
      var validPositions = _gameMap.GetValidMovePositions(player);
      if (validPositions.Contains(mousePos))
      {
        ClearMouseUI(mouseUI, true);
        Cursor.SetCursor(mouseUI.moveCursor, mouseUI.moveCursorHotSpot, mouseUI.cursorMode);
        PlaceTile(player, mousePos, mouseUI.thinkingAboutMovingHere, "Mouse UI");
      }
      else
      {
        ClearMouseUI(mouseUI, true);
      }

      mouseUI.lastMousePos = mousePos;
    }
  }

  public void DrawAttackSuggestionsHover(Player player)
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
      var validPositions = _gameMap.GetValidAttackPositions(player);
      if (validPositions.Contains(mousePos))
      {
        ClearMouseUI(mouseUI, false);
        Cursor.SetCursor(mouseUI.attackCursor, mouseUI.attackCursorHotSpot, mouseUI.cursorMode);
        PlaceTile(player, mousePos, mouseUI.thinkingAboutAttackingHere, "Mouse UI");
        DrawSideAttackSuggestionsHover(player, mousePos, mouseUI);
      }
      else
      {
        ClearMouseUI(mouseUI, false);
      }

      mouseUI.lastMousePos = mousePos;
    }
  }

  private void DrawSideAttackSuggestionsHover(Player player, Vector3 mousePos, MouseUI mouseUI)
  {
    // West
    if (player.transform.position.x - mousePos.x > 0)
    {
      var sideLeft = new Vector3(
        mousePos.x - 1,
        mousePos.y - 1,
        mousePos.z
      );
      var sideRight = new Vector3(
        mousePos.x - 1,
        mousePos.y + 1,
        mousePos.z
      );
      PlaceTile(player, sideLeft, mouseUI.thinkingAboutAttackingHereSideAttacks, "Mouse UI");
      PlaceTile(player, sideRight, mouseUI.thinkingAboutAttackingHereSideAttacks, "Mouse UI");
    }
    // East
    else if (player.transform.position.x - mousePos.x < 0)
    {
      var sideLeft = new Vector3(
        mousePos.x + 1,
        mousePos.y - 1,
        mousePos.z
      );
      var sideRight = new Vector3(
        mousePos.x + 1,
        mousePos.y + 1,
        mousePos.z
      );
      PlaceTile(player, sideLeft, mouseUI.thinkingAboutAttackingHereSideAttacks, "Mouse UI");
      PlaceTile(player, sideRight, mouseUI.thinkingAboutAttackingHereSideAttacks, "Mouse UI");
    }
    // North
    else if (player.transform.position.y - mousePos.y > 0)
    {
      var sideLeft = new Vector3(
        mousePos.x + 1,
        mousePos.y - 1,
        mousePos.z
      );
      var sideRight = new Vector3(
        mousePos.x - 1,
        mousePos.y - 1,
        mousePos.z
      );
      PlaceTile(player, sideLeft, mouseUI.thinkingAboutAttackingHereSideAttacks, "Mouse UI");
      PlaceTile(player, sideRight, mouseUI.thinkingAboutAttackingHereSideAttacks, "Mouse UI");
    }
    // South
    else
    {
      var sideLeft = new Vector3(
        mousePos.x + 1,
        mousePos.y + 1,
        mousePos.z
      );
      var sideRight = new Vector3(
        mousePos.x - 1,
        mousePos.y + 1,
        mousePos.z
      );
      PlaceTile(player, sideLeft, mouseUI.thinkingAboutAttackingHereSideAttacks, "Mouse UI");
      PlaceTile(player, sideRight, mouseUI.thinkingAboutAttackingHereSideAttacks, "Mouse UI");
    }

  }

  public void Clear(MouseUI mouseUI)
  {
    ClearMouseUI(mouseUI, true);
    GameObject.Find("Suggestions").GetComponent<Tilemap>().ClearAllTiles();
  }

  public void ClearMouseUI(MouseUI mouseUI, bool resetCursor)
  {
    if (resetCursor)
    {
      Cursor.SetCursor(null, Vector2.zero, mouseUI.cursorMode);
    }

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