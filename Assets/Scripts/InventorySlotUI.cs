using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public InventorySlot slot;
    public Image itemIcon;

    public void SetSlot(InventorySlot newSlot)
    {
        slot = newSlot;
        itemIcon.sprite = newSlot.item.itemIcon;
        itemIcon.enabled = true;
        GetComponent<InteractableItem>().Initialize(slot.item);
    }
}