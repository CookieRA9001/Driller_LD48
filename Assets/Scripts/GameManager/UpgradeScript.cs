using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeScript : MonoBehaviour
{
    public TextMeshProUGUI drillSpeedText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI FortuneText;
    public TextMeshProUGUI OreAmountText;
    public TextMeshProUGUI DarknessText;
    public TextMeshProUGUI MaxHealthText;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI UpgradedText;
    public TextMeshProUGUI KillAll;
    private Animator anim;
    private PlayerStats pStats;
    private PlayerMovement pMovement;
    private GameManager gManager;

    private void Start(){
        anim = UpgradedText.gameObject.GetComponent<Animator>();
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        pMovement = pStats.gameObject.GetComponent<PlayerMovement>();
        gManager = gameObject.GetComponent<GameManager>();
        UpdateTexts(-1);
    }

    public void DisplayUpgrade(string text) {
        UpgradedText.text = text;
        anim.Play("Upgraded");
    }

    public void UpdateTexts(int i) {
        switch (i) {
            case 0: {
                drillSpeedText.text = ("Drill Speed: " + pMovement.SpriteAnimator.speed);
                break;
            };
            case 1: {
                moveSpeedText.text = ("Movement Speed: " + pMovement.runspeed);
                break;
            };
            case 2: {
                FortuneText.text = ("Ore Amount Chance: " + gManager.oreChancesMultiplier);
                break;
            };
            case 3: {
                OreAmountText.text = ("Ore Multiplier: " + pStats.typeToAmount[3] + ", " + pStats.typeToAmount[8] + ", " + pStats.typeToAmount[6] + ", " + pStats.typeToAmount[5] + ", " + pStats.typeToAmount[4]);
                break;
            };
            case 4: {
                DarknessText.text = ("Darkness Decrease: " + gManager.DarknessDiv);
                break;
            };
            case 5: {
                MaxHealthText.text = ("Max Health: " + pStats.MaxHealth);
                break;
            };
            case 6: {
                HealthText.text = ("Health: " + pStats.Health);
                break;
            };
            case 7: {
                KillAll.text = ("Break Steel: " + gManager.killAll);
                break;
            };
            default: {
                drillSpeedText.text = ("Drill Speed: " + pMovement.SpriteAnimator.speed);
                moveSpeedText.text = ("Movement Speed: " + pMovement.runspeed);
                FortuneText.text = ("Ore Amount Chance: " + gManager.oreChancesMultiplier);
                OreAmountText.text = ("Ore Multiplier: " + pStats.typeToAmount[3] + ", " + pStats.typeToAmount[8] + ", " + pStats.typeToAmount[6] + ", " + pStats.typeToAmount[5] + ", " + pStats.typeToAmount[4]);
                DarknessText.text = ("Darkness Decrease: " + gManager.DarknessDiv);
                MaxHealthText.text = ("Max Health: " + pStats.MaxHealth);
                HealthText.text = ("Health: " + pStats.Health);
                KillAll.text = ("Break Steel: " + gManager.killAll);
                break;
            };
        }
    }
}
