using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreLight : Item{
    public override void Craft(int count) {
        GameManager GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GM.DarknessDiv += 500*count;
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Darkness Decrease Increased To: " + GM.DarknessDiv);
        uScript.UpdateTexts(4);
    }
}
