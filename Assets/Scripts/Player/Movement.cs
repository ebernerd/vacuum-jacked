using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

	Rigidbody2D rb;
	public float speed;

	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;

	//  Ground check / jumping variables
	public Transform groundCheckTransform;
	public float groundCheckRadius;
	public LayerMask groundLayerMask;
	public float jumpForce;

	bool isGrounded = false;

	public float rememberGroundFor;
	float lastTimeGrounded;

	public const int defaultAdditionalJumps = 1;
	int additionalJumps;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		additionalJumps = defaultAdditionalJumps;
		lastTimeGrounded = Time.captureDeltaTime;
	}

	void Update()
	{
		//	Handle updating movement-related states first
		CheckIfGrounded();

		//	Now that we have all movement states assigned for this frame, let's handle the user input
		HandleHorizontalMovement();
		HandleVerticalMovement();
	}


	//	Checks if the character's ground check object is colliding with an object on the ground layer mask
	void CheckIfGrounded()
	{
		Collider2D collider = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayerMask);
		if (collider != null)
		{
			isGrounded = true;
			additionalJumps = defaultAdditionalJumps;
		}
		else
		{
			if (isGrounded)
			{
				lastTimeGrounded = Time.time;
			}
			isGrounded = false;
		}
	}

	//	Handles actions like jumping or crouching
	void HandleVerticalMovement()
	{
		bool isKeyDown = Input.GetKeyDown(KeyCode.Space);
		bool wasGroundedRecently = Time.time - lastTimeGrounded <= rememberGroundFor;
		bool hasAdditionalJumps = additionalJumps > 0;
		bool canJump = isGrounded || wasGroundedRecently || hasAdditionalJumps;

		if (isKeyDown && canJump)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpForce);
			additionalJumps -= 1;
		}
	}

	//	Handles running and sprinting
	void HandleHorizontalMovement()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float moveBy = x * speed;
		rb.velocity = new Vector2(moveBy, rb.velocity.y);
	}
}
