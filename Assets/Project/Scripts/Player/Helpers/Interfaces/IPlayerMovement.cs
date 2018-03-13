using System.Collections;
using UnityEngine;

public interface IPlayerMovement
{
	Vector3 GetMouseClick(Player player);
	Direction GetDirection(Player player);
	IEnumerator Move(Player player);
}