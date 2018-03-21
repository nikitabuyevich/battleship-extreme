using UnityEngine;

public interface IMouse
{
  Vector3 GetMousePos(Player player);
  void SetAttackCursor(MouseUI mouseUI);
  void DrawAttackSuggestions(Player player);
  void DrawPossibleMoves(Player player);
  void DrawSuggestionOverMouse(Player player);
  void Clear(MouseUI mouseUI);
  void ClearMouseUI(MouseUI mouseUI);
}