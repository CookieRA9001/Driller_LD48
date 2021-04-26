using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ItemMenuManager : MonoBehaviour
{
    public ItemProperty[] Items;
    public ItemScript[] iScripts;
    public int Page;
    public int PageCount;
    public Button BackButton;
    public Button NextButton;
    public Image BackButtonImage;
    public Image NextButtonImage;
    public Color PageButtonDisabled;
    private Color defaultColor = new Color(1, 1, 1, 1);

    private Dictionary<int, ItemProperty> ids = new Dictionary<int, ItemProperty>();
    private PlayerStats pStats;

    private void Start()
    {
        PageCount = (int)Mathf.Ceil(Items.Length / 4f);
        pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        LoadItems();
        UpdateButtons();
        UpdateIDs();
    }

    public void ClearPrices() {
        for (int i = 0; i < Items.Length; i++) {
            for (int f = 0; f < Items[i].OrePrice.Length; f++) {
                Items[i].OrePrice[f] = Items[i].BasePrice[f];
            }
        }
    }

    public void LoadItems() {
        int startIndex = (Page-1)*4;
        int count = 4;
        if ((Items.Length-startIndex) < count) count = Items.Length-startIndex;
        for (int i = 0; i < 4; i++) {
            if (i >= count) {
                iScripts[i].gameObject.SetActive(false);
            } else {
                iScripts[i].linkedItem = Items[startIndex+i];
                iScripts[i].gameObject.SetActive(true);
                iScripts[i].OrePrice = Items[startIndex+i].OrePrice;
                iScripts[i].UpdatePrice();
                iScripts[i].itemType = Items[startIndex+i].id;
                iScripts[i].itemImage.sprite = Items[startIndex+i].itemImage;
                iScripts[i].itemName.text = Items[startIndex+i].itemName;
                iScripts[i].itemDescription.text = Items[startIndex+i].itemDescription;
            }
        }
    }

    public void NextPage() {
        if ((Page+1) <= PageCount) {
            Page+=1;
            LoadItems();
            UpdateButtons();
            FindObjectOfType<AudioManeger>().Play("Click");
        }
    }

    public void PreviousPage() {
        if ((Page-1) >= 1) {
            Page-=1;
            LoadItems();
            UpdateButtons();
            FindObjectOfType<AudioManeger>().Play("Click");
        }
    }

    public void UpdateButtons() {
        if (Page+1 > PageCount) {
            NextButton.interactable = false;
            NextButtonImage.color = PageButtonDisabled;
        } else {
            NextButton.interactable = true;
            NextButtonImage.color = defaultColor;
        }

        if (Page-1 <= 0) {
            BackButton.interactable = false;
            BackButtonImage.color = PageButtonDisabled;
        } else {
            BackButton.interactable = true;
            BackButtonImage.color = defaultColor;
        }
    }

    public void UpdateIDs() {
        for (int i = 0; i < Items.Length; i++) {
            ids.Add(Items[i].id, Items[i]);
        }
    }

    public void CraftItem(int type, int count) {
        ids[type].itemClass.Craft(count);
        UpdatePrices();
        pStats.UpdateOreCounter();
        FindObjectOfType<AudioManeger>().Play("Click");
        FindObjectOfType<AudioManeger>().Play("Upgrade");
    }

    public void UpdatePrices() {
        for (int i = 0; i < iScripts.Length; i++) {
            iScripts[i].UpdatePrice();
        }
    }

    public void ExitFromMenu() {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        FindObjectOfType<AudioManeger>().Play("Click");
    }
}

[Serializable]
public class ItemClass
{
    public int[] OrePrice = {0, 0, 0, 0, 0};
    public int itemType;
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
}
