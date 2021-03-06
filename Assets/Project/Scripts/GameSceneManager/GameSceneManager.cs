﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Zenject;

public class GameSceneManager : MonoBehaviour
{
	private IFogOfWar _fogOfWar;
	private IReposition _reposition;
	private ITurn _turn;
	private ISceneTransition _sceneTransition;

	[Inject]
	public void Construct(
		IFogOfWar fogOfWar,
		IReposition reposition,
		ITurn turn,
		ISceneTransition sceneTransition)
	{
		_fogOfWar = fogOfWar;
		_reposition = reposition;
		_turn = turn;
		_sceneTransition = sceneTransition;

		players = GameObject.FindObjectsOfType(typeof(Player)) as Player[];
		if (StaticVariables.numberOfPlayers == 2)
		{
			Destroy(players[2].gameObject);
			Destroy(players[3].gameObject);
			numberOfPlayers = 2;
			players = new Player[]
			{
				players[0],
					players[1]
			};
			players[0].name = StaticVariables.player1Name;
			players[1].name = StaticVariables.player2Name;
		}
		else if (StaticVariables.numberOfPlayers == 3)
		{
			Destroy(players[3].gameObject);
			numberOfPlayers = 3;
			players = new Player[]
			{
				players[0],
					players[1],
					players[2]
			};
			players[0].name = StaticVariables.player1Name;
			players[1].name = StaticVariables.player2Name;
			players[2].name = StaticVariables.player3Name;
		}
		else if (StaticVariables.numberOfPlayers == 4)
		{
			numberOfPlayers = 4;
			players[0].name = StaticVariables.player1Name;
			players[1].name = StaticVariables.player2Name;
			players[2].name = StaticVariables.player3Name;
			players[3].name = StaticVariables.player4Name;
		}
	}

	[Header("UI")]
	public Text movesLeft;
	public Text attacksLeft;
	public Text playersName;
	public Text playersLeft;
	public Text currentPlayerText;
	public Text winnerText;
	public Text money;
	public Text income;
	public Text refineriesLeft;

	// Player health stats
	[Header("Health UI")]
	public Text healthCurrentText;
	public Text healthMaxText;

	public GameObject endGameTransition;
	public GameObject endGameUI;
	public GameObject endGameBackground;

	public GameObject transition;
	public GameObject transitionUI;
	public GameObject transitionBackground;

	[Header("Abilities UI")]
	public Button moveButton;
	public Button attackButton;
	public Button rotateButton;

	[Header("Setup")]
	public GameObject floor;
	public ShopManager shopManager;
	public TextManager textManager;
	public int screenWidth = 640;
	public int screenHeight = 360;
	public bool gameIsFullscreen = true;

	[Header("Fog of War")]
	[Range(0, 1)]
	public float fogOfWarAlphaLevel = 0.5f;
	public TileBase blackTile;

	internal Player[] players;
	internal int currentPlayersTurn = 0;
	internal int numberOfMovesThisTurn = 0;
	internal int numberOfAttacksThisTurn = 0;
	internal int numberOfTurns = 0;
	internal bool gameOver = false;

	private string _currentPlayer;
	internal string currentPlayer
	{
		set
		{
			_currentPlayer = value;
			currentPlayerText.text = _currentPlayer + "'s\n Turn";
		}
		get
		{
			return _currentPlayer;
		}
	}

	private int _numberOfPlayers;
	internal int numberOfPlayers
	{
		set
		{
			_numberOfPlayers = value;
			if (_numberOfPlayers == 1)
			{
				DeclareWinner();
			}
			playersLeft.text = "Players \nLeft: " + _numberOfPlayers;
		}
		get
		{
			return _numberOfPlayers;
		}
	}

	private int _numberOfAttacks;
	internal int numberOfAttacks
	{
		set
		{
			_numberOfAttacks = value;
			attacksLeft.text = "Attacks \nLeft: " + _numberOfAttacks;
		}
		get
		{
			return _numberOfAttacks;
		}
	}

	private int _numberOfRefineries;
	internal int numberOfRefineries
	{
		set
		{
			_numberOfRefineries = value;
			refineriesLeft.text = "Refineries \nOwned: " + _numberOfRefineries;
		}
		get
		{
			return _numberOfRefineries;
		}
	}

	private int _numberOfMoves;
	internal int numberOfMoves
	{
		set
		{
			_numberOfMoves = value;
			movesLeft.text = "Moves \nLeft: " + _numberOfMoves;
		}
		get
		{
			return _numberOfMoves;
		}
	}

