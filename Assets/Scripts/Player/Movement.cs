using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : MonoBehaviour {

	Rigidbody2D rb;
	public float speed;
		
	//	Update to the default scale X of the prefab
	public float parentXScale = 0.44315f;

	//  Ground check / jumping variables
	public Transform groundCheckTransform;
	public float groundCheckRadius;
	public LayerMask groundLayerMask;
	public float jumpForce;

	public int directionFacing = 1; //	Set to 1 for facing right, -1 for facing left

	private bool isGrounded = false;

	public float rememberGroundFor;
	private float lastTimeGrounded;

	private Vector2 movementIntent;
	private bool canJump = false;

	private Animator animator;


	void Start() {
		rb = GetComponent<Rigidbody2D>();
		lastTimeGrounded = Time.captureDeltaTime;

		animator = GetComponent<Animator>();
	}

	void Update() {
		//	Handle updating movement-related states first
		CheckIfGrounded();

		//	Now that we have all movement states assigned for this frame, let's handle the user input
		HandleHorizontalMovement();
		HandleVerticalMovement();

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
	}

	//	Checks if the character's ground check object is colliding with an object on the ground layer mask
	void CheckIfGrounded() {
		Collider2D hit = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayerMask);
		if (hit != null) {
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

		if (rb.velocity.y < -1 && !canJump) {
			Debug.Log("Player may jump again");
			canJump = true;
		}
	}

	//	Handles running and sprinting
	void HandleHorizontalMovement() {
		float moveBy = movementIntent.x * speed;
		rb.velocity = new Vector2(moveBy, rb.velocity.y);
		if (transform.localScale.x != directionFacing) {
			transform.localScale = new Vector3(directionFacing * parentXScale, transform.localScale.y, 0);
		}
	}


	/**
	 *	The functions below are automatically called by the input system. It knows to look for the right methods.
	 *	We have actions like "Move", "Punch", etc, and the input system will automatically look for "OnMove", "OnPunch"
	 */
	
	void OnMove(InputValue value)
	{
		movementIntent = value.Get<Vector2>();

		//	Handle detecting which direction the player is facing
		if (movementIntent.x > 0)
		{
			directionFacing = 1;
		}
		else if (movementIntent.x < 0)
		{
			directionFacing = -1;
		}
	}

	//	Unity input system already knows to call this because the name matches the action
	void OnJump()
	{
		bool wasGroundedRecently = Time.time - lastTimeGrounded <= rememberGroundFor;
		bool shouldJump = isGrounded || wasGroundedRecently;

		if (canJump && shouldJump)
		{
			canJump = false;
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpForce);
		}
	}

	void OnPunch() {
		//	Trigger the punch animation
		animator.SetTrigger("Punch01");
	}
}
