using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScript : MonoBehaviour
{
    public PriceFieldScript pField;
    public int[] OrePrice;
    public int itemType;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public ItemProperty linkedItem;

    public ItemMenuManager manager;
    public TMP_InputField inputField;
    public Button craftButton;
    public Image craftButtonImage;
    public Color cantAfford;
    public Color canAfford;
    public Color notUsed;
    public Color cantCraft;

    private PlayerStats pStats;
    private bool canBuy;
    private Color defaultColor = new Color(1, 1, 1, 1);
    private int count = 1;
    private AudioManeger audioP;

    private void Start()
    {
        GetPlayer();
    }

    public void UpdatePrice() {
        canBuy = true;
        count = 1;
        if (inputField.text != "") {
            count = int.Parse(inputField.text);
            if (count < 1) count = 1;
            inputField.text = count.ToString();
        }

        for (int i = 0; i < OrePrice.Length; i++) {
            if (pStats == null) GetPlayer();
            pField.OrePrices[i].text = (FinalPrice(i, count, true)).ToString();
            if (OrePrice[i] == 0) {
                pField.OrePrices[i].color = notUsed;
            } else if (pStats.ores[i] < (FinalPrice(i, count, true))) {
                pField.OrePrices[i].color = cantAfford;
                canBuy = false;
            } else {
                pField.OrePrices[i].color = canAfford;
            }
        }

        if (canBuy == false) {
            craftButton.interactable = false;
            craftButtonImage.color = cantCraft;
        } else {
            craftButton.interactable = true;
            craftButtonImage.color = defaultColor;
        }
    }

    public void Craft() {
        UpdatePrice();
        if (canBuy) {
            for (int i = 0; i < OrePrice.Length; i++) {
                pStats.ores[i] -= FinalPrice(i, count, true);
            }
            manager.CraftItem(itemType, count);
            if (linkedItem.priceIncrease != 0) {
                for (int i = 0; i < OrePrice.Length; i++) {
                    linkedItem.OrePrice[i] = FinalPrice(i, count+1, false);
                    OrePrice[i] = linkedItem.OrePrice[i];
                }
            }
        }
        PlaySound(0);
        UpdatePrice();
    }

    public void PlaySound(int i) {
        if (audioP == null) audioP = FindObjectOfType<AudioManeger>();
        if (i == 0) audioP.Play("Upgrade");
        else audioP.Play("Click");
    }

    public int FinalPrice(int i, int count, bool sum) {
        if (OrePrice[i] != 0 && linkedItem.priceIncrease != 0 && count > 0) {
            int price = OrePrice[i];
            int pricel = price;
            if (linkedItem.increasePercent) {
                for (int f = 1; f < count; f++) {
                    price = (int)Mathf.Round(price*linkedItem.priceIncrease);
                    pricel += price;
                }
            } else {
                for (int f = 1; f < count; f++) {
                    price += (int)Mathf.Round(linkedItem.priceIncrease);
                    pricel += price;
                }
            }
            if (sum) return pricel;
            else return price;
        } else return OrePrice[i]*count;
    }

    void GetPlayer() {
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
}
