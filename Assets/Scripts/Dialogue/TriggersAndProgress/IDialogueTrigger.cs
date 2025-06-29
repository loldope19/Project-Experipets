using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IDialogueTrigger : MonoBehaviour
{
    /// <summary>
    /// If left blank, defaults to using the core dialogue
    /// </summary>
    [SerializeField] private string _databaseTag;

    /// <summary>
    /// If left blank, opts to use random dialogue if database is provided
    /// </summary>
    [SerializeField] private string _dialogueTag;
    [SerializeField] private int _startIndex = 0;
    public void GenericTriggerDialogue()
    {
        if (String.IsNullOrEmpty(_databaseTag) && !String.IsNullOrEmpty(_dialogueTag))
            DialogueManager.Instance?.StartDialogue(_dialogueTag, _startIndex);
        else if (!String.IsNullOrEmpty(_databaseTag) && String.IsNullOrEmpty(_dialogueTag))
            DialogueManager.Instance?.StartRandomDialogue(_databaseTag, _startIndex);
        else
            DialogueManager.Instance?.StartDialogue(_databaseTag, _dialogueTag, _startIndex);
    }
    
}
