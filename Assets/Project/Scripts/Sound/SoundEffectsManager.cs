using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
	public AudioSource musicSource;

	public AudioClip attackSoundEffect;
	public AudioClip destroySoundEffect;
	public AudioClip hitSoundEffect;
	public AudioClip pickupSoundEffect;
	public AudioClip placeSoundEffect;
	public AudioClip selectSoundEffect;
	public AudioClip powerupSoundEffect;
	public AudioClip moveSoundEffect;
	public AudioClip endTurnSoundEffect;

	void Start()
	{
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}
}