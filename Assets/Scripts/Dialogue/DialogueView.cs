using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Handles the visual display of dialogue
/// </summary>
public class DialogueView : MonoBehaviour
{
    [Header("Core Components")]
    [SerializeField] private TMP_Text _mainTextbox;
    [SerializeField] private TMP_Text _characterNameText;
    /// <summary>
    /// These options must be on a gameobject that has both a TMP_Text component, and a IDialogueProgressCallback to work
    /// </summary>
    [SerializeField] private List<GameObject> _dialogueOptions;
    [SerializeField] private float _scrollTimePerCharacter;

    [Header("Visuals")]
    [SerializeField] private Image _backingPanelImage;
    [SerializeField] private Sprite _boxWithNameSprite;
    [SerializeField] private Sprite _boxNoNameSprite;


    public void Activate()
    {
        // Clear text
        _mainTextbox.text = "";
        foreach (var option in _dialogueOptions)
        {
            option.SetActive(false);

            TMP_Text optionText = option.GetComponentInChildren<TMP_Text>();
            if (optionText != null) optionText.text = "";
            else Debug.LogWarning($"[WARN]({this}): Dialogue option must have a TMP_Text component in its children.");

            IDialogueProgressCallback optionCallback = option.GetComponentInChildren<IDialogueProgressCallback>();
            if (optionCallback != null) optionCallback.CallbackIndex = -1;
            else Debug.LogWarning($"[WARN]({this}): Dialogue option must have a IDialogueProgressCallback in its children.");
        }

        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        // Clear Text
        _mainTextbox.text = "";
        foreach (var option in _dialogueOptions)
        {
            option.SetActive(false);

            TMP_Text optionText = option.GetComponentInChildren<TMP_Text>();
            if (optionText != null) optionText.text = "";

            IDialogueProgressCallback optionCallback = option.GetComponentInChildren<IDialogueProgressCallback>();
            if (optionCallback != null) optionCallback.CallbackIndex = -1;
        }

        _currentDialogueStep = null;
        gameObject.SetActive(false);
    }

    private bool _isScrolling = false;
    private DialogueStep _currentDialogueStep;
    /// <summary>
    /// Call to display a dialogue step (if view is active)
    /// </summary>
    /// <param name="dialogueStep">The dialogue step to display</param>
    public void DisplayStep(DialogueStep dialogueStep)
    {
        _currentDialogueStep = dialogueStep;

        if (!string.IsNullOrEmpty(dialogueStep.CharacterName))
        {
            _characterNameText.gameObject.SetActive(true);
            _characterNameText.text = dialogueStep.CharacterName;
            _backingPanelImage.sprite = _boxWithNameSprite;
        }
        else
        {
            _characterNameText.gameObject.SetActive(false);
            _backingPanelImage.sprite = _boxNoNameSprite;
        }

        string processedText = dialogueStep.Text;
        if (!string.IsNullOrEmpty(SystemInfoManager.Instance.pcUsername))
        {
            processedText = processedText.Replace("{PC_USERNAME}", SystemInfoManager.Instance.pcUsername);
        }

        if (PlayerData.Instance != null && !string.IsNullOrEmpty(PlayerData.Instance.playerName))
        {
            processedText = processedText.Replace("{PLAYER_NAME}", PlayerData.Instance.playerName);
        }

        _mainTextbox.text = "";
        foreach (var option in _dialogueOptions)
        {
            option.SetActive(false);
        }

        if (!string.IsNullOrEmpty(dialogueStep.eventToTrigger))
        {
            GameEventManager.Instance.TriggerEvent(dialogueStep.eventToTrigger);
        }

        if (_scrollTimePerCharacter <= 0)
        {
            ShowText(processedText);
        }
        else
        {
            StartCoroutine(ScrollText(processedText));
        }
    }

    private void ShowText(string textToShow)
    {
        _mainTextbox.text = textToShow;

        for (int i = 0; i < _dialogueOptions.Count; i++)
        {
            GameObject option = _dialogueOptions[i];
            TMP_Text optionText = option.GetComponentInChildren<TMP_Text>();
            IDialogueProgressCallback optionCallback = option.GetComponentInChildren<IDialogueProgressCallback>();

            if (i < _currentDialogueStep.DialogueOptions.Count)
            {
                if (optionText != null) optionText.text = _currentDialogueStep.DialogueOptions[i].OptionText;
                if (optionCallback != null) optionCallback.CallbackIndex = i;
                option.SetActive(true);
            }
            else
            {
                if (optionText != null) optionText.text = "";
                if (optionCallback != null) optionCallback.CallbackIndex = -1;
                option.SetActive(false);
            }
        }
    }

    private IEnumerator ScrollText(string textToScroll)
    {
        _isScrolling = true;

        int charIndex = 0;
        float elapsedTime = 0f;

        if (string.IsNullOrEmpty(textToScroll))
        {
            ShowText(textToScroll); 
            yield break;
        }

        while (charIndex < textToScroll.Length)
        {
            _mainTextbox.text = textToScroll.Substring(0, charIndex + 1);
            yield return null;

            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= _scrollTimePerCharacter)
            {
                elapsedTime = 0f;
                charIndex++;

                if (charIndex >= textToScroll.Length) break;

                if (textToScroll[charIndex] == '<')
                {
                    while (charIndex < textToScroll.Length && textToScroll[charIndex] != '>')
                    {
                        charIndex++;
                    }
                }
            }
        }

        ShowText(textToScroll);

        _isScrolling = false;
    }

    public void ProgressDialogue()
    {
        if (_isScrolling)
        {
            _isScrolling = false;
            StopAllCoroutines();
            string processedText = _currentDialogueStep.Text;
            if (PlayerData.Instance != null && !string.IsNullOrEmpty(PlayerData.Instance.playerName))
            {
                processedText = processedText.Replace("{PLAYER_NAME}", PlayerData.Instance.playerName);
            }

            ShowText(processedText);
        }
        else
        {
            DialogueManager.Instance.NextDialogueStep();
        }
    }

    public bool IsCurrentlyScrolling()
    {
        return _isScrolling;
    }
}
