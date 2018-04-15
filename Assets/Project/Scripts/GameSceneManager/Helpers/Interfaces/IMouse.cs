using UnityEngine;

public interface IMouse
{
  Vector3 GetMousePos(Player player);
  void SetAttackCursor(MouseUI mouseUI);
  void SetDefaultCursor(MouseUI mouseUI);
  void DrawAttackSuggestions(Player player);
  void DrawMoveSuggestions(Player player);
  void DrawMoveSuggestionsHover(Player player);
  void DrawBuildRefinerySuggestions(Player player);
  void DrawBuildRefinerySuggestionHover(Player player);
  void DrawAttackSuggestionsHover(Player player);
  void Clear(MouseUI mouseUI);
  void ClearMouseUI(MouseUI mouseUI, bool resetCursor);
}