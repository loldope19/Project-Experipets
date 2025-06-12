using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("-- UI References --")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject inventorySlotPrefab;

    public void ShowInventory(ItemCategory categoryToShow)
    {
        inventoryPanel.SetActive(true);
        RefreshInventoryDisplay(categoryToShow);
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }

    private void RefreshInventoryDisplay(ItemCategory categoryToShow)
    {
        foreach (Transform child in contentPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var inventorySlot in InventoryManager.Instance.inventorySlots)
        {
            if (inventorySlot.item.category == categoryToShow)
            {
                GameObject slotInstance = Instantiate(inventorySlotPrefab, contentPanel);

                // --- Finding the components ---
                Image itemIcon = slotInstance.transform.Find("ItemIcon")?.GetComponent<Image>();
                TextMeshProUGUI quantityText = slotInstance.transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();
                Button useButton = slotInstance.transform.Find("UseButton")?.GetComponent<Button>();

                // --- Defensive Checks (will print a clear error if something is wrong) ---
                if (itemIcon == null) Debug.LogError("Could not find 'ItemIcon' child with an Image component in the prefab!");
                if (quantityText == null) Debug.LogError("Could not find 'QuantityText' child with a TextMeshProUGUI component in the prefab!");
                if (useButton == null) Debug.LogError("Could not find a Button component on the root of the prefab!");

                // --- Populate the UI (only if the components were found) ---
                if (itemIcon != null)
                    itemIcon.sprite = inventorySlot.item.itemIcon;

                if (quantityText != null)
                    quantityText.text = $"x{inventorySlot.quantity}";

                if (useButton != null)
                {
                    ItemData currentItem = inventorySlot.item;
                    useButton.onClick.AddListener(() => {
                        InventoryManager.Instance.UseItem(currentItem);
                        RefreshInventoryDisplay(categoryToShow);
                    });
                }
            }
        }
    }
}
