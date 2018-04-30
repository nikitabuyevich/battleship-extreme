using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GameCamera : MonoBehaviour
{
	private ITurn _turn;
	private IGameMap _gameMap;

	[Inject]
	public void Construct(ITurn turn, IGameMap gameMap)
	{
		_turn = turn;
		_gameMap = gameMap;
	}

	void LateUpdate()
	{
		if (_turn != null && _turn.CurrentPlayer() != null)
		{
			transform.position = new Vector3(
				Mathf.Clamp(_turn.CurrentPlayer().transform.position.x + 0.5f, 7.5f, _gameMap.Get().xMax - 1.5f),
				Mathf.Clamp(_turn.CurrentPlayer().transform.position.y + 0.5f, 3.5f, _gameMap.Get().yMax - 1.5f),
				transform.position.z);
		}
	}
}