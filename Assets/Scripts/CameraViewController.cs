using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewController : MonoBehaviour
{
    [Header("-- Camera Control Settings --")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rotationSpeed = 2.0f;

    [Header("-- UI Canvas Groups --")]
    [SerializeField] private CanvasGroup computerUI_CanvasGroup;
    [SerializeField] private CanvasGroup shopUI_CanvasGroup;

    private Quaternion computerViewRotation = Quaternion.Euler(0, 0, 0);
    private Quaternion shopViewRotation = Quaternion.Euler(0, 180, 0);

    private bool isRotating = false;

    public void RotateToShop()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCamera(shopViewRotation, shopUI_CanvasGroup, computerUI_CanvasGroup));
        }
    }

    public void RotateToComputer()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCamera(computerViewRotation, computerUI_CanvasGroup, shopUI_CanvasGroup));
        }
    }


    private IEnumerator RotateCamera(Quaternion targetRotation, CanvasGroup uiToActivate, CanvasGroup uiToDeactivate)
    {
        isRotating = true;

        uiToDeactivate.interactable = false;
        uiToDeactivate.blocksRaycasts = false;

        while (Quaternion.Angle(cameraTransform.rotation, targetRotation) > 0.1f)
        {
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        cameraTransform.rotation = targetRotation;

        uiToActivate.interactable = true;
        uiToActivate.blocksRaycasts = true;

        isRotating = false;
    }
}
