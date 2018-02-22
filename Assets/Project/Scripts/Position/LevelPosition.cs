using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelPosition : MonoBehaviour
{

	public GameObject player;
	private Vector3 leftMostCorner;

	// Use this for initialization
	void Start()
	{
		transform.position = GetRepositionVector3(transform.position);
	}

	public Vector3 GetRepositionVector3(Vector3 pos)
	{
		var children = GetComponentsInChildren<Transform>();
		foreach (var child in children)
		{
			if (child.transform.name == "Base")
			{
				leftMostCorner = child.GetComponent<Tilemap>().origin;
			}
		}

		// Subtract by 1.5 units to position objects in bottom left corner
		var moveUnits = 1.5f;

		return new Vector3(
			Mathf.Abs(leftMostCorner.x + moveUnits),
			Mathf.Abs(leftMostCorner.y + moveUnits),
			pos.z
		);
	}
}