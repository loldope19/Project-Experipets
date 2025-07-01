using UnityEngine;

public class PetCareUIManager : MonoBehaviour
{
    [Header("Sub-Menu Panels")]
    [SerializeField] private GameObject feedSubMenu;
    [SerializeField] private GameObject cleanSubMenu;
    [SerializeField] private GameObject playSubMenu;
    [SerializeField] private GameObject decorateSubMenu;
    // Add other sub-menus here as you build them

    [Header("Specialized Inventory Panels")]
    [SerializeField] private PaginatedInventoryPanel foodInventoryPanel;
    [SerializeField] private PaginatedInventoryPanel cleanInventoryPanel;
    [SerializeField] private PaginatedInventoryPanel playInventoryPanel;
    [SerializeField] private PaginatedInventoryPanel decorateInventoryPanel;

    private void Start()
    {
        HideAllSubMenus();
    }

    private void HideAllSubMenus()
    {
        feedSubMenu.SetActive(false);
        cleanSubMenu.SetActive(false);
        playSubMenu.SetActive(false);
        decorateSubMenu.SetActive(false);
    }

    private void HideAllPanels()
    {
        foodInventoryPanel.gameObject.SetActive(false);
        cleanInventoryPanel.gameObject.SetActive(false);
        playInventoryPanel.gameObject.SetActive(false);
        decorateInventoryPanel.gameObject.SetActive(false);
    }

    // -- Functions for Main Toolbar Buttons --
    public void OnFeedCategoryClicked()
    {
        HideAllSubMenus();
        if (feedSubMenu.activeSelf) feedSubMenu.SetActive(false);
        else feedSubMenu.SetActive(true);
    }

    public void OnCleanCategoryClicked()
    {
        HideAllSubMenus();
        if (cleanSubMenu.activeSelf) cleanSubMenu.SetActive(false);
        else cleanSubMenu.SetActive(true);
    }

    public void OnPlayCategoryClicked()
    {
        HideAllSubMenus();
        if (playSubMenu.activeSelf) playSubMenu.SetActive(false);
        else playSubMenu.SetActive(true);
    }

    public void OnDecorateCategoryClicked()
    {
        HideAllSubMenus();
        if (decorateSubMenu.activeSelf) decorateSubMenu.SetActive(false);
        else decorateSubMenu.SetActive(true);
    }

    // --- Functions for Sub-Menu Buttons ---
    public void OnMealButtonClicked()
    {
        HideAllPanels();
        foodInventoryPanel.Show(ItemCategory.Food);
    }

    public void OnWallButtonClicked()
    {
        HideAllPanels();
        decorateInventoryPanel.Show(ItemCategory.Wall);
    }

    public void OnFloorButtonClicked()
    {
        HideAllPanels();
        decorateInventoryPanel.Show(ItemCategory.Floor);
    }

    public void OnFurnitureButtonClicked()
    {
        HideAllPanels();
        decorateInventoryPanel.Show(ItemCategory.Furniture);
    }
}