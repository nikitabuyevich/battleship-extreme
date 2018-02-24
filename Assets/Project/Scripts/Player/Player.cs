using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{

	[Header("Setup")]
	public bool isAllowedToMove = true;
	public float moveSpeed = 1f;
	public int visionRadius = 1;
	[Range(0, 1)]
	public float revealAlphaLevel = 1f;
	[Range(0, 1)]
	public float visitedAlphaLevel = 0.75f;
	public bool squareVision = true;

	[Header("Starting Position")]
	public float startingX;
	public float startingY;

	[Header("Sprites")]
	public Sprite northSprite;
	public Sprite eastSprite;
	public Sprite southSprite;
	public Sprite westSprite;

	[Header("GameObjects")]
	public GameObject level;

	internal bool _isMoving = false;
	internal Vector2 _input;

	// Helpers
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

	void Start()
	{
		// move player to starting pos
		transform.position = new Vector3(startingX, startingY, transform.position.z);
	}

	void Update()
	{
		// TODO: display spaces player can move to

		// start moving
		if (!_isMoving && isAllowedToMove)
		{
			_playerMovement.GetInput(this);
			if (_input != Vector2.zero && !_playerCollisions.SpaceIsBlocked(this))
			{
				_spriteRenderer.RenderDirection(this);
				StartCoroutine(_playerMovement.Move(this));
			}
		}
	}
}