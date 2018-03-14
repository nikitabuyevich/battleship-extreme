using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GameCamera : MonoBehaviour
{
	private ITurn _turn;
	private IBounds _bounds;

	[Inject]
	public void Construct(ITurn turn, IBounds bounds)
	{
		_turn = turn;
		_bounds = bounds;
	}

	void LateUpdate()
	{
		transform.position = new Vector3(
			Mathf.Clamp(_turn.CurrentPlayer().transform.position.x + 0.5f, 7.5f, _bounds.Get().xMax - 1.5f),
			Mathf.Clamp(_turn.CurrentPlayer().transform.position.y + 0.5f, 3.5f, _bounds.Get().yMax - 1.5f),
			transform.position.z);
	}
}