using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Refinery : GameEntity
{

	public GameObject deathEffect;
	public int income;

	internal GameObject ownedBy;

	private bool initiatedDeath = false;

	void Update()
	{
		if (health <= 0 && !initiatedDeath)
		{
			initiatedDeath = true;
			var refineryOwner = ownedBy.GetComponent<Player>();
			refineryOwner.numberOfRefineries -= 1;
			refineryOwner.income -= income;
			refineryOwner.refineries.Remove(this.gameObject);

			var gameSceneManager = GameObject.Find("GameSceneManager").GetComponent<GameSceneManager>();
			var player = gameSceneManager.GetCurrentPlayer();
			player.gameSceneManager.GetComponent<GameSceneManager>().SetPlayerStats();
			StartCoroutine(DelayDeathAnimation(0.25f));
			player.WaitUntilParticlesFade(1.5f);
		}
	}

	private IEnumerator DelayDeathAnimation(float amount)
	{
		yield return new WaitForSeconds(amount);
		Instantiate(deathEffect, transform.position, transform.rotation, transform.parent);
		PlayDestroySoundEffect();
		Destroy(this.gameObject);
	}

	private void PlayDestroySoundEffect()
	{
		var soundEffectsManager = GameObject.Find("SoundEffectsManager").GetComponent<SoundEffectsManager>();
		soundEffectsManager.musicSource.clip = soundEffectsManager.destroySoundEffect;
		soundEffectsManager.musicSource.Play();
	}
}