using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This class handles player-to-flag interactions
 */
public class FlagHandler : MonoBehaviour
{
    //  How far from the player's center the flag should spawn on throw
    public static Vector2 flagThrowOffset = new Vector2(1.5f, 2.2f);
    public static int damageNeededToDrop = 5;

	public GameObject playerFlagGO; //	Reference to the flag held by this player
    public bool isHoldingFlag = false;
	public LayerMask flagLayerMask;
       
    public bool canFlagBePlanted = false;

    public GameObject throwableFlagPrefab;

    //  To be set in Start()
    private PlayerController playerController;
    private Rigidbody2D rb;

	private void Start() {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
	}

	public void OnCollisionEnter2D(Collision2D collision) {
        //  This script needn't do any processing if the user is already holding the flag
        if (isHoldingFlag) {
            return;
        }

        bool isFlag = Utilities.IsObjectInLayer(collision.gameObject, flagLayerMask);
        if (isFlag) {
            isHoldingFlag = true;

            //  Destroy the flag game object, hiding it from scene
            Destroy(collision.gameObject);

            //  Enable the flag GO so that it looks like the user is holding it
            playerFlagGO.SetActive(true);
        }
    }

    public void ThrowFlag(float throwForce = 3f) {
        //  This script shouldn't do anything if the user isn't holding the flag
        if (!isHoldingFlag) {
            return;
        }

        isHoldingFlag = false;
        playerFlagGO.SetActive(false);

        int facing = (int)playerController.directionFacing;
        Vector3 throwSpawnPoint = new Vector3(
            transform.position.x + (flagThrowOffset.x * facing),
            transform.position.y + flagThrowOffset.y, 
            0
        );

        GameObject newFlagGO = Instantiate(throwableFlagPrefab, throwSpawnPoint, Quaternion.identity);
        throwableFlagPrefab.GetComponent<OutOfWorldRespawn>().respawnPoint = GameObject.Find("Flag Respawn").transform;
        Vector2 newVelocity = new Vector2((throwForce + Mathf.Abs(rb.velocity.x)) * facing, rb.velocity.y);
        newFlagGO.GetComponent<Rigidbody2D>().velocity = newVelocity;
    }
}