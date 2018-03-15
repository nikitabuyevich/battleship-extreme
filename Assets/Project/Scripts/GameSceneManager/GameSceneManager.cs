using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Zenject;

public class GameSceneManager : MonoBehaviour
{
	private IFogOfWar _fogOfWar;
	private IPlayerFogOfWar _playerFogOfWar;
	private IReposition _reposition;
	private ITurn _turn;

	[Inject]
	public void Construct(IFogOfWar fogOfWar, IPlayerFogOfWar playerFogOfWar, IReposition reposition, ITurn turn)
	{
		_fogOfWar = fogOfWar;
		_playerFogOfWar = playerFogOfWar;
		_reposition = reposition;
		_turn = turn;

		players = GameObject.FindObjectsOfType(typeof(Player)) as Player[];
	}

	[Header("UI")]
	public Text numberOfMovesText;

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
		Screen.SetResolution(screenWidth, screenHeight, gameIsFullscreen);

		_reposition.SetLevel();
		_fogOfWar.SetFogOfWar();
		_turn.ResetAll();
		_playerFogOfWar.ChangeFogOfWar(_turn.CurrentPlayer(), _turn.CurrentPlayer().revealAlphaLevel);

	}

	public void EndTurnBtn()
	{
		_playerFogOfWar.ChangeFogOfWar(_turn.CurrentPlayer(), _turn.CurrentPlayer().revealAlphaLevel);
		if (numberOfMoves == 0)
		{
			_turn.NextPlayer();
		}
	}
}