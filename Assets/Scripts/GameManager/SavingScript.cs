using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingScript : MonoBehaviour
{
    public bool SavingEnabled = true;
    private GameManager gameManager;
    public ItemMenuManager itemMenuManager;
    private PlayerStats playerStats;
    private PlayerMovement playerMovement;
    private UpgradeScript upgradeScript;
    public GameObject CheckPointObjectRaf;
    void Start()
    {
        LoadDependencies();
        InvokeRepeating("Save", 10, 30);
        //Load();
    }

    void LoadDependencies() {
        gameManager = gameObject.GetComponent<GameManager>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        upgradeScript = gameObject.GetComponent<UpgradeScript>();
    }

    public void Load() {
        if (!SavingEnabled) return;
        Debug.Log("Load");
        if (playerStats == null) {
            LoadDependencies();
        }

        playerStats.floor = PlayerPrefs.GetInt("Floor");
        gameManager.floor = PlayerPrefs.GetInt("Floor");
        playerStats.UpdateFloor();

        playerStats.ores[0] = PlayerPrefs.GetInt("Copper");
        playerStats.ores[1] = PlayerPrefs.GetInt("Iron");
        playerStats.ores[2] = PlayerPrefs.GetInt("Gold");
        playerStats.ores[3] = PlayerPrefs.GetInt("Diamond");
        playerStats.ores[4] = PlayerPrefs.GetInt("Demonsteel");
        playerStats.UpdateOreCounter();

        playerStats.DrillPower = 0;
        playerStats.getDrillPower(PlayerPrefs.GetInt("DrillPower"));

        playerMovement.SpriteAnimator.speed = PlayerPrefs.GetFloat("DrillSpeed");
        playerMovement.runspeed = PlayerPrefs.GetFloat("MoveSpeed");
        gameManager.oreChancesMultiplier = PlayerPrefs.GetFloat("OreChance");
        playerStats.typeToAmount[3] = PlayerPrefs.GetInt("FortuneCopper");
        playerStats.typeToAmount[8] = PlayerPrefs.GetInt("FortuneIron");
        playerStats.typeToAmount[6] = PlayerPrefs.GetInt("FortuneGold");
        playerStats.typeToAmount[5] = PlayerPrefs.GetInt("FortuneDiamond");
        playerStats.typeToAmount[4] = PlayerPrefs.GetInt("FortuneDemonsteel");

        gameManager.DarknessDiv = PlayerPrefs.GetFloat("Darkness");

        playerStats.Health = PlayerPrefs.GetInt("Health");
        playerStats.MaxHealth = PlayerPrefs.GetInt("MaxHealth");
        playerStats.UpdateHealth();
        upgradeScript.UpdateTexts(-1);

        gameManager.SpawnCustomFromNew(PlayerPrefs.GetInt("FloorInType"), PlayerPrefs.GetInt("Floor"));

        for (int i = 0; i < itemMenuManager.Items.Length; i++) {
            for (int f = 0; f < 5; f++) {
                PlayerPrefs.GetInt("Item" + itemMenuManager.Items[i].id + "Price" + f, itemMenuManager.Items[i].OrePrice[f]);
            }
        }
        itemMenuManager.LoadItems();

        if (PlayerPrefs.GetInt("hasCheckPoint") == 1) {
            Vector3 tp = new Vector3(PlayerPrefs.GetFloat("CheckpointPositionX") + 100, PlayerPrefs.GetFloat("CheckpointPositionY"), PlayerPrefs.GetFloat("CheckpointPositionZ"));
            GameObject CPoint = Instantiate(CheckPointObjectRaf,tp, Quaternion.identity);
            CheckPoint cp = CPoint.GetComponent<CheckPoint>();
            if(playerStats.chechPoint != null) Destroy(playerStats.chechPoint.gameObject);

            cp.sr = cp.gameObject.GetComponent<SpriteRenderer>();
            cp.dp = PlayerPrefs.GetInt("CheckpointDrillPower");

            for (int i = 0; i < 5; i++) cp.ores[i] = PlayerPrefs.GetInt("CheckpointOres" + i);
            for (int i = 0; i < 5; i++) cp.fortune[i] = PlayerPrefs.GetInt("CheckpointFortunes" + i);

            cp.MaxHealth = PlayerPrefs.GetInt("CheckpointMaxHealth");
            cp.speed = PlayerPrefs.GetFloat("CheckpointRunspeed");
            cp.mining_speed = PlayerPrefs.GetFloat("CheckpointDrillspeed");
            cp.floor = PlayerPrefs.GetInt("CheckpointFloor");
            cp.FloorType = PlayerPrefs.GetInt("CheckpointFloorType");
            cp.flashlight = PlayerPrefs.GetFloat("CheckpointDarkness");
            cp.oCM = PlayerPrefs.GetFloat("CheckpointOreChance");

            playerStats.isCheckPoint = true;

            playerStats.chechPoint = cp;
        }
    }

    public void Save() {
        if (!SavingEnabled) return;
        Debug.Log("Save");
        if (gameManager == null) {
            LoadDependencies();
        }
        PlayerPrefs.SetInt("Saved", 1);

        PlayerPrefs.SetInt("Floor", playerStats.floor);
        PlayerPrefs.SetInt("Copper", playerStats.ores[0]);
        PlayerPrefs.SetInt("Iron", playerStats.ores[1]);
        PlayerPrefs.SetInt("Gold", playerStats.ores[2]);
        PlayerPrefs.SetInt("Diamond", playerStats.ores[3]);
        PlayerPrefs.SetInt("Demonsteel", playerStats.ores[4]);
        PlayerPrefs.SetInt("DrillPower", playerStats.DrillPower);
        PlayerPrefs.SetFloat("DrillSpeed", playerMovement.SpriteAnimator.speed);
        PlayerPrefs.SetFloat("MoveSpeed", playerMovement.runspeed);
        PlayerPrefs.SetFloat("OreChance", gameManager.oreChancesMultiplier);
        PlayerPrefs.SetInt("FortuneCopper", playerStats.typeToAmount[3]);
        PlayerPrefs.SetInt("FortuneIron", playerStats.typeToAmount[8]);
        PlayerPrefs.SetInt("FortuneGold", playerStats.typeToAmount[6]);
        PlayerPrefs.SetInt("FortuneDiamond", playerStats.typeToAmount[5]);
        PlayerPrefs.SetInt("FortuneDemonsteel", playerStats.typeToAmount[4]);
        PlayerPrefs.SetFloat("Darkness", gameManager.DarknessDiv);
        PlayerPrefs.SetInt("Health", playerStats.Health);
        PlayerPrefs.SetInt("MaxHealth", playerStats.MaxHealth);

        if (playerStats.FloorIn != null) {
            PlayerPrefs.SetInt("FloorInType", playerStats.FloorIn.GetComponent<FloorParentScript>().floorScript.type);
        }

        for (int i = 0; i < itemMenuManager.Items.Length; i++) {
            for (int f = 0; f < 5; f++) {
                PlayerPrefs.SetInt("Item" + itemMenuManager.Items[i].id + "Price" + f, itemMenuManager.Items[i].OrePrice[f]);
            }
        }

        if (playerStats.isCheckPoint) {
            PlayerPrefs.SetInt("hasCheckPoint", 1);

            PlayerPrefs.SetFloat("CheckpointPositionX", playerStats.chechPoint.transform.position.x);
            PlayerPrefs.SetFloat("CheckpointPositionY", playerStats.chechPoint.transform.position.y);
            PlayerPrefs.SetFloat("CheckpointPositionZ", playerStats.chechPoint.transform.position.z);

            PlayerPrefs.SetInt("CheckpointDrillPower", playerStats.chechPoint.dp);
            
            for (int i = 0; i < 5; i++) PlayerPrefs.SetInt("CheckpointOres" + i, playerStats.chechPoint.ores[i]);
            for (int i = 0; i < 5; i++) PlayerPrefs.SetInt("CheckpointFortunes" + i, playerStats.chechPoint.fortune[i]);

            PlayerPrefs.SetInt("CheckpointMaxHealth", playerStats.chechPoint.MaxHealth);
            PlayerPrefs.SetFloat("CheckpointRunspeed", playerStats.chechPoint.speed);
            PlayerPrefs.SetFloat("CheckpointDrillspeed", playerStats.chechPoint.mining_speed);
            PlayerPrefs.SetInt("CheckpointFloor", playerStats.chechPoint.floor);
            PlayerPrefs.SetInt("CheckpointFloorType", playerStats.chechPoint.FloorType);
            PlayerPrefs.SetFloat("CheckpointDarkness", playerStats.chechPoint.flashlight);
            PlayerPrefs.SetFloat("CheckpointOreChance", playerStats.chechPoint.oCM);

        } else {
            PlayerPrefs.SetInt("hasCheckPoint", -1);
        }
    }

    public void WriteSave() {
        PlayerPrefs.Save();
    }
}
