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
    private readonly float maxStatValue = 100f;

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
        CheckForFailure();
    }

    private void CheckForFailure()
    {
        if (hunger <= 0 || cleanliness <= 0 || happiness <= 0)
        {
            GameManager.Instance.TriggerGameOver();
        }
    }

    public void DecayStatsForNewDay(int messCount)
    {
        hunger -= 25f;
        happiness -= 25f;
        float cleanlinessPenalty = 25f + (messCount * 5f); // 5 points per piece of trash/poopies
        cleanliness -= cleanlinessPenalty;

        Debug.Log($"Day ended with {messCount} mess items. Cleanliness penalty: {cleanlinessPenalty}");


        ClampAllStats();
        CheckForFailure();
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
