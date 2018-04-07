using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Refinery : GameEntity
{
	public GameObject ownedBy;
	public int income;

	void Update()
	{
		if (health <= 0)
		{
			var player = ownedBy.GetComponent<Player>();
			player.numberOfRefineries -= 1;
			player.income -= income;
			player.gameSceneManager.GetComponent<GameSceneManager>().SetPlayerStats();
			Destroy(this.gameObject);
			Debug.Log(this.name + " has been destroyed!");
			player.refineries.Remove(this.gameObject);
		}
	}
}