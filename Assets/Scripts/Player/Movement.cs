using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : MonoBehaviour {

	Rigidbody2D rb;
	public float speed;

	//  Ground check / jumping variables
	public Transform groundCheckTransform;
	public float groundCheckRadius;
	public LayerMask groundLayerMask;
	public float jumpForce;

	private bool isGrounded = false;

	public float rememberGroundFor;
	private float lastTimeGrounded;

	private Vector2 movementIntent;
	private bool canJump = false;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		lastTimeGrounded = Time.captureDeltaTime;
	}

	void Update() {
		//	Handle updating movement-related states first
		CheckIfGrounded();

		//	Now that we have all movement states assigned for this frame, let's handle the user input
		HandleHorizontalMovement();
		HandleVerticalMovement();
	}

	//	The Unity input system automatically calls this method and updates our movementIntent vector2
	void OnMove(InputValue value) {
		movementIntent = value.Get<Vector2>();
	}

	//	Checks if the character's ground check object is colliding with an object on the ground layer mask
	void CheckIfGrounded() {
		Collider2D collider = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayerMask);
		if (collider != null) {
			isGrounded = true;
		} else {
			if (isGrounded) {
				lastTimeGrounded = Time.time;
			}
			isGrounded = false;
		}
	}

	//	Handles actions like jumping or crouching
	void HandleVerticalMovement() {

		if (canJump && movementIntent.y == 1) {
			bool wasGroundedRecently = Time.time - lastTimeGrounded <= rememberGroundFor;
			bool shouldJump = isGrounded || wasGroundedRecently;

			if (canJump && shouldJump) {
				canJump = false;
				rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpForce);
			}
		}


		if (movementIntent.y < 1 && !canJump) {
			canJump = true;
		}
	}

	//	Handles running and sprinting
	void HandleHorizontalMovement() {
		float moveBy = movementIntent.x * speed;
		rb.velocity = new Vector2(moveBy, rb.velocity.y);
	}
}
