using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory { Food, Medicine, Treat, Toy, Cleaning }

public enum ToolType
{
    None,           // Not a tool
    Towel,          // Cleans the pet
    Broom,          // Cleans the environment
    Gloves,         // Cleans shit lmao
    DraggableToy,   // BONE
    Ball,           // ball
    LaserPointer    // The laser pointer
}

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
    public ToolType toolType;

    [Header("-- Item Effects --")]
    public float hungerEffect;
    public float cleanlinessEffect;
    public float happinessEffect;
    // Add more item effects here--
}
