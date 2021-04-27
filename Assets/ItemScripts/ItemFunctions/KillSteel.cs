using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSteel : Item{
    public override void Craft(int count) {
        GameManager GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GM.killAll = true;
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Your drill rowers with POWER!\nYou can drill steel enemies!");
        //GameObject[] list = GameObject.FindGameObjectsWithTag("Enemy");
        //for (int i = 0; i < list.Length; i++) {
        //    list[i].GetComponent<EnemySpriteGroup>().canDie = true;
        //}
        EnemyStats[] allPortal = FindObjectsOfType<EnemyStats>();
        foreach (EnemyStats e in allPortal)
        {
            e.canDie = true;
        }
        uScript.UpdateTexts(7);
    }
}
