using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This class handles player-to-flag interactions
 */
public class FlagScript : MonoBehaviour
{
	public GameObject playerFlagGO; //	Reference to the flag held by this player
    public Movement playerMovement; //  Reference to this player's movement script
    public bool isHoldingFlag = false;
	public LayerMask flagLayerMask;
       
    public bool canFlagBePlanted = false;

    public GameObject throwableFlagPrefab;

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

    public void ThrowFlag() {
        //  This script shouldn't do anything if the user isn't holding the flag
        if (!isHoldingFlag) {
            return;
        }

        isHoldingFlag = false;
        // TODO - throw the flag
    }
}