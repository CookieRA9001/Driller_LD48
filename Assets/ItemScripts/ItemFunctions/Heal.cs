using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item{
    public override void Craft(int count) {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        pStats.HealthUp(count);
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Health Increased To: " + pStats.Health);
        uScript.UpdateTexts(6);
    }
}
