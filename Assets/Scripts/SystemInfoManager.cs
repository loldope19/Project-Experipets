using System;
using UnityEngine;

public class SystemInfoManager : MonoBehaviour
{
    public static SystemInfoManager Instance { get; private set; }
    public string pcUsername { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            RetrieveSystemInfo();
        }
    }

    private void RetrieveSystemInfo()
    {
        try
        {
            pcUsername = Environment.UserName;
            Debug.Log($"Successfully retrieved PC Username: {pcUsername}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to retrieve system username: {e.Message}");
            pcUsername = "Caretaker";
        }
    }
}