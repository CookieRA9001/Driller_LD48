using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgadeLogic : MonoBehaviour
{
    private GameObject player;
    private PlayerStats playerStats;
    private PlayerMovement controller;
    public int key;
    public int price = -1;
    public int recorce;
    public bool started = false;
    private GameObject priceTag;
    private GameObject T;
    public Sprite s;
    public void Setup(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        priceTag = new GameObject("Price Tag");
        priceTag.transform.parent = transform;
        SpriteRenderer sr = priceTag.AddComponent<SpriteRenderer>();
        sr.sprite = s;
        priceTag.transform.position = new Vector3(transform.position.x+0.55f, transform.position.y + 0.9f, transform.position.z);
        
        T = new GameObject();
        T.transform.parent = transform;
        TextMeshPro TMP = T.AddComponent<TextMeshPro>();
        TMP.text = price.ToString();
        TMP.fontSize = 6;
        TMP.rectTransform.sizeDelta = new Vector2(2, 1);
        T.transform.position = new Vector3(transform.position.x+0.15f, transform.position.y + 0.75f, transform.position.z);

        started = true;
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (started){
            if (collider.CompareTag("Player"))
                if (price <= playerStats.ores[recorce]){
                    playerStats.ores[recorce] -= price;
                    UpgradeScript uScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeScript>();
                    switch (key) {
                        case 0: {
                            playerStats.getDrillPower(25);
                            
                            uScript.DisplayUpgrade("Drill Power Increased To: " + playerStats.DrillPower);
                        break;}
                        case 1: {
                             playerStats.HealthUp(1);

                             uScript.DisplayUpgrade("Health Increased To: " + playerStats.Health);
                             uScript.UpdateTexts(6);
                        break;}
                        case 2: {
                             controller.runspeed += 2;

                             uScript.DisplayUpgrade("Movement Speed Increased To: " + controller.runspeed);
                             uScript.UpdateTexts(1);
                        break;}
                        case 3: {
                            controller.SpriteAnimator.speed += 0.1f;

                            uScript.DisplayUpgrade("Drill Speed Increased To: " + controller.SpriteAnimator.speed);
                            uScript.UpdateTexts(0);
                        break;}
                        case 4: { 
                            playerStats.typeToAmount[3] += 1;

                            uScript.DisplayUpgrade("Ore Multiplier Increased To: " + playerStats.typeToAmount[3] + ", " + playerStats.typeToAmount[8] + ", " + playerStats.typeToAmount[6] + ", " + playerStats.typeToAmount[5] + ", " + playerStats.typeToAmount[4]);
                            uScript.UpdateTexts(3);
                        break;}
                        case 5: { 
                            playerStats.typeToAmount[8] += 1;

                            uScript.DisplayUpgrade("Ore Multiplier Increased To: " + playerStats.typeToAmount[3] + ", " + playerStats.typeToAmount[8] + ", " + playerStats.typeToAmount[6] + ", " + playerStats.typeToAmount[5] + ", " + playerStats.typeToAmount[4]);
                            uScript.UpdateTexts(3);
                        break;}
                        case 6: { 
                            playerStats.MaxHealth += 1;
                            playerStats.HealthUp(1);

                            uScript.DisplayUpgrade("Max Health Increased To: " + playerStats.MaxHealth);
                            uScript.UpdateTexts(5);
                            uScript.UpdateTexts(6);
                        break;}
                        case 7: { 
                            playerStats.typeToAmount[6] += 1;

                            uScript.DisplayUpgrade("Ore Multiplier Increased To: " + playerStats.typeToAmount[3] + ", " + playerStats.typeToAmount[8] + ", " + playerStats.typeToAmount[6] + ", " + playerStats.typeToAmount[5] + ", " + playerStats.typeToAmount[4]);
                            uScript.UpdateTexts(3);
                        break;}
                        case 8: { 
                            playerStats.typeToAmount[5] += 1;

                            uScript.DisplayUpgrade("Ore Multiplier Increased To: " + playerStats.typeToAmount[3] + ", " + playerStats.typeToAmount[8] + ", " + playerStats.typeToAmount[6] + ", " + playerStats.typeToAmount[5] + ", " + playerStats.typeToAmount[4]);
                            uScript.UpdateTexts(3);
                        break;}
                        case 9: { 
                            playerStats.typeToAmount[4] += 1;

                            uScript.DisplayUpgrade("Ore Multiplier Increased To: " + playerStats.typeToAmount[3] + ", " + playerStats.typeToAmount[8] + ", " + playerStats.typeToAmount[6] + ", " + playerStats.typeToAmount[5] + ", " + playerStats.typeToAmount[4]);
                            uScript.UpdateTexts(3);
                        break;}
                    }
                    playerStats.UpdateOreCounter();
                    FindObjectOfType<AudioManeger>().Play("Upgrade");
                    Destroy(gameObject);
                }
                else if(playerStats.DamageTimer<=0) {
                    playerStats.TakeDamage(1);
                    if (transform.position.x - player.transform.position.x > 0){
                        controller.KnockBackRight(10);
                    }
                    else{
                        controller.KnockBackLeft(10);
                    }
                    controller.KnockBackDown(1);
                }
        }  
    }
}
