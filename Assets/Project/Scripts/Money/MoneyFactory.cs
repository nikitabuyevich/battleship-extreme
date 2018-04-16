using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoneyFactory : MonoBehaviour
{
	private ITurn _turn;
	private IGameMap _gameMap;

	[Inject]
	public void Construct(
		IGameMap gameMap,
		ITurn turn)
	{
		_gameMap = gameMap;
		_turn = turn;
	}

	public GameSceneManager gameSceneManager;
	public GameObject money;
	public int respawnTime;

	internal bool moneySpawned = false;
	internal int modifiedRespawnTime = 0;

	void Update()
	{
		if (!moneySpawned)
		{
			if (gameSceneManager.numberOfTurns >= modifiedRespawnTime)
			{
				moneySpawned = true;
				var gameObj = GameObject.Find("Game");
				var moneyObj = Instantiate(money, transform.position, transform.rotation, gameObj.transform);
				moneyObj.GetComponent<Money>().spawner = this.gameObject;
				var player = _turn.CurrentPlayer();
				_gameMap.CheckAndHideGameEntities(player);
			}
		}
	}
}