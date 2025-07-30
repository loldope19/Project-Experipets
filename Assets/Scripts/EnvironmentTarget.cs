using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirtType
{
    Dust,  // Cleaned by the Broom
    Poop   // Cleaned by the Gloves
}

// This component identifies an object as something that can be cleaned by an environmental tool like a broom.
public class EnvironmentTarget : MonoBehaviour
{
    public DirtType dirtType;
    void Start()
    {
        DayManager.messCount++;
        Debug.Log($"Mess created. Total mess count: {DayManager.messCount}");
    }

    void OnDestroy()
    {
        DayManager.messCount--;
        Debug.Log($"Mess cleaned. Total mess count: {DayManager.messCount}");
    }
    public void Clean()
    {
        Debug.Log($"Cleaning {gameObject.name}!");
        Destroy(gameObject);
        PetStats.Instance.Clean(5);
        if (!PlayerData.Instance.isPrologueComplete)
        {
            DialogueManager.Instance.StartDialogue("Prologue", "PROLOGUE_06_CleanTutorialComplete");
        }
    }
}