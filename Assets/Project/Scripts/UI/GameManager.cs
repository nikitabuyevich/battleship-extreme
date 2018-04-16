using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class GameManager : MonoBehaviour
{
	public GameObject transition;
	public GameObject transitionBackground;

	public void PlayGame()
	{
		var soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		soundManager.musicSource.clip = soundManager.mainThemeSong;
		soundManager.musicSource.Play();
		transition.SetActive(true);
		StartCoroutine(BackgroundFadeIn());
	}

	public void SetPlayer1Name(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			StaticVariables.player1Name = name;
		}
	}

	public void SetPlayer2Name(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			StaticVariables.player2Name = name;
		}
	}

	public void SetPlayer3Name(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			StaticVariables.player3Name = name;
		}
	}

	public void SetPlayer4Name(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			StaticVariables.player4Name = name;
		}
	}

	public void LoadStartMenu()
	{
		SceneManager.LoadScene("Start Menu");
	}

	public void LoadOptionsMenu()
	{
		SceneManager.LoadScene("Options");
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void BackToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	private IEnumerator BackgroundFadeIn()
	{
		var t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime * 2f;
			transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, t);
			yield return null;
		}

		SceneManager.LoadScene("Game");
		yield return 0;
	}

}