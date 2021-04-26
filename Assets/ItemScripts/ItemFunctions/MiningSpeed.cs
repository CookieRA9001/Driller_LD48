using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningSpeed : Item
{
    public override void Craft(int count) {
        PlayerMovement controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        controller.SpriteAnimator.speed += 0.1f*count;
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Drill Speed Increased To: " + controller.SpriteAnimator.speed);
        uScript.UpdateTexts(0);
    }
}