	void Start()
	{
		Screen.SetResolution(screenWidth, screenHeight, gameIsFullscreen);

		_reposition.SetLevel();
		_fogOfWar.SetFogOfWar();
		_turn.ResetAll();
		SetNewGame();
	}

	private void DeclareWinner()
	{
		gameOver = true;
		PlayWinningTheme();
		endGameTransition.SetActive(true);
		endGameUI.SetActive(true);
		winnerText.text = currentPlayer + " WON!";
		textManager.SendMessageToChat(currentPlayer + " is the winner!", Message.MessageType.announcement);
		endGameBackground.GetComponent<Image>().color = new Color(0, 0, 0, 0);
		var transparencyAlphaValue = 75f / 255f;
		StartCoroutine(_sceneTransition.BackgroundFadeIn(endGameBackground, transparencyAlphaValue, false));
	}

	public void GoToMainMenu()
	{
		PlayTitleScreenThemeSong();
		SceneManager.LoadScene("Main Menu");
	}

	public void EndTurnBtn()
	{
		PlayEndTurnSoundEffect();
		numberOfTurns += 1;
		transition.SetActive(true);
		transitionUI.SetActive(true);
		playersName.text = _turn.GetNextPlayer().name + "'s Turn";
		transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, 0);
		StartCoroutine(_sceneTransition.BackgroundFadeIn(transitionBackground, 1f, true));
	}

	public void NextPlayer()
	{
		PlaySelectSoundEffect();
		StartCoroutine(_sceneTransition.BackgroundFadeOut(transitionBackground, transition, transitionUI, shopManager));
	}

	public void AbilityCancelBtn()
	{
		var player = _turn.CurrentPlayer();
		player.SetInitialState();
	}

	public void AbilityMoveBtn()
	{
		if (numberOfMoves > 0)
		{
			var player = _turn.CurrentPlayer();
			player.ChangeState(typeof(IPlayerMoveState));
		}
	}

	public void AbilityAttackBtn()
	{
		if (numberOfAttacks > 0)
		{
			var player = _turn.CurrentPlayer();
			player.ChangeState(typeof(IPlayerAttackState));
		}
	}

	public void AbilityRotateBtn()
	{
		var player = _turn.CurrentPlayer();
		player.CurrentState().AbilityRotate(player);
	}

	public void SetPlayerStats()
	{
		var player = _turn.CurrentPlayer();

		currentPlayer = player.name;
		money.text = "$" + player.money;
		income.text = "+" + player.income;
		numberOfRefineries = player.numberOfRefineries;

		// set player health information
		healthCurrentText.text = player.health.ToString();
		healthMaxText.text = player.maxHealth.ToString();

		numberOfMoves = player.numberOfMovesPerTurn - numberOfMovesThisTurn;
		numberOfAttacks = player.numberOfAttacksPerTurn - numberOfAttacksThisTurn;

		SetAbilityButtons();
	}

	public Player GetCurrentPlayer()
	{
		return _turn.CurrentPlayer();
	}

	public void SetAbilityButtons()
	{
		var player = _turn.CurrentPlayer();

		this.moveButton.interactable = this.numberOfMoves > 0;
		this.attackButton.interactable = this.numberOfAttacks > 0;
		if (player.rotationsAreFree)
		{
			this.rotateButton.interactable = true;
		}
		else
		{
			this.rotateButton.interactable = this.numberOfMoves > 0;
		}
	}

	private void SetNewGame()
	{
		textManager.SendMessageToChat("Started a new game of Sea Conquest!", Message.MessageType.announcement);
		SetPlayerStats();
		transition.SetActive(true);
		transitionUI.SetActive(true);
		playersName.text = _turn.CurrentPlayer().name + "'s Turn";
		transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, 1);
	}

	private void PlaySelectSoundEffect()
	{
		var soundEffectsManager = GameObject.Find("SoundEffectsManager").GetComponent<SoundEffectsManager>();
		soundEffectsManager.musicSource.clip = soundEffectsManager.selectSoundEffect;
		soundEffectsManager.musicSource.Play();
	}

	private void PlayEndTurnSoundEffect()
	{
		var soundEffectsManager = GameObject.Find("SoundEffectsManager").GetComponent<SoundEffectsManager>();
		soundEffectsManager.musicSource.clip = soundEffectsManager.endTurnSoundEffect;
		soundEffectsManager.musicSource.Play();
	}

	private void PlayWinningTheme()
	{
		var soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		soundManager.musicSource.clip = soundManager.winThemeSong;
		soundManager.musicSource.Play();
	}

	private void PlayTitleScreenThemeSong()
	{
		var soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		soundManager.musicSource.clip = soundManager.titleScreenThemeSong;
		soundManager.musicSource.Play();
	}
}