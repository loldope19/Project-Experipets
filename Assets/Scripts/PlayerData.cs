using UnityEngine;

public class PlayerData : MonoBehaviour, IResettable
{
    public static PlayerData Instance { get; private set; }

    public string playerName;
    public bool isPrologueComplete = false;
    public bool isNameConfirmed = false;

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
        }
    }

    public void ResetState()
    {
        playerName = "";
        isPrologueComplete = false;
        isNameConfirmed = false;
        Debug.Log("PlayerData state has been reset.");
    }
}