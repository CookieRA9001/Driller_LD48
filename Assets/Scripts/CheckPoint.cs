using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour{
    private static int[] typeToOre = {3,8,6,5,4};
    public int[] ores = { 0, 0, 0, 0, 0 };
    public int[] fortune = { 0, 0, 0, 0, 0 };
    public int MaxHealth, dp;
    public float speed, mining_speed, flashlight, oCM;
    public int FloorType;
    public int floor;
    public bool killall;

    private float framTime = 0.5f;
    public Sprite fram1, fram2;
    public SpriteRenderer sr;
    public void CreateCheckPoint(GameObject p, int FT) {
        GameManager gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        PlayerStats playerStats = p.GetComponent<PlayerStats>(); ;
        PlayerMovement playerMove = p.GetComponent<PlayerMovement>();
        dp = playerStats.DrillPower;
        for (int i = 0; i < 5; i++) ores[i] = playerStats.ores[i];
        for (int i = 0; i < 5; i++) fortune[i] = playerStats.typeToAmount[typeToOre[i]];
        MaxHealth = playerStats.MaxHealth;
        speed = playerMove.runspeed;
        mining_speed = playerMove.SpriteAnimator.speed;
        floor = playerStats.floor;
        FloorType = FT;
        flashlight = gManager.DarknessDiv;
        oCM = gManager.oreChancesMultiplier;
        killall = gManager.killAll;
        playerStats.isCheckPoint = true;
    }
    public void Respawn(GameObject p) {
        GameManager gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Vector3 sp = gManager.spawnPos; sp.y -= 0.6f;
        transform.position = sp;
        PlayerStats playerStats = p.GetComponent<PlayerStats>(); ;
        PlayerMovement playerMove = p.GetComponent<PlayerMovement>();
        playerMove.runspeed = speed;
        playerMove.SpriteAnimator.speed = mining_speed;
        playerStats.DrillPower = 0;
        playerStats.getDrillPower(dp);
        playerStats.MaxHealth = MaxHealth;
        playerStats.Health = 1;
        playerStats.HealthUp(0);
        for (int i = 0; i < 5; i++) playerStats.ores[i] = ores[i];
        for (int i = 0; i < 5; i++) playerStats.typeToAmount[typeToOre[i]] = fortune[i];
        playerStats.UpdateOreCounter();
        playerStats.floor = floor;
        gManager.killAll = killall;
        playerStats.isCheckPoint = false;
        gManager.DarknessDiv = flashlight;
        gManager.oreChancesMultiplier = oCM;
        //recreate floor
        gManager.SpawnCustomFromNew(FloorType, floor);
        playerStats.UpdateFloor();
        UpgradeScript uScript = gManager.GetComponent<UpgradeScript>();
        uScript.UpdateTexts(-1);
    }
    public void Update(){
        if (framTime > 0){
            framTime -= Time.deltaTime;
            if (framTime <= 0) {
                if (sr.sprite==fram2){
                    sr.sprite = fram1;
                }
                else {
                    sr.sprite = fram2;
                }
                framTime = 0.5f;
            }
        }
    }
}
