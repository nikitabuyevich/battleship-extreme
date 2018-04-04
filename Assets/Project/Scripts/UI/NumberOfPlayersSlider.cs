using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfPlayersSlider : MonoBehaviour
{
	public Text numberOfPlayers;

	public void SelectNumberOfPlayers(float slider)
	{
		StaticVariables.numberOfPlayers = (int) slider;
		numberOfPlayers.text = "NUMBER OF PLAYERS: " + slider;
	}

}