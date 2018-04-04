using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public Text player1Name;
	public Text player2Name;
	public Text player3Name;
	public Text player4Name;

	public void PlayGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void SetPlayer1Name(string name)
	{
		StaticVariables.player1Name = name;
	}

	public void SetPlayer2Name(string name)
	{
		StaticVariables.player2Name = name;
	}

	public void SetPlayer3Name(string name)
	{
		StaticVariables.player3Name = name;
	}

	public void SetPlayer4Name(string name)
	{
		StaticVariables.player4Name = name;
	}

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