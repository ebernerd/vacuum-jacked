using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCapture : MonoBehaviour
{
	public GameObject playerFlagGO; //	Reference to the flag held by this player
    public bool isHoldingFlag = false;
	public LayerMask flagLayerMask;

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