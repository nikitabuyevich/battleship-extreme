using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GameCamera : MonoBehaviour
{

	private ITurn _turn;

	[Inject]
	public void Construct(ITurn turn)
	{
		_turn = turn;
		bounds = GetBounds();
	}

	private BoundsInt bounds;

	// Basically in order to get a 16:9 perfect pixel ratio we need this
	private float magicNumber = 0.26f;

	void LateUpdate()
	{
		transform.position = new Vector3(
			Mathf.Clamp(_turn.CurrentPlayer().transform.position.x, 4.76f, bounds.xMax + 1 + magicNumber),
			Mathf.Clamp(_turn.CurrentPlayer().transform.position.y, 2f, bounds.yMax),
			transform.position.z);
	}

	private BoundsInt GetBounds()
	{
		var tilemaps = GameObject.Find("Floor").GetComponentsInChildren<Tilemap>();
		foreach (var tilemap in tilemaps)
		{
			if (tilemap.name == "Walls")
			{
				return tilemap.cellBounds;
			}
		}

		return new BoundsInt();
	}
}