using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("-- References --")]
    [SerializeField] private PetStats petStats;

    [Header("-- Inventory --")]
    public List<ItemData> ownedItems = new List<ItemData>();

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

    public void AddItem(ItemData item)
    {
        ownedItems.Add(item);
        Debug.Log($"Added {item.itemName} to inventory.");
        // trigger event for update ui here
    }

    public void UseItem(ItemData item)
    {
        if (ownedItems.Contains(item))
        {
            if (petStats != null)
            {
                petStats.ApplyItemEffects(item);

                ownedItems.Remove(item);

                Debug.Log($"Used and removed {item.itemName} from inventory.");
                // update Inventory UI here.
            }
            else
            {
                Debug.LogError("PetStats reference is not set in the InventoryManager.");
            }
        }
        else
        {
            Debug.LogWarning("Attempted to use an item the player does not own.");
        }
    }
}
