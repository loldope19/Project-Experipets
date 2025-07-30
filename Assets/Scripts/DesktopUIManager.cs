using System.Collections;
using UnityEngine;

public class DesktopUIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject startMenuPanel;

    [Header("Transition Effects")]
    [SerializeField] private Animator fadePanelAnimator;

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
        ToggleStartMenu();
        ViewManager.Instance.GoToLoginView();
    }

    public void OnShutDownButtonClicked()
    {
        StartCoroutine(ShutdownSequence());
    }

    private IEnumerator ShutdownSequence()
    {
        if (startMenuPanel.activeSelf)
        {
            ToggleStartMenu();
        }

        fadePanelAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);

        int minorTasksDone = TaskManager.Instance.minorTasksCompletedThisChapter;
        CurrencyManager.Instance.AddCurrency(minorTasksDone, 0);
        DayManager.Instance.EndDayAndDecayStats();

        ViewManager.Instance.GoToLoginView();
        fadePanelAnimator.SetTrigger("FadeOut");
    }
}