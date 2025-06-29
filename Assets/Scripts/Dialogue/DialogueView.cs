using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handles the visual display of dialogue
/// </summary>
public class DialogueView : MonoBehaviour
{
    [SerializeField] private TMP_Text _mainTextbox;
    /// <summary>
    /// These options must be on a gameobject that has both a TMP_Text component, and a IDialogueProgressCallback to work
    /// </summary>
    [SerializeField] private List<GameObject> _dialogueOptions;
    [SerializeField] private float _scrollTimePerCharacter;

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

    private DialogueStep _currentDialogueStep;
    /// <summary>
    /// Call to display a dialogue step (if view is active)
    /// </summary>
    /// <param name="dialogueStep">The dialogue step to display</param>
    public void DisplayStep(DialogueStep dialogueStep)
    {
        _currentDialogueStep = dialogueStep;

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

        if (_scrollTimePerCharacter <= 0) ShowText();
        else StartCoroutine(ScrollText());
    }

    private void ShowText()
    {
        // Set main textbox text
        _mainTextbox.text = _currentDialogueStep.Text;

        // Set option text boxes
        for (int i = 0; i < _dialogueOptions.Count; i++)
        {
            GameObject option = _dialogueOptions[i];
            TMP_Text optionText = option.GetComponentInChildren<TMP_Text>();
            IDialogueProgressCallback optionCallback = option.GetComponentInChildren<IDialogueProgressCallback>();

            // If there exists an option for the step to be inserted into the option, set text, callback index, and display
            if (i < _currentDialogueStep.DialogueOptions.Count)
            {
                if(optionText != null) optionText.text = _currentDialogueStep.DialogueOptions[i].OptionText;
                if(optionCallback != null) optionCallback.CallbackIndex = i;
                option.SetActive(true);
            }
            else
            {
                if(optionText != null) optionText.text = "";
                if(optionCallback != null) optionCallback.CallbackIndex = -1;
                option.SetActive(false);
            }
        }
    }

    private IEnumerator ScrollText()
    {
        int charIndex = 0;
        float elapsedTime = 0f;

        while (charIndex < _currentDialogueStep.Text.Length)
        {
            _mainTextbox.text = _currentDialogueStep.Text.Substring(0, charIndex);
            yield return new WaitForEndOfFrame();

            // Increment index
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= _scrollTimePerCharacter)
            {
                elapsedTime = 0f;
                charIndex++;

                // Increment again if a backslash is encountered
                if (_currentDialogueStep.Text[charIndex] == '\\')
                    charIndex++;

                // If no backslash is met, check if processing a special modifier (ie. marked by <>)
                else if (_currentDialogueStep.Text[charIndex] == '<')
                {
                    // Keep progressing the index until a '>' is met up to the end of the text
                    while (_currentDialogueStep.Text[charIndex] != '>' && charIndex < _currentDialogueStep.Text.Length)
                        charIndex++;
                } 
            }
        }

        ShowText();
    }
}
