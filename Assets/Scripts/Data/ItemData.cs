using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory { Food, Toy, Cleaning, Medicine }

[CreateAssetMenu(fileName = "New Item", menuName = "Experipets/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("-- General Item Info --")]
    public string itemName;
    public Sprite itemIcon;
    public int cost;
    public string description;

    [Header("-- Item Category --")]
    public ItemCategory category;

    [Header("-- Item Effects --")]
    public float hungerEffect;
    public float cleanlinessEffect;
    public float happinessEffect;
    // Add more item effects here--
}
