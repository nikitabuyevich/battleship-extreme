using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Zenject;

public class GameSceneManager : MonoBehaviour
{
	private IPlayerFogOfWar _playerFogOfWar;
	private ITurn _turn;

	[Inject]
	public void Construct(IPlayerFogOfWar playerFogOfWar, ITurn turn)
	{
		_playerFogOfWar = playerFogOfWar;
		_turn = turn;

		players = GameObject.FindObjectsOfType(typeof(Player)) as Player[];
		_turn.Init(players, this);
	}

	[Header("UI")]
	public Text numberOfMovesText;

	[Header("Setup")]
	public GameObject floor;

	private Player[] players;
	internal int currentPlayersTurn = 0;

	private int _numberOfMoves;
	internal int numberOfMoves
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

	void Start()
	{
		_turn.ResetAll();
		_playerFogOfWar.ChangeFogOfWar(_turn.CurrentPlayer(), _turn.CurrentPlayer().revealAlphaLevel);
	}

	public void EndTurnBtn()
	{
		if (numberOfMoves == 0)
		{
			_turn.NextPlayer();
		}
	}
}