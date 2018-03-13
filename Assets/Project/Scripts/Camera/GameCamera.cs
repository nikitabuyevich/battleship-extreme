using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GameCamera : MonoBehaviour
{
	private ICameraPosition _cameraPosition;

	[Inject]
	public void Construct(ICameraPosition cameraPosition)
	{
		_cameraPosition = cameraPosition;
	}

	void LateUpdate()
	{
		transform.position = _cameraPosition.GetCameraPoisition(transform.position.z);
	}
}