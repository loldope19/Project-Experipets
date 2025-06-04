using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("-- Pet Logic Manager --")]
    public PetStats petStats;

    [Header("-- UI Bars --")]
    public Slider hungerBar;
    public Slider cleanlinessBar;
    public Slider happinessBar;

    // Update is called once per frame
    void Update()
    {
        if (petStats != null)
        {
            hungerBar.value = petStats.hunger;
            cleanlinessBar.value = petStats.cleanliness;
            happinessBar.value = petStats.happiness;
        }
    }

    public void OnFeedButtonPressed() { if (petStats != null) petStats.Feed(20); } // Example amount
    public void OnCleanButtonPressed() { if (petStats != null) petStats.Clean(20); }
    public void OnPlayButtonPressed() { if (petStats != null) petStats.Play(20); }
}
