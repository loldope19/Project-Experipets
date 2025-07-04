using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("-- References --")]
    [SerializeField] private PetStats petStats;

    [Header("-- Inventory --")]
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    [Header("-- Task Logic Manager --")]
    public TaskManager task;

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
        // call a function here to refresh the inventory UI
    }

    public void UseItem(ItemData item)
    {
        InventorySlot slot = inventorySlots.FirstOrDefault(s => s.item == item);

        if (slot != null)
        {
            if (petStats != null)
            {
                TaskManager.Instance.OnItemUsed(item);
                petStats.ApplyItemEffects(item);
                slot.quantity--;

                if (slot.quantity <= 0)
                {
                    inventorySlots.Remove(slot);
                }

                if (item.category == ItemCategory.Food) PetAnimationManager.Instance.Eat();
                if (item.category == ItemCategory.Toy) PetAnimationManager.Instance.Play();

                Debug.Log($"Used {item.itemName}. {slot.quantity} remaining.");
                // refresh the inventory UI here
            }
        }
    }
}
