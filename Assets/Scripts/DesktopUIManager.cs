using UnityEngine;

public class DesktopUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject startMenuPanel;

    public void ToggleStartMenu()
    {
        bool isActive = startMenuPanel.activeSelf;
        startMenuPanel.SetActive(!isActive);
    }

    public void OnSettingsClicked()
    {
        Debug.Log("Settings button clicked!");
        ToggleStartMenu();
    }

    public void OnLogOutClicked()
    {
        Debug.Log("Logging out...");
        ViewManager.Instance.GoToLoginView();
        ToggleStartMenu();
    }
}