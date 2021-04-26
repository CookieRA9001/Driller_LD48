using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFloorTrigger : MonoBehaviour
{
    bool triggered = false;
    public FloorScript fScript;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered) {
            triggered = true;
            PlayerStats pStats = other.GetComponent<PlayerStats>();
            pStats.NewFloor();
            pStats.FloorIn = fScript.parent;
        }
    }
}
