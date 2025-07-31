using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action OnGameOver;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Debug.Log("GAME OVER");
        OnGameOver?.Invoke();
    }

    public void ResetGame()
    {
        IResettable[] resettableObjects = FindObjectsOfType<PlayerData>();
        foreach (var obj in resettableObjects)
        {
            obj.ResetState();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}