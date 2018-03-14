using System.Collections;
using UnityEngine;

public interface IPlayerMovement
{
	Vector3 GetMousePos(Player player);
	Direction GetMoveDirection(Vector3 mousePos, Player player);
	Direction GetDirection(Player player);
	IEnumerator Move(Player player);
	bool ClickIsValid(Player player);
}