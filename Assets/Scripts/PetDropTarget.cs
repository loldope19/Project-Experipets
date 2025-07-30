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
            if (!PlayerData.Instance.isPrologueComplete)
            {
                DialogueManager.Instance.StartDialogue("Prologue", "PROLOGUE_04_FeedTutorialComplete");
            }

            Debug.Log($"Used consumable {droppedItem.name} on the pet!");
            PetStats.Instance.ApplyItemEffects(droppedItem);
            TaskManager.Instance.OnItemUsed(droppedItem);
            InventoryManager.Instance.UseItem(droppedItem);

            PetAnimationManager.Instance.Eat();

            Destroy(droppedObject);
            SpawnTrash();
        }
        else if (droppedItem.category == ItemCategory.Toy)
        {
            Debug.Log($"Played with {droppedItem.name}!");

            switch (droppedItem.toolType)
            {
                case ToolType.Ball:
                    // Ayo Dun, Ball play is done via clicking on the ball and then clicking on a spawn area
                    // (literally the free space on the PC)
                    // NOT via dropping it on the poor guy;
                    PetAnimationManager.Instance.Ball();
                    break;
                case ToolType.DraggableToy:
                    PetAnimationManager.Instance.Bone();
                    break;
                case ToolType.LaserPointer:
                    // Ayo Dun, Laser play is done via clicking on the laser; that's it
                    PetAnimationManager.Instance.Laser();
                    break;
            }

            if (!PlayerData.Instance.isPrologueComplete)
            {
                DialogueManager.Instance.StartDialogue("Prologue", "PROLOGUE_05_PlayTutorialComplete");
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