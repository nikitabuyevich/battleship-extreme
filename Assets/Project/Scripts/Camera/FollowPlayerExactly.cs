using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FollowPlayerExactly : MonoBehaviour
{

	private ITurn _turn;

	[Inject]
	public void Construct(ITurn turn)
	{
		_turn = turn;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		transform.position = new Vector3(
			_turn.CurrentPlayer().transform.position.x,
			_turn.CurrentPlayer().transform.position.y,
			transform.position.z
		);
	}
}