using UnityEngine;
using UnityEngine.EventSystems;

public class PetDropTarget : MonoBehaviour, IDropHandler
{
    [Header("Mess Spawning")]
    public GameObject trashPrefab;
    private SpawnArea spawnArea;

    private void Start()
    {
        spawnArea = FindObjectOfType<SpawnArea>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        InventorySlotUI slotUI = droppedObject.GetComponent<InventorySlotUI>();

        if (slotUI == null || slotUI.slot.item == null) return;

        ItemData droppedItem = slotUI.slot.item;

        if (droppedItem.category == ItemCategory.Food ||
            droppedItem.category == ItemCategory.Medicine ||
            droppedItem.category == ItemCategory.Treat)
        {
            Debug.Log($"Used consumable {droppedItem.name} on the pet!");
            PetStats.Instance.ApplyItemEffects(droppedItem);
            TaskManager.Instance.OnItemUsed(droppedItem);
            InventoryManager.Instance.UseItem(droppedItem);

            Destroy(droppedObject);
            SpawnTrash();
        }
        else if (droppedItem.category == ItemCategory.Toy)
        {
            Debug.Log($"Played with {droppedItem.name}!");

            PetStats.Instance.Play(droppedItem.happinessEffect);
            TaskManager.Instance.OnItemUsed(droppedItem);
        }
    }

    private void SpawnTrash()
    {
        if (trashPrefab != null && spawnArea != null)
        {
            Vector2 spawnPosition = spawnArea.GetRandomPosition();
            GameObject newTrash = Instantiate(trashPrefab, spawnArea.transform);
            newTrash.GetComponent<RectTransform>().localPosition = spawnPosition;

            Debug.Log("Trash spawned at local position: " + spawnPosition);
        }
    }
}