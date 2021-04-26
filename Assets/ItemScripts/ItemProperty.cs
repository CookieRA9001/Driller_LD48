using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

public class ItemProperty : ScriptableObject
{
    public int[] OrePrice = {0, 0, 0, 0, 0};
    public float priceIncrease = 0;
    public bool increasePercent = false;
    public int id;
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
    public Item itemClass;
    public int[] BasePrice = {0, 0, 0, 0, 0};

}
