using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : GameEntity
{
	[Inject]
	private readonly GameSceneManager _gameSceneManager;

	// Player states
	private IPlayerWaitingTurnState _playerWaitingTurnState;
	private IPlayerMoveState _playerMoveState;
	private IPlayerAttackState _playerAttackState;

	private IPlayerMovement _playerMovement;
	private IPlayerSpriteRenderer _spriteRenderer;
	private IGameMap _gameMap;
	private IMouse _mouse;

	[Inject]
	public void Construct(
		IPlayerWaitingTurnState playerWaitingTurnState,
		IPlayerMoveState playerMoveState,
		IPlayerAttackState playerAttackState,
		IPlayerMovement playerMovement,
		IPlayerSpriteRenderer spriteRenderer,
		IGameMap gameMap,
		IMouse mouse)
	{
		_playerWaitingTurnState = playerWaitingTurnState;
		_playerMoveState = playerMoveState;
		_playerAttackState = playerAttackState;

		_playerMovement = playerMovement;
		_spriteRenderer = spriteRenderer;
		_gameMap = gameMap;
		_mouse = mouse;
	}

	// Events
	public delegate void MovementHandler();

	public event MovementHandler OnPlayerMovement;

	[Header("Upgrades")]
	public bool canMoveAcross = false;
	public bool rotationsAreFree = false;
	public int attackPower = 2;
	public int sideHitAttackPower = 1;
	public int sideHitRange = 1;
	public int numberOfMoveSpacesPerTurn = 1;
	public int numberOfMovesPerTurn = 1;
	public int numberOfAttackSpacesPerTurn = 2;
	public int numberOfAttacksPerTurn = 1;

	[Header("Income")]
	public int money;
	public float income;

	[Header("Gameplay")]
	public float moveSpeed = 2f;
	[Range(0, 1)]
	public float revealAlphaLevel = 1f;
	[Range(0, 1)]
	public float visitedAlphaLevel = 0.75f;

	[Header("Starting Position")]
	public float startingX;
	public float startingY;

	[Header("Sprites")]
	public Sprite northSprite;
	public Sprite eastSprite;
	public Sprite southSprite;
	public Sprite westSprite;
	public float eastWestOffsetX = 0.1f;
	public float eastWestOffsetY = 0.14f;

	[Header("Setup")]
	public GameObject level;
	public GameObject gameCamera;
	public GameObject mouseUI;

	internal bool _isMoving = false;
	internal Vector2 _input;
	internal Dictionary<string, Color> fogOfWar = new Dictionary<string, Color>();
	internal bool isAbleToMove = false;
	internal bool isAbleToAttack = false;

	private IState currentlyRunningState;
	private IState previouslyRunningState;

	void Update()
	{
		ExecuteStateUpdate();

		if (Input.GetMouseButtonDown(1))
		{
			SetInitialState();
		}

		CheckHealth();
	}

	private void CheckHealth()
	{
		if (health <= 0)
		{
			Destroy(this.gameObject);
			Debug.Log(this.name + " has been destroyed!");
		}
	}

	public void Move()
	{
		if (!_isMoving)
		{
			if (Input.GetMouseButtonDown(0))
			{
				var mousePos = _mouse.GetMousePos(this);
				var validPositions = _gameMap.GetValidMovePositions(this);
				if (validPositions.Contains(mousePos))
				{
					var currentDir = _playerMovement.GetDirection(this);
					_spriteRenderer.RenderDirection(this, currentDir);
					StartCoroutine(_playerMovement.Move(this));
					UseMoveTurn();
				}
			}
		}
	}

	public void UseMoveTurn()
	{
		if (OnPlayerMovement != null)
		{
			OnPlayerMovement();
		}
	}

	public IState CurrentState()
	{
		return currentlyRunningState;
	}

	public void ChangeState(Type type)
	{
		if (type == typeof(IPlayerMoveState))
		{
			ChangeState(_playerMoveState);
		}
		else if (type == typeof(IPlayerWaitingTurnState))
		{
			ChangeState(_playerWaitingTurnState);
		}
		else if (type == typeof(IPlayerAttackState))
		{
			ChangeState(_playerAttackState);
		}
		else
		{
			Debug.Log(type.ToString() + " state not found");
		}
	}

	private void ChangeState(IState newState)
	{
		if (currentlyRunningState != null)
		{
			currentlyRunningState.Exit(this);
		}
		previouslyRunningState = currentlyRunningState;
		currentlyRunningState = newState;
		currentlyRunningState.Enter(this);
	}

	public void ExecuteStateUpdate()
	{
		if (currentlyRunningState != null)
		{
			currentlyRunningState.Execute(this);
		}
	}

	public void SwitchToPreviouslyRunningState()
	{
		currentlyRunningState.Exit(this);
		currentlyRunningState = previouslyRunningState;
		currentlyRunningState.Enter(this);
	}

	public void SetInitialState()
	{
		if (currentlyRunningState != _playerWaitingTurnState &&
			previouslyRunningState != _playerWaitingTurnState &&
			previouslyRunningState != null &&
			currentlyRunningState != previouslyRunningState)
		{
			SwitchToPreviouslyRunningState();
		}
	}
}