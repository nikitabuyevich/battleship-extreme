using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public PlayerMovementHelper _movementHelper;
	internal PlayerSpriteRendererHelper _spriteRendererHelper;
	internal PlayerCollisionHelper _collisionHelper;

	void Start()
	{
		// Initialize helpers
		_movementHelper = GetComponent<PlayerMovementHelper>();
		_spriteRendererHelper = GetComponent<PlayerSpriteRendererHelper>();
		_collisionHelper = GetComponent<PlayerCollisionHelper>();

		// move player to starting pos
		transform.position = new Vector3(startingX, startingY, transform.position.z);
	}

	void Update()
	{
		// TODO: display spaces player can move to

		// start moving
		if (!_isMoving && isAllowedToMove)
		{
			_movementHelper.GetInput(this);

			if (!_collisionHelper.SpaceIsBlocked(this))
			{
				_spriteRendererHelper.RenderDirection(this);
				StartCoroutine(_movementHelper.Move(this));
			}
		}
	}
}