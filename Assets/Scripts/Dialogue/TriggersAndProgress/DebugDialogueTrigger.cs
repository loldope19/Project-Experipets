using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDialogueTrigger : IDialogueTrigger
{
    [SerializeField] private bool _startDialogue;
    void Update()
    {
        if (_startDialogue)
        {
            _startDialogue = false;
            this.GenericTriggerDialogue();
        }
    }
}
