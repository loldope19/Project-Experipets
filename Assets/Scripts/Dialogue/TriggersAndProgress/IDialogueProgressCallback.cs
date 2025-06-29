using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDialogueProgressCallback : MonoBehaviour
{
    /// <summary>
    /// The callback index, ie. the option's index. If -1, treated as no callback index
    /// </summary>
    [HideInInspector] public int CallbackIndex = -1;
    protected void GenericCallback()
    {
        DialogueManager.Instance?.NextDialogueStep(CallbackIndex);
    }

}
