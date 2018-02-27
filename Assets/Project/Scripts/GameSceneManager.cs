using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
	[Header("Gameplay")]
	public Player player;
	public int numberOfMovesPerTurn = 1;

	[Header("UI")]
	public Text numberOfMovesText;

	private int _numberOfMoves = 0;
	private int numberOfMoves
	{
		set
		{
			_numberOfMoves = value;
			numberOfMovesText.text = "Number of Moves Left: " + _numberOfMoves;
		}
		get
		{
			return _numberOfMoves;
		}
	}

	// Use this for initialization
	void Start()
	{
		// subscribe to player events
		player.OnPlayerMovement += OnPlayerMovement;

		numberOfMoves = numberOfMovesPerTurn;
	}

	private void OnPlayerMovement()
	{
		numberOfMoves -= 1;
	}
}