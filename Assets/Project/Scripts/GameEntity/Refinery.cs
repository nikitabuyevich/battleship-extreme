using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refinery : GameEntity
{
	public GameObject deathEffect;
	public int income;

	internal GameObject ownedBy;

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
			Instantiate(deathEffect, transform.position, transform.rotation, transform.parent);
			player.WaitUntilParticlesFade(1.5f);
		}
	}
}