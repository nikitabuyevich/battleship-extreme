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

	[Inject]
	public void Construct(IFogOfWar fogOfWar, IReposition reposition, ITurn turn)
	{
		_fogOfWar = fogOfWar;
		_reposition = reposition;
		_turn = turn;

		players = GameObject.FindObjectsOfType(typeof(Player)) as Player[];
	}

	[Header("UI")]
	public Text movesLeft;
	public Text attacksLeft;
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
	}

	public void EndTurnBtn()
	{
		transition.SetActive(true);
		transitionUI.SetActive(true);
		transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, 0);
		StartCoroutine(BackgroundFadeIn());
	}

	public void NextPlayer()
	{
		var transitionBackground = GameObject.FindGameObjectWithTag("Transition Background");
		StartCoroutine(BackgroundFadeOut());
		_turn.NextPlayer();
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

	private IEnumerator BackgroundFadeIn()
	{
		var t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime * 1.5f;
			transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, t);
			yield return null;
		}

		yield return 0;
	}

	private IEnumerator BackgroundFadeOut()
	{
		var t = 1f;
		transitionUI.SetActive(false);

		while (t > 0f)
		{
			t -= Time.deltaTime * 1.5f;
			transitionBackground.GetComponent<Image>().color = new Color(0, 0, 0, t);
			yield return null;
		}

		transition.SetActive(false);
		yield return 0;
	}
}