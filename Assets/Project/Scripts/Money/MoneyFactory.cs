using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFactory : MonoBehaviour
{
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
				var moneyObj = Instantiate(money, transform.position, transform.rotation, transform.parent);
				moneyObj.GetComponent<Money>().spawner = this.gameObject;
			}
		}
	}
}