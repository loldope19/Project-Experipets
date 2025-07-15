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

        bool wasMajorTaskCompleted = TaskManager.Instance.IsTaskComplete(TaskManager.Instance.ActiveMajorTask);
        DayManager.Instance.EndDayAndDecayStats();

        CurrencyManager.Instance.AddCurrency(TaskManager.Instance.minorTasksCompleted, TaskManager.Instance.majorTasksCompleted);

        if (wasMajorTaskCompleted)
        {
            DayManager.Instance.AdvanceToNextChapter();
            PetAnimationManager.Instance.StageChange();
        }
        else
        {
            TaskManager.Instance.PrepareForNextDay();
        }

        ViewManager.Instance.GoToLoginView();

        fadePanelAnimator.SetTrigger("FadeOut");
    }
}