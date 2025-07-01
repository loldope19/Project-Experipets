using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStats : MonoBehaviour
{
    public static PetStats Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("-- Pet Parameters --")]
    public float hunger = 50;
    public float cleanliness = 50;
    public float happiness = 50;
    private float maxStatValue = 100f;

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
        hunger -= 25f;
        cleanliness -= 25f;
        happiness -= 25f;

        // Make sure they don't go below zero
        ClampAllStats();
        Debug.Log($"Stats decayed. Hunger: {hunger}, Cleanliness: {cleanliness}, Happiness: {happiness}");
    }

    private void ClampAllStats()
    {
        hunger = Mathf.Clamp(hunger, 0f, maxStatValue);
        cleanliness = Mathf.Clamp(cleanliness, 0f, maxStatValue);
        happiness = Mathf.Clamp(happiness, 0f, maxStatValue);
    }

    public void Feed(float amount) { hunger = Mathf.Min(hunger + amount, maxStatValue); }
    public void Clean(float amount) { cleanliness = Mathf.Min(cleanliness + amount, maxStatValue); }
    public void Play(float amount) { happiness = Mathf.Min(happiness + amount, maxStatValue); }
}
