using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

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

            PetAnimationManager.Instance.Eat();
            AudioManager.Instance.PlaySFX(SfxType.eat);

            Destroy(droppedObject);
            SpawnTrash();
        }
        else if (droppedItem.category == ItemCategory.Toy)
        {
            Debug.Log($"Played with {droppedItem.name}!");

            switch (droppedItem.toolType)
            {
                case ToolType.Ball:
                    PetAnimationManager.Instance.Ball();
                    AudioManager.Instance.PlaySFX(SfxType.Ball);
                    break;
                case ToolType.DraggableToy:
                    PetAnimationManager.Instance.Bone();
                    AudioManager.Instance.PlaySFX(SfxType.toy);
                    break;
                case ToolType.LaserPointer:
                    PetAnimationManager.Instance.Laser();
                    AudioManager.Instance.PlaySFX(SfxType.laser);
                    break;
            }

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