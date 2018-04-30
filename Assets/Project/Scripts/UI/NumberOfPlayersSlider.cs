using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfPlayersSlider : MonoBehaviour
{
	public Text numberOfPlayers;

	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;

	void Start()
	{
		player1.SetActive(true);
		player2.SetActive(true);
		player3.SetActive(false);
		player4.SetActive(false);
	}

	public void SelectNumberOfPlayers(float slider)
	{
		var desiredPlayers = (int) slider;
		StaticVariables.numberOfPlayers = desiredPlayers;
		if (desiredPlayers == 2)
		{
			player3.SetActive(false);
			player4.SetActive(false);
		}
		else if (desiredPlayers == 3)
		{
			player3.SetActive(true);
			player4.SetActive(false);
		}
		else if (desiredPlayers == 4)
		{
			player3.SetActive(true);
			player4.SetActive(true);
		}

		numberOfPlayers.text = "NUMBER OF PLAYERS: " + slider;

	}

}