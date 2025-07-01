using System;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager Instance { get; private set; }

    [Header("View Parent Objects")]
    [SerializeField] private GameObject loginView;
    [SerializeField] private GameObject desktopView;
    [SerializeField] private GameObject petCareView;
    // We will add PetCare_View, Shop_View, etc. here later

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    private void Start()
    {
        HideAllViews();
        loginView.SetActive(true);
    }

    public void HideAllViews()
    {
        loginView.SetActive(false);
        desktopView.SetActive(false);
        petCareView.SetActive(false);
    }

    public void GoToDesktopView()
    {
        HideAllViews();
        desktopView.SetActive(true);
    }

    public void GoToLoginView()
    {
        HideAllViews();
        loginView.SetActive(true);
    }

    public void GoToPetCareView()
    {
        HideAllViews();
        petCareView.SetActive(true);
    }
}