using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueClickProgresser : IDialogueProgressCallback, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        this.GenericCallback();
    }
}
