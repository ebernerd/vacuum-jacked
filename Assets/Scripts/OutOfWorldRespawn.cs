using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfWorldRespawn : MonoBehaviour {
	public static float outOfWorldY = -50f;
	public Transform respawnPoint;

	private Rigidbody2D rb;
	private void Start() {
		rb = GetComponent<Rigidbody2D>();	
	}

	private void FixedUpdate() {
		if (transform.position.y < outOfWorldY) {
			rb.velocity = Vector3.zero;
			transform.position = respawnPoint.position;

			//	Send a "OnFallOutOfWorld" message to the game object
			SendMessage("OnFallOutOfWorld");
		}
	}
}
