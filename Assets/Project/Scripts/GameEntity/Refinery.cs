using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			var player = ownedBy.GetComponent<Player>();
			player.numberOfRefineries -= 1;
			player.income -= income;
			player.gameSceneManager.GetComponent<GameSceneManager>().SetPlayerStats();
			Debug.Log(this.name + " has been destroyed!");
			player.refineries.Remove(this.gameObject);
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