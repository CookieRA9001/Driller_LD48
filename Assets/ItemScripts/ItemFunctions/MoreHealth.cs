using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreHealth : Item{
    public override void Craft(int count) {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        pStats.MaxHealth += count;
        pStats.HealthUp(count);
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Max Health Increased To: " + pStats.MaxHealth);
        uScript.UpdateTexts(5);
        uScript.UpdateTexts(6);
    }
}
