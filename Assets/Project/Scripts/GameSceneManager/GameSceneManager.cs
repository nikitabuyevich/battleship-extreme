using System.Collections;
using UnityEngine;
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
			players[0].name = StaticVariables.player1Name;
			players[1].name = StaticVariables.player2Name;
			Destroy(players[2]);
			Destroy(players[3]);
		}
		else if (StaticVariables.numberOfPlayers == 3)
		{
			players[0].name = StaticVariables.player1Name;
			players[1].name = StaticVariables.player2Name;
			players[2].name = StaticVariables.player3Name;
			Destroy(players[3]);
		}
		else if (StaticVariables.numberOfPlayers == 4)
		{
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
	public GameObject transition;
	public GameObject transitionUI;
	public GameObject transitionBackground;

	[Header("Setup")]
	public GameObject floor;
	public int screenWidth = 640;
	public int screenHeight = 360;
	public bool gameIsFullscreen = true;

	[Header("Fog of War")]
	[Range(0, 1)]
	public float fogOfWarAlphaLevel = 0.5f;
	public TileBase blackTile;

	internal Player[] players;
	internal int currentPlayersTurn = 0;

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

	public void EndTurnBtn()
	{
		transition.SetActive(true);
		transitionUI.SetActive(true);
		playersName.text = _turn.GetNextPlayer().name + "'s Turn";
		transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, 0);
		StartCoroutine(_sceneTransition.BackgroundFadeIn(transitionBackground, true));
	}

	public void NextPlayer()
	{
		StartCoroutine(_sceneTransition.BackgroundFadeOut(transitionBackground, transition, transitionUI));
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

	private void SetNewGame()
	{
		transition.SetActive(true);
		transitionUI.SetActive(true);
		playersName.text = _turn.CurrentPlayer().name + "'s Turn";
		transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, 1);
	}
}