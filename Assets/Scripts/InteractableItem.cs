using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class InteractableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas rootCanvas;
    
    private Transform originalParent;
    private Vector2 originalPosition;

    [SerializeField] private ItemData currentItem;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        rootCanvas = GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        currentItem = GetComponent<InventorySlotUI>().slot.item;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;
        currentItem = GetComponent<InventorySlotUI>().slot.item;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)rectTransform.parent,
            eventData.position,
            rootCanvas.worldCamera,
            out Vector2 localPoint
        );
        rectTransform.localPosition = localPoint;

        if (currentItem.category == ItemCategory.Cleaning)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent<PetRubTarget>(out PetRubTarget pet))
                {
                    pet.ApplyCleaning();
                }
            }
        }

        if (currentItem.category == ItemCategory.Toy)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent<PetRubTarget>(out PetRubTarget pet))
                {
                    pet.ApplyMood();
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // If the drop doesn't happen on a valid target (like the pet), eventData.pointerDrag will still be this object.
        // If it was a successful drop, the PetDropTarget would have destroyed this object, but OnEndDrag still runs.
        // The simplest way to handle the snap-back is to just do it. If the object gets destroyed, this won't matter.
        
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
        canvasGroup.blocksRaycasts = true;
    }
}