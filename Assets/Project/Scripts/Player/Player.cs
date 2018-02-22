using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	[Header("Setup")]
	public bool isAllowedToMove = true;
	public float moveSpeed = 1f;
	public int gridHeight = 1;
	public int gridWidth = 1;

	[Header("Starting Position")]
	public float startingX;
	public float startingY;

	[Header("Sprites")]
	public Sprite northSprite;
	public Sprite eastSprite;
	public Sprite southSprite;
	public Sprite westSprite;

	internal bool _isMoving = false;
	internal Vector2 _input;

	// Helpers
	internal PlayerMovementHelper _movementHelper;
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

			if (_collisionHelper.SpaceIsNotBlocked(this))
			{
				_spriteRendererHelper.RenderDirection(this);
				StartCoroutine(_movementHelper.Move(this));
			}
		}
	}
}