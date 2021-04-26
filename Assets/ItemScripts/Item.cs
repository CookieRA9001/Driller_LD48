using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {
    public PlayerStats pStats;
    private void Start()
    {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    public abstract void Craft(int count);
}
