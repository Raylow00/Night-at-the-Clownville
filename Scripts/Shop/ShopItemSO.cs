using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public string itemID;
    public string itemDescription;
    public int itemCost;
    public bool isItemOneTimePurchase;
    public bool purchased;
}
