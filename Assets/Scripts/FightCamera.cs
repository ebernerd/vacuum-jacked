using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FightCamera : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public CinemachineVirtualCamera vcam;

    private Vector2 midpoint;
    private float zoom;

    private void UpdateMidpoint() {
        Vector2 pos1 = player1.transform.position;
        Vector2 pos2 = player2.transform.position;

        midpoint = new Vector3((pos1.x + pos2.x) / 2, ((pos1.y + pos2.y) / 2) + 2f, transform.position.z);
    }

    private void UpdateZoom() {
        
	}

    private void CommitUpdates() {
        transform.position = midpoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateMidpoint();
        UpdateZoom();
        CommitUpdates();
    }
}
