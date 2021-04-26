using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingSpeed : Item
{
    public override void Craft(int count) {
        PlayerMovement controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        controller.runspeed += 2f*count;
        UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
        uScript.DisplayUpgrade("Movement Speed Increased To: " + controller.runspeed);
        uScript.UpdateTexts(1);
    }
}
