using UnityEngine;
using UnityEngine.EventSystems;

public class PetDropTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        InventorySlotUI slotUI = droppedObject.GetComponent<InventorySlotUI>();

        if (slotUI != null && (slotUI.slot.item.category == ItemCategory.Food
            || slotUI.slot.item.category == ItemCategory.Medicine || slotUI.slot.item.category == ItemCategory.Treat))
        {
            ItemData foodItem = slotUI.slot.item;

            Debug.Log($"Dropped {foodItem.name} on the pet!");

            PetStats.Instance.ApplyItemEffects(foodItem);
            TaskManager.Instance.OnItemUsed(foodItem);
            InventoryManager.Instance.UseItem(foodItem);

            Destroy(droppedObject);
        }
    }
}