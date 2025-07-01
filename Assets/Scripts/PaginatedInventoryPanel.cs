using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PaginatedInventoryPanel : MonoBehaviour
{
    [Header("UI References (for this panel only)")]
    [SerializeField] private Transform slotContainer;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;

    [Header("Pagination Settings")]
    [SerializeField] private int itemsPerPage = 3;
    private int currentPage = 0;
    private List<InventorySlot> filteredItems;
    private ItemCategory currentCategory;

    private void OnEnable()
    {
        // Optional: Add listeners for when the main inventory changes, to auto-refresh
    }

    public void Show(ItemCategory categoryToShow)
    {
        gameObject.SetActive(true);
        currentPage = 0;
        currentCategory = categoryToShow;

        filteredItems = InventoryManager.Instance.inventorySlots
                        .Where(slot => slot.item.category == categoryToShow)
                        .ToList();
        Debug.Log($"Found {filteredItems.Count} items in the '{categoryToShow}' category.");

        RefreshDisplay();   
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void NextPage()
    {
        int maxPages = Mathf.CeilToInt((float)filteredItems.Count / itemsPerPage) - 1;
        if (currentPage < maxPages)
        {
            currentPage++;
            RefreshDisplay();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            RefreshDisplay();
        }
    }

    private void RefreshDisplay()
    {
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        int startIndex = currentPage * itemsPerPage;
        int endIndex = Mathf.Min(startIndex + itemsPerPage, filteredItems.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            InventorySlot inventorySlot = filteredItems[i];
            GameObject slotInstance = Instantiate(inventorySlotPrefab, slotContainer);

            Image itemIcon = slotInstance.transform.Find("ItemIcon").GetComponent<Image>();
            TMPro.TextMeshProUGUI quantityText = slotInstance.transform.Find("QuantityText").GetComponent<TMPro.TextMeshProUGUI>();
            Button useButton = slotInstance.transform.Find("UseButton").GetComponent<Button>();

            itemIcon.sprite = inventorySlot.item.itemIcon;
            quantityText.text = $"x{inventorySlot.quantity}";

            ItemData currentItem = inventorySlot.item;

            Debug.Log(itemIcon.sprite.name + ": " + quantityText.text);
            useButton.onClick.AddListener(() => {
                InventoryManager.Instance.UseItem(currentItem);
                Show(currentCategory);
            });
        }

        if (leftArrowButton) leftArrowButton.interactable = (currentPage > 0);
        if (rightArrowButton) rightArrowButton.interactable = (startIndex + itemsPerPage < filteredItems.Count);
    }
}
