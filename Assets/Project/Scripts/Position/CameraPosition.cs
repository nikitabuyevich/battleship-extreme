﻿using UnityEngine;

public class CameraPosition : MonoBehaviour
{

	public GameObject levelObj;
	public GameObject background;

	// Use this for initialization
	void Start()
	{
		var positionScript = levelObj.GetComponent<LevelPosition>();
		var levelPosition = positionScript.GetRepositionVector3(transform.position);

		// Put level in the bottom left positio no of the camera
		transform.position = new Vector3(
			levelPosition.x + 2.11f,
			levelPosition.y + 1.25f,
			transform.position.z
		);

		// Set zoom
		GetComponent<Camera>().orthographicSize = 6.25f;

		background.transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			background.transform.position.z
		);
	}
}