using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour {

	public enum PlayerDirection {
		Left = -1,
		Right = 1
	}
	
	Rigidbody2D rb;
	public float speed;

	//	Update to the default scale X of the prefab
	public float parentXScale = 0.44315f;

	//  Ground check / jumping variables
	public Transform groundCheckTransform;
	public Transform respawnPoint;
	public float groundCheckRadius;
	public float jumpForce;

	public LayerMask groundLayerMask;
	public LayerMask flagPlantLayerMask;

	public Collider2D playerCollider; // the collider that actually collides with ground, players, etc
	public KeyCode[] KeyCodes = (KeyCode.O, KeyCode.P);

	public PlayerDirection directionFacing = PlayerDirection.Left;

	private bool isGrounded = true;

	public float rememberGroundFor;
	private float lastTimeGrounded;

	private Vector2 movementIntent;

	private bool canJump = true;
	private bool isInFlagZone = false;

	//	Resources to be fetched on Start()
	private Animator animator;
	private FlagHandler flagHandler;


	void Start() {
		rb = GetComponent<Rigidbody2D>();
		lastTimeGrounded = Time.captureDeltaTime;

		animator = GetComponent<Animator>();
		flagHandler = GetComponent<FlagHandler>();
	}

	
	
	void FixedUpdate() {
		//	Handle updating movement-related states first
		CheckIfGrounded();
		//check if combo recording 
		CheckCombo();
		//	Now that we have all movement states assigned for this frame, let's handle the user input
		HandleHorizontalMovement();
		HandleVerticalMovement();

	}



	public void DetectPressedKeys()
    {
        //Go through all the Keys
        //To make it faster we can attach a class and put all the keys that are allowed to be pressed
        //This will make the process a bit faster rather than moving through all keys
        KeyCodes = (KeyCode.O, KeyCode.P);
		foreach (KeyCode kcode in KeyCodes)
        {
            if (Input.GetKeyDown(kcode))
            {
                KeysPressed.Add(kcode); //Add the Key to the List

               // StartCoroutine(ResetComboTimer()); //Start the Reseting process
            }
        }
    }

   
	//reset players combo and stun them.
	public void ResetCombo()
    {
        KeysPressed.Clear(); //Empty the list
    }





	private Tuple<Vector3, Vector2> GetBoxCastParams() {
		Vector3 center = groundCheckTransform.position;
		Bounds bounds = playerCollider.bounds;
		Vector2 vecBounds = new Vector2(bounds.size.x, groundCheckRadius);
		return Tuple.Create(center, vecBounds);
	}

	public void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		var bounds = GetBoxCastParams();
		Gizmos.DrawWireCube(bounds.Item1, bounds.Item2);
	}


	void CheckCombo() {
		if (Input.GetKey(KeyCode.LeftShift))
		{

			combo_start_time = Time.time;
			DetectPressedKeys();
		}
	}

	//	Checks if the character's ground check object is colliding with an object on the ground layer mask
	void CheckIfGrounded() {
		//	Build the box
		var bounds = GetBoxCastParams();
		Collider2D hit = Physics2D.BoxCast(bounds.Item1, bounds.Item2, 0, Vector2.down, 1f).collider;
		
		if (hit != null && hit.GetComponent<Collider2D>() != playerCollider) {
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

		int facing = (int)directionFacing;
		if (transform.localScale.x != facing) {
			transform.localScale = new Vector3(facing * parentXScale, transform.localScale.y, 0);
		}
	}

	void CheckForFallOutOfWorld() {
		if (transform.position.y < Utilities.outOfWorldY) {
			GetHealth().DealDamage(9999);
		}
	}

	public void Respawn() {
		transform.position = respawnPoint.position;
	}

	//	To be called by subcomponents (e.g., player 1 damage script uses GetHealth() on the enemy's controller
	public Health GetHealth() {
		return GetComponent<Health>();
	}

	public FlagHandler GetFlagHandler() {
		return GetComponent<FlagHandler>();
	}

	public Collider2D GetPlayerCollider() {
		return playerCollider;
	}

	/**
	 *	The functions below are automatically called by the input system. It knows to look for the right methods.
	 *	We have actions like "Move", "Punch", etc, and the input system will automatically look for "OnMove", "OnPunch"
	 */
	void OnMove(InputValue value) {
		movementIntent = value.Get<Vector2>();

		animator.SetFloat("Walk01", movementIntent.normalized.magnitude);

		//	Handle detecting which direction the player is facing
		float movementThreshold = 0.5f;
		if (movementIntent.x > movementThreshold) {
			directionFacing = PlayerDirection.Right;
		} else if (movementIntent.x < -movementThreshold) {
			directionFacing = PlayerDirection.Left;
		}
	}

	//	Unity input system already knows to call this because the name matches the action
	void OnJump()
	{
		bool wasGroundedRecently = Time.time - lastTimeGrounded <= rememberGroundFor;
		bool shouldJump = isGrounded || wasGroundedRecently;
		Debug.Log(wasGroundedRecently + " " + shouldJump + " " + canJump);
		if (canJump && shouldJump)
		{
			canJump = false;
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpForce);
		}
	}

	void OnPrimaryPunch() {
		animator.SetTrigger("Punch01");
	}

	void OnSecondaryPunch() {
		animator.SetTrigger("Punch02");
	}

	void OnThrowFlag() {
		flagHandler.ThrowFlag();
	}

	void OnPlantFlag() {
		if (isInFlagZone) {
			flagHandler.PlantFlag();
		}
	}


	void OnPause() {
		Debug.Log("Pausing");
		PauseMenu pauseMenu = GameObject.Find("Game UI").GetComponent<PauseMenu>();
		pauseMenu.Pause();
	}

	//	This method is called via OutOfWorldRespawn
	void OnFallOutOfWorld() {
		//	Deal enough damage to mark a death
		GetHealth().DealDamage(9999);
	}



	private void OnTriggerEnter2D(Collider2D collision) {
		if (Utilities.IsObjectInLayer(collision.gameObject, flagPlantLayerMask)) {
			isInFlagZone = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (Utilities.IsObjectInLayer(collision.gameObject, flagPlantLayerMask)) {
			isInFlagZone = false;
		}
	}
}

