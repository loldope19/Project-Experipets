using UnityEngine;
using UnityEngine.UI;

public class PetCareUIManager : MonoBehaviour
{
    [Header("Sub-Menu Panels")]
    [SerializeField] private GameObject feedSubMenu;
    [SerializeField] private GameObject cleanSubMenu;
    [SerializeField] private GameObject playSubMenu;
    [SerializeField] private GameObject decorateSubMenu;
    // Add other sub-menus here as you build them

    [Header("Notification Panels")]
    [SerializeField] private GameObject majorTaskCompletedPopup;

    [Header("Specialized Inventory Panels")]
    [SerializeField] private PaginatedInventoryPanel foodInventoryPanel;
    [SerializeField] private PaginatedInventoryPanel cleanInventoryPanel;
    [SerializeField] private PaginatedInventoryPanel playInventoryPanel;
    [SerializeField] private PaginatedInventoryPanel decorateInventoryPanel;

    [Header("Needs Display UI")]
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider cleanlinessSlider;
    [SerializeField] private Slider happinessSlider;


    private void Start()
    {
        HideAllSubMenus();
        HideMajorTaskCompletedPopup();
        // DialogueManager.Instance.StartDialogue("CoreFive", 0);
    }

    private void Update()
    {
        if (PetStats.Instance != null)
        {
            hungerSlider.value = PetStats.Instance.hunger;
            cleanlinessSlider.value = PetStats.Instance.cleanliness;
            happinessSlider.value = PetStats.Instance.happiness;
        }
    }

    private void OnEnable()
    {
        if (foodInventoryPanel.gameObject.activeSelf)
        {
            foodInventoryPanel.Show(ItemCategory.Food);
        }
        else if (cleanInventoryPanel.gameObject.activeSelf)
        {
            cleanInventoryPanel.Show(ItemCategory.Cleaning);
        }
        else if (playInventoryPanel.gameObject.activeSelf)
        {
            playInventoryPanel.Show(ItemCategory.Toy);
        }
        HideAllPanels();
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
        else
        {
            cleanSubMenu.SetActive(true);
            cleanInventoryPanel.Show(ItemCategory.Cleaning);
        }
    }

    public void OnPlayCategoryClicked()
    {
        HideAllSubMenus();
        if (playSubMenu.activeSelf) playSubMenu.SetActive(false);
        else
        {
            playSubMenu.SetActive(true);
            playInventoryPanel.Show(ItemCategory.Toy);
        }
    }

    public void OnDecorateCategoryClicked()
    {
        HideAllSubMenus();
        if (decorateSubMenu.activeSelf) decorateSubMenu.SetActive(false);
        else decorateSubMenu.SetActive(true);
    }

    // --- Functions for Pop-ups ---
    public void ShowMajorTaskCompletedPopup()
    {
        if (majorTaskCompletedPopup != null)
        {
            majorTaskCompletedPopup.SetActive(true);
        }
    }

    public void HideMajorTaskCompletedPopup()
    {
        if (majorTaskCompletedPopup != null)
        {
            majorTaskCompletedPopup.SetActive(false);
        }
    }   

    // --- Functions for Sub-Menu Buttons ---
    public void OnMealButtonClicked()
    {
        HideAllPanels();
        foodInventoryPanel.Show(ItemCategory.Food);
    }

    public void OnMedicineButtonClicked()
    {
        HideAllPanels();
        foodInventoryPanel.Show(ItemCategory.Medicine);
    }

    public void OnTreatButtonClicked()
    {
        HideAllPanels();
        foodInventoryPanel.Show(ItemCategory.Treat);
    }

}