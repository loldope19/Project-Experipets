using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStats : MonoBehaviour
{
    [Header("-- Pet Parameters --")]
    public float hunger = 50;
    public float cleanliness = 50;
    public float happiness = 50;

    [Header("-- Decay Rate Parameters --")]
    public float hungerDecayRate;
    public float cleanlinessDecayRate;
    public float happinessDecayRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hunger -= hungerDecayRate * Time.deltaTime;
        cleanliness -= cleanlinessDecayRate * Time.deltaTime;
        happiness -= happinessDecayRate * Time.deltaTime;

        hunger = Mathf.Clamp(hunger, 0f, 100f);
        cleanliness = Mathf.Clamp(cleanliness, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);
    }

    public void Feed(float amount) { hunger = Mathf.Min(hunger + amount, 100f); }
    public void Clean(float amount) { cleanliness = Mathf.Min(cleanliness + amount, 100f); }
    public void Play(float amount) { happiness = Mathf.Min(happiness + amount, 100f); }
}
