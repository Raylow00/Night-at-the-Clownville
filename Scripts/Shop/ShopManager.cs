using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string itemID;
        public GameObject itemHandler;    // This would the item handler - that enables or disables the item purchased
    }

    [Header("Coins")]
    [SerializeField] private PlayerStatsSO playerStats;
    [SerializeField] private PlayerCoinSystem playerCoinSystem;

    [Header("UI GameObjects")]
    [SerializeField] private GameObject drinksTabButton;
    [SerializeField] private GameObject drinksScrollRect;
    [SerializeField] private GameObject weaponsScrollRect;
    [SerializeField] private GameObject skinsScrollRect;
    [SerializeField] private ShopItemSO[] shopItemsSO;
    [SerializeField] private ItemTemplate[] itemTemplates;
    [SerializeField] private GameObject[] shopPanelsGO;
    [SerializeField] private Button[] purchaseButtons;

    [Header("Item Unlock Handler")]
    [SerializeField] private Item[] items;

    [Header("Events")]
    [SerializeField] private VoidEvent onItemPurchasedEvent;

    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }

        LoadItemPanels();

        ShopCheckIfPurchasable();
    }

    public void OpenDrinksScrollRect()
    {
        drinksScrollRect.SetActive(true);
        weaponsScrollRect.SetActive(false);
        skinsScrollRect.SetActive(false);
    }

    public void OpenWeaponsScrollRect()
    {
        drinksScrollRect.SetActive(false);
        weaponsScrollRect.SetActive(true);
        skinsScrollRect.SetActive(false);
    }

    public void OpenSkinsScrollRect()
    {
        drinksScrollRect.SetActive(false);
        weaponsScrollRect.SetActive(false);
        skinsScrollRect.SetActive(true);
    }

    public void AddCoins()
    {
        playerStats.playerCurrentCoins = playerCoinSystem.playerCoin.CollectCoins(1);

        ShopCheckIfPurchasable();
    }

    public void LoadItemPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            itemTemplates[i].titleText.text = shopItemsSO[i].itemName;
            itemTemplates[i].descText.text = shopItemsSO[i].itemDescription;
            itemTemplates[i].costText.text = "R Coins: " + shopItemsSO[i].itemCost.ToString();
        }

        OpenDrinksScrollRect();
    }

    public void ShopCheckIfPurchasable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if(playerStats.playerCurrentCoins < shopItemsSO[i].itemCost || (shopItemsSO[i].isItemOneTimePurchase && shopItemsSO[i].purchased))
            {
                purchaseButtons[i].interactable = false;
            }
            else if(playerStats.playerCurrentCoins >= shopItemsSO[i].itemCost)
            {
                purchaseButtons[i].interactable = true;
            }
        }
    }

    public bool PlayerCheckIfPurchasable(string id)
    {
        bool purchasable = false;

        foreach(Item item in items)
        {
            if(item.itemID == id)
            {
                purchasable = item.itemHandler.GetComponent<IPurchasable>().CheckIfPurchasable(id);
            }
        }

        return purchasable;
    }

    public void PurchaseItem(int buttonNumber)
    {
        bool isPurchasable = PlayerCheckIfPurchasable(shopItemsSO[buttonNumber].itemID);

        if(isPurchasable)
        {
            playerStats.playerCurrentCoins = playerCoinSystem.playerCoin.UseCoins(shopItemsSO[buttonNumber].itemCost);

            shopItemsSO[buttonNumber].purchased = true;

            ShopCheckIfPurchasable();

            UnlockItemPurchased(shopItemsSO[buttonNumber].itemID);

            onItemPurchasedEvent.Raise();
        }
    }

    public void UnlockItemPurchased(string id)
    {
        foreach(Item item in items)
        {
            if(item.itemID == id)
            {
                item.itemHandler.GetComponent<IPurchasable>().UnlockItem(id);
            }
        }
    }
}
