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
        InventoryManager.OnInventoryChanged += RefreshDisplay;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= RefreshDisplay;
    }

    public void Show(ItemCategory categoryToShow)
    {
        gameObject.SetActive(true);
        currentPage = 0;
        currentCategory = categoryToShow;

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

        filteredItems = InventoryManager.Instance.inventorySlots
                                      .Where(slot => slot.item.category == currentCategory)
                                      .ToList();

        int startIndex = currentPage * itemsPerPage;
        int endIndex = Mathf.Min(startIndex + itemsPerPage, filteredItems.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            InventorySlot inventorySlot = filteredItems[i];
            GameObject slotInstance = Instantiate(inventorySlotPrefab, slotContainer);

            InventorySlotUI slotUI = slotInstance.GetComponent<InventorySlotUI>();
            if (slotUI != null)
            {
                slotUI.SetSlot(inventorySlot);
            }

            TMPro.TextMeshProUGUI quantityText = slotInstance.transform.Find("QuantityText").GetComponent<TMPro.TextMeshProUGUI>();
            if (inventorySlot.item.category == ItemCategory.Food ||
                inventorySlot.item.category == ItemCategory.Medicine ||
                inventorySlot.item.category == ItemCategory.Treat)
            {
                quantityText.gameObject.SetActive(true);
                quantityText.text = $"x{inventorySlot.quantity}";
            }
            else
            {
                quantityText.gameObject.SetActive(false);
            }
        }

        if (leftArrowButton) leftArrowButton.interactable = (currentPage > 0);
        if (rightArrowButton) rightArrowButton.interactable = (startIndex + itemsPerPage < filteredItems.Count);
    }
}