using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreOres : Item{
    public override void Craft(int count) {
        GameManager GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GM.oreChancesMultiplier += 1f;
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Ore Amount Chance Increased To: " + GM.oreChancesMultiplier);
        uScript.UpdateTexts(2);
    }
}
