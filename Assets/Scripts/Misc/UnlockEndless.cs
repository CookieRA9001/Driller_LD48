using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockEndless : MonoBehaviour
{
    public GameObject[] tiles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            for (int i = 0; i < tiles.Length; i++) {
                Destroy(tiles[i]);
            }
            for (int f = 0; f < other.gameObject.GetComponent<PlayerStats>().ores.Length; f++) {
                other.gameObject.GetComponent<PlayerStats>().ores[f] += 1000;
            }
            other.gameObject.GetComponent<PlayerStats>().UpdateOreCounter();
        }
    }
}
