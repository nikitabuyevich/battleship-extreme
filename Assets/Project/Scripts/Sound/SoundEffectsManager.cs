using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
	public AudioSource musicSource;

	public AudioClip attackSoundEffect;

	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
		musicSource.clip = attackSoundEffect;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			musicSource.Play();
		}
	}
}