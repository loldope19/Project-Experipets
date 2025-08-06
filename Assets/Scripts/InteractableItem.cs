using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class InteractableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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

    public void Initialize(ItemData itemToDisplay)
    {
        currentItem = itemToDisplay;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItem.toolType == ToolType.Ball || currentItem.toolType == ToolType.LaserPointer)
        {
            ToolModeController.Instance.SetToolMode(currentItem.toolType);
        }
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

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            // Check the specific tool type to determine the interaction
            switch (currentItem.toolType)
            {
                case ToolType.Towel:
                    if (result.gameObject.TryGetComponent<PetRubTarget>(out PetRubTarget pet))
                    {
                        pet.ApplyCleaning();
                        AudioManager.Instance.PlaySFX(SfxType.wash);
                    }
                    break;

                case ToolType.Broom:
                    if (result.gameObject.TryGetComponent<EnvironmentTarget>(out EnvironmentTarget broomTarget))
                    {
                        if (broomTarget.dirtType == DirtType.Dust) { broomTarget.Clean();
                            AudioManager.Instance.PlaySFX(SfxType.sweep);
                        }
                    }
                    break;

                case ToolType.Gloves:
                    if (result.gameObject.TryGetComponent<EnvironmentTarget>(out EnvironmentTarget gloveTarget))
                    {
                        if (gloveTarget.dirtType == DirtType.Poop) { gloveTarget.Clean(); 
                            AudioManager.Instance.PlaySFX(SfxType.Clean);
                        }
                    }
                    break;
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