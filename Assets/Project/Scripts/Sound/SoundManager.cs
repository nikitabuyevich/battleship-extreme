using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

	public AudioSource musicSource;

	public AudioClip titleScreenThemeSong;
	public AudioClip mainThemeSong;
	public AudioClip winThemeSong;

	void Start()
	{
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		musicSource.clip = titleScreenThemeSong;
		musicSource.Play();
	}
}