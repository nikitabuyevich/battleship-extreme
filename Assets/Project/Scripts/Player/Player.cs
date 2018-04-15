﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : GameEntity
{
	// Player states
	private IPlayerWaitingTurnState _playerWaitingTurnState;
	private IPlayerMoveState _playerMoveState;
	private IPlayerAttackState _playerAttackState;
	private IPlayerBuildState _playerBuildState;
	private IPlayerShopingState _playerShopingState;

	private IPlayerMovement _playerMovement;
	private IPlayerSpriteRenderer _spriteRenderer;
	private IPlayerMoney _playerMoney;
	private IGameMap _gameMap;
	private IMouse _mouse;

	[Inject]
	public void Construct(
		IPlayerWaitingTurnState playerWaitingTurnState,
		IPlayerMoveState playerMoveState,
		IPlayerAttackState playerAttackState,
		IPlayerBuildState playerBuildState,
		IPlayerShopingState playerShopingState,
		IPlayerMoney playerMoney,
		IPlayerMovement playerMovement,
		IPlayerSpriteRenderer spriteRenderer,
		IGameMap gameMap,
		IMouse mouse)
	{
		_playerWaitingTurnState = playerWaitingTurnState;
		_playerMoveState = playerMoveState;
		_playerAttackState = playerAttackState;
		_playerBuildState = playerBuildState;
		_playerShopingState = playerShopingState;

		_playerMovement = playerMovement;
		_spriteRenderer = spriteRenderer;
		_playerMoney = playerMoney;
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
	public int buildRange = 1;
	public int numberOfMoveSpacesPerTurn = 1;
	public int numberOfMovesPerTurn = 1;
	public int numberOfAttackSpacesPerTurn = 2;
	public int numberOfAttacksPerTurn = 1;
	public int numberOfRefineries = 0;

	[Header("Income")]
	public int money = 500;
	public int income;

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
	public GameObject gameSceneManager;
	public GameObject refinery;
	public GameObject mainHitEffect;
	public GameObject sideHitEffect;

	internal bool _isMoving = false;
	internal Vector2 _input;
	internal Dictionary<string, Color> fogOfWar = new Dictionary<string, Color>();
	internal List<GameObject> refineries = new List<GameObject>();
	internal BoughtAmount boughtAmount = new BoughtAmount();
	internal bool isAbleToMove = false;
	internal bool isAbleToAttack = false;
	internal int refineryHealth = 0;
	internal int refineryVisionRadius = 0;
	internal int refineryIncome = 0;

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
		AbilityShortcuts();

		// TODO: DELETE LATER
		DebugMode();
	}

	private void CheckHealth()
	{
		if (health <= 0)
		{
			gameSceneManager.GetComponent<GameSceneManager>().numberOfPlayers -= 1;
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

	public void CreateRefinery(Vector3 pos)
	{
		var newRefinery = Instantiate(refinery, pos, transform.rotation, transform.parent);
		newRefinery.GetComponentInChildren<SpriteRenderer>().color = new Color(
			this.GetComponentInChildren<SpriteRenderer>().color.r,
			this.GetComponentInChildren<SpriteRenderer>().color.g,
			this.GetComponentInChildren<SpriteRenderer>().color.b,
			this.GetComponentInChildren<SpriteRenderer>().color.a
		);

		this.refineries.Add(newRefinery);
		var refineryComp = newRefinery.GetComponent<Refinery>();
		refineryComp.ownedBy = this.gameObject;
		refineryComp.health = this.refineryHealth;
		refineryComp.maxHealth = this.refineryHealth;
		refineryComp.visionRadius = this.refineryVisionRadius;
		refineryComp.income = this.refineryIncome;
		income += this.refineryIncome;

		this.refineryHealth = 0;
		this.refineryVisionRadius = 0;
		this.refineryIncome = 0;
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
		else if (type == typeof(IPlayerBuildState))
		{
			ChangeState(_playerBuildState);
		}
		else if (type == typeof(IPlayerShopingState))
		{
			ChangeState(_playerShopingState);
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
		_playerMoney.CheckIfOnMoney(this);
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
		// reset refinery resources
		if (currentlyRunningState == _playerBuildState)
		{
			this.refineryHealth = 0;
			this.refineryVisionRadius = 0;
			this.refineryIncome = 0;
		}
		if (currentlyRunningState != _playerWaitingTurnState &&
			previouslyRunningState != _playerWaitingTurnState &&
			previouslyRunningState != null &&
			currentlyRunningState != previouslyRunningState)
		{
			SwitchToPreviouslyRunningState();
		}
	}

	private void AbilityShortcuts()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			gameSceneManager.GetComponent<GameSceneManager>().AbilityMoveBtn();
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			gameSceneManager.GetComponent<GameSceneManager>().AbilityAttackBtn();
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			currentlyRunningState.AbilityRotate(this);
		}
	}

	// TODO: REMOVE LATER, JUST FOR DEBUGGING

	private void DebugMode()
	{
		if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
		{
			money += 1000;
			gameSceneManager.GetComponent<GameSceneManager>().SetPlayerStats();
		}
	}
}