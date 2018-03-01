using System.Collections;
using UnityEngine;

public interface IPlayerMovement
{
	void GetInput(Player player);
	Direction GetDirection(Vector2 input);
	IEnumerator Move(Player player);
}