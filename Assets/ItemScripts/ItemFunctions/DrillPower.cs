using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillPower : Item{
    public int DrillPowerPerItem;
    public override void Craft(int count) {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        pStats.getDrillPower(DrillPowerPerItem * count);
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Drill Power Increased To: " + pStats.DrillPower);
    }
}
