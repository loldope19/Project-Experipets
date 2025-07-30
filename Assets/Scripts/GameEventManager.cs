using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }

    [Header("Scene References")]
    [SerializeField] private List<Light> roomLights; // Drag the main room light here
    [SerializeField] private Animator fadePanelAnimator; // Drag your FadePanel here

    [Header("Dialogue Elements")]
    [SerializeField] private Image backingPanel;

    [Header("Pet Care View Elements")]
    [SerializeField] private PetCareUIManager petCareUIManager;
    [SerializeField] private Button petCareCloseButton;
    [SerializeField] private Button feedButton;
    [SerializeField] private Button cleanButton;
    [SerializeField] private Button playButton;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void TriggerEvent(string eventName)
    {
        switch (eventName)
        {
            // --- NARRATIVE EVENTS ---
            case "EndPrologue":
                PlayerData.Instance.isPrologueComplete = true;
                EnablePetCareButtons();
                StartCoroutine(EndChapterSequence(0));
                break;
            case "EndChapter1":
                StartCoroutine(EndChapterSequence(1));
                break;
            case "EndChapter2":
                StartCoroutine(EndChapterSequence(2));
                break;
            case "EndChapter3":
                StartCoroutine(EndChapterSequence(3));
                break;
            case "EndChapter4":
                StartCoroutine(EndChapterSequence(4));
                break;

            // --- GAMEPLAY EVENTS ---
            case "Prologue_LightsUp":
                Prologue_RoomLightsUp();
                break;

            case "ShowLoginScreen":
                ShowLoginScreen();
                break;

            case "TriggerDesktopReboot":
                TriggerReboot1();
                break;

            case "TriggerPetCareReboot":
                StopAllCoroutines();
                StartCoroutine(TriggerRebootSequencePetCare());
                break;

            case "NameConfirmed":
                PlayerData.Instance.isNameConfirmed = true;
                break;

            case "ShowPetCareScreen":
                ShowPetCareScreen();
                break;

            case "StartFeedTutorial":
                StartCoroutine(FeedTutorialSequence());
                break;

            case "StartPlayTutorial":
                StartCoroutine(PlayTutorialSequence());
                break;

            case "StartCleanTutorial":
                StartCoroutine(CleanTutorialSequence());
                break;

            case "PanelDefault":
                SetPanelAlpha(0.5058824f);
                break;

            case "PanelBlack":
                SetPanelAlpha(1.0f);
                break;

            case "PanelNone":
                SetPanelAlpha(0);
                break;

            

            // Add more cases here for future events

            default:
                Debug.LogWarning($"GameEventManager: Unknown event called: {eventName}");
                break;
        }
    }

    

    // --- DIALOGUE EVENT FUNCTIONS ---
    public void Prologue_RoomLightsUp()
    {
        StartCoroutine(RoomLightsUpSequence());
    }
    private IEnumerator RoomLightsUpSequence()
    {
        Debug.Log("Starting prologue sequence: Room lights up.");

        foreach (var light in roomLights)
        {
            light.enabled = false;
        }

        if (fadePanelAnimator != null) fadePanelAnimator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1.5f);

        foreach (var light in roomLights)
        {
            light.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void ShowLoginScreen()
    {
        if (PlayerData.Instance) PlayerData.Instance.isPrologueComplete = false;
        ViewManager.Instance.GoToLoginView();
    }

    public void ShowPetCareScreen()
    {
        if (PlayerData.Instance) PlayerData.Instance.isPrologueComplete = false;
        ViewManager.Instance.GoToPetCareView();
    }

    public void TriggerReboot1()
    {
        StartCoroutine (TriggerRebootSequenceDesktop());
    }
    private IEnumerator TriggerRebootSequenceDesktop()
    {
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.5f);
        ViewManager.Instance.GoToDesktopView();
        yield return new WaitForSeconds(0.2f);
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.5f);
        ViewManager.Instance.GoToDesktopView();
        yield return new WaitForSeconds(0.1f);
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.5f);
        ViewManager.Instance.GoToDesktopView();
    }

    private IEnumerator TriggerRebootSequencePetCare()
    {
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.3f);
        ViewManager.Instance.GoToDesktopView();
        yield return new WaitForSeconds(0.1f);
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.4f);
        ViewManager.Instance.GoToPetCareView();
        yield return new WaitForSeconds(0.2f);
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.5f);
        ViewManager.Instance.GoToDesktopView();
        yield return new WaitForSeconds(0.1f);
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.3f);
        ViewManager.Instance.GoToPetCareView();
        EnablePetCareButtons();
    }

    private void DisablePetCareButtons()
    {
        petCareCloseButton.gameObject.SetActive(false);
        feedButton.gameObject.SetActive(false);
        cleanButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
    }

    private void EnablePetCareButtons()
    {
        petCareCloseButton.gameObject.SetActive(true);
        feedButton.gameObject.SetActive(true);
        cleanButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
    }

    private void EnableButton(Button button)
    {
        button.gameObject.SetActive(true);
    }

    private void SetPanelAlpha(float alpha)
    {
        backingPanel.color = new Color(0, 0, 0, alpha);
    }

    private IEnumerator FeedTutorialSequence()
    {
        Debug.Log("Starting Feed Tutorial Sequence...");

        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.4f);
        ViewManager.Instance.GoToPetCareView();
        yield return new WaitForSeconds(0.2f);
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.5f);
        ViewManager.Instance.GoToDesktopView();
        yield return new WaitForSeconds(0.1f);
        ViewManager.Instance.HideAllViews();
        yield return new WaitForSeconds(0.3f);
        ViewManager.Instance.GoToPetCareView();
        yield return new WaitForSeconds(0.1f);

        DisablePetCareButtons();
        EnableButton(feedButton);
    }

    private IEnumerator PlayTutorialSequence()
    {
        Debug.Log("Starting Play Tutorial Sequence...");

        ViewManager.Instance.GoToPetCareView();
        yield return new WaitForSeconds(0.1f);

        petCareUIManager.HideAllSubMenus();
        petCareUIManager.HideAllPanels();
        DisablePetCareButtons();
        EnableButton(playButton);
    }

    private IEnumerator CleanTutorialSequence()
    {
        Debug.Log("Starting Clean Tutorial Sequence...");

        ViewManager.Instance.GoToPetCareView();
        yield return new WaitForSeconds(0.1f);

        petCareUIManager.HideAllSubMenus();
        petCareUIManager.HideAllPanels();
        DisablePetCareButtons();
        EnableButton(cleanButton);

    }

    private IEnumerator EndChapterSequence(int chapterToEnd)
    {
        Debug.Log($"Starting end sequence for Chapter {chapterToEnd}...");

        if (fadePanelAnimator != null)
        {
            fadePanelAnimator.SetTrigger("FadeIn");
        }
        yield return new WaitForSeconds(2f);
        ViewManager.Instance.HideAllViews();

        if (chapterToEnd == 1)
        {
            // e.g., Play a specific sound effect for Chapter 1's ending
        }
        else if (chapterToEnd == 2)
        {
            // e.g., Show a quick glitch effect on screen for Chapter 2's ending
        }

        DayManager.Instance.AdvanceToNextChapter(chapterToEnd + 1);
        ViewManager.Instance.GoToLoginView();
    }

}