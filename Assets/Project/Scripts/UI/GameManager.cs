using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public void LoadStartMenu()
	{
		SceneManager.LoadScene("Start Menu");
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void BackToMainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

}