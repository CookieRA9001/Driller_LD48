using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSteel : Item{
    public override void Craft(int count) {
        GameManager GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GM.killAll = true;
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Your drill rowers with POWER!\nYou can drill steel enemies!");
        EnemySpriteGroup[] allPortal = FindObjectsOfType<EnemySpriteGroup>();
        foreach (EnemySpriteGroup e in allPortal)
        {
            e.canDie = true;
        }
        uScript.UpdateTexts(7);
    }
}
