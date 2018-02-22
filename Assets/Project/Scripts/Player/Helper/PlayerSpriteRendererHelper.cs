using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRendererHelper : MonoBehaviour
{

	public void RenderDirection(Player player)
	{
		var currentDir = player._movementHelper.GetDirection(player._input);

		switch (currentDir)
		{
			case Direction.North:
				gameObject.GetComponent<SpriteRenderer>().sprite = player.northSprite;
				break;
			case Direction.East:
				gameObject.GetComponent<SpriteRenderer>().sprite = player.eastSprite;
				break;
			case Direction.South:
				gameObject.GetComponent<SpriteRenderer>().sprite = player.southSprite;
				break;
			case Direction.West:
				gameObject.GetComponent<SpriteRenderer>().sprite = player.westSprite;
				break;
		}
	}
}