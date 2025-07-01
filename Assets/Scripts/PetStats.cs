using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStats : MonoBehaviour
{
    [Header("-- Pet Parameters --")]
    public float hunger = 3;
    public float cleanliness = 3;
    public float happiness = 3;

    /// <summary>
    /// This is the new key function for applying item effects.
    /// The InventoryManager will call this when an item is used.
    /// </summary>
    /// <param name="itemData">The ScriptableObject of the item being used.</param>
    public void ApplyItemEffects(ItemData itemData)
    {
        if (itemData == null) return;

        hunger += itemData.hungerEffect;
        cleanliness += itemData.cleanlinessEffect;
        happiness += itemData.happinessEffect;

        ClampAllStats();
    }

    public void DecayStatsForNewDay()
    {
        hunger -= 1;
        cleanliness -= 1;
        happiness -= 1;

        // Make sure they don't go below zero
        ClampAllStats();
    }

    private void ClampAllStats()
    {
        hunger = Mathf.Clamp(hunger, 0f, 5f);
        cleanliness = Mathf.Clamp(cleanliness, 0f, 5f);
        happiness = Mathf.Clamp(happiness, 0f, 5f);
    }

    public void Feed(float amount) { hunger = Mathf.Min(hunger + amount, 5f); }
    public void Clean(float amount) { cleanliness = Mathf.Min(cleanliness + amount, 5f); }
    public void Play(float amount) { happiness = Mathf.Min(happiness + amount, 5f); }
}
