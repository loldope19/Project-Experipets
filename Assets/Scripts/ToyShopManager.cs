using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToyShopManager : MonoBehaviour
{
    [Header("-- Shop Configuration --")]
    public List<ItemData> itemsForSale;

    [Header("-- UI References --")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject shopItemSlotPrefab;

    void Start()
    {
        GenerateShopItems();
    }

    private void GenerateShopItems()
    {
        foreach (var itemData in itemsForSale)
        {
            GameObject slotInstance = Instantiate(shopItemSlotPrefab, shopPanel.transform);

            // --- Find the UI elements within the instantiated prefab ---
            // Note: You might need to adjust these paths based on your prefab's hierarchy.
            // e.g., slotInstance.transform.Find("ItemIcon").GetComponent<Image>();
            Image itemIcon = slotInstance.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.sprite = slotInstance.transform.Find("ItemIcon").GetComponent<Image>().sprite;
            TextMeshProUGUI itemNameText = slotInstance.transform.Find("ItemNameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemCostText = slotInstance.transform.Find("ItemCostText").GetComponent<TextMeshProUGUI>();
            Button buyButton = slotInstance.transform.Find("BuyButton").GetComponent<Button>();

            itemIcon.sprite = itemData.itemIcon;
            itemNameText.text = itemData.itemName;
            itemCostText.text = $"{itemData.cost}G";

            buyButton.onClick.AddListener(() => BuyItem(itemData));
        }
    }

    public void BuyItem(ItemData itemData)
    {
        Debug.Log($"Attempting to buy {itemData.itemName} for {itemData.cost}G.");

        bool success = CurrencyManager.Instance.SpendCurrency(itemData.cost);

        if (success)
        {
            InventoryManager.Instance.AddItem(itemData);
            Debug.Log($"Successfully purchased {itemData.itemName}!");
            // Optional: Play a "purchase successful" sound effect.
        }
        else
        {
            Debug.Log($"Purchase failed. Not enough currency.");
            // Optional: Play a "purchase failed" sound effect.
        }
    }
}
