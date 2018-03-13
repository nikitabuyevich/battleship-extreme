using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
	private IPlayerMovement _playerMovement;
	private IPlayerSpriteRenderer _spriteRenderer;
	private IPlayerCollisions _playerCollisions;

	[Inject]
	public void Construct(
		IPlayerMovement playerMovement,
		IPlayerSpriteRenderer spriteRenderer,
		IPlayerCollisions playerCollisions)
	{
		_playerMovement = playerMovement;
		_spriteRenderer = spriteRenderer;
		_playerCollisions = playerCollisions;
	}

	[Header("Gameplay")]
	public bool isAllowedToMove = true;
	public float moveSpeed = 1f;
	public int visionRadius = 1;
	public int numberOfMovesPerTurn = 1;
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

	internal bool _isMoving = false;
	internal Vector2 _input;
	internal Dictionary<string, Color> fogOfWar = new Dictionary<string, Color>();

	// Events
	public delegate void MovementHandler();

	public event MovementHandler OnPlayerMovement;

	void Update()
	{
		// TODO: display spaces player can move to
		var returnedCameraPos = gameCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
		Debug.Log(returnedCameraPos);
		// start moving
		if (!_isMoving && isAllowedToMove)
		{
			if (_playerMovement.ClickIsValid(this) && !_playerCollisions.SpaceIsBlocked(this))
			{
				_spriteRenderer.RenderDirection(this);
				StartCoroutine(_playerMovement.Move(this));
				if (OnPlayerMovement != null)
				{
					OnPlayerMovement();
				}
			}
		}
	}
}