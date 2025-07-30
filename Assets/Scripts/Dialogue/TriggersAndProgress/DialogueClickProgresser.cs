using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueClickProgresser : IDialogueProgressCallback, IPointerClickHandler
{
    private DialogueView _dialogueView;

    private void Awake()
    {
        _dialogueView = FindObjectOfType<DialogueView>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_dialogueView != null && _dialogueView.IsCurrentlyScrolling())
        {
            _dialogueView.ProgressDialogue();
        }
        else
        {
            this.GenericCallback();
        }
    }
}
