using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public static event Action OnInventoryChanged;

    [Header("-- References --")]
    [SerializeField] private PetStats petStats;

    [Header("-- Inventory --")]
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

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
        InventorySlot slot = inventorySlots.FirstOrDefault(s => s.item == item);

        if (slot != null)
        {
            slot.AddQuantity(1);
        }
        else
        {
            inventorySlots.Add(new InventorySlot(item, 1));
        }

        Debug.Log($"Added 1 {item.itemName}. We now have {slot?.quantity ?? 1}.");
        OnInventoryChanged?.Invoke();
    }

    public void UseItem(ItemData item)
    {
        InventorySlot slot = inventorySlots.FirstOrDefault(s => s.item == item);

        if (slot != null)
        {
            slot.quantity--;

            if (slot.quantity <= 0)
            {
                inventorySlots.Remove(slot);
            }

            Debug.Log($"Used {item.itemName}. {slot.quantity} remaining.");
            OnInventoryChanged?.Invoke();
        }
    }
}