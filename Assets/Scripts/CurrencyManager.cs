using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    [Header("-- Currency --")]
    public int currentBalance = 200;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // not sure if i do want it to persist between scenes.
        }
    }

    public bool CanAfford(int amount)
    {
        return currentBalance >= amount;
    }

    public void AddCurrency(int amount)
    {
        currentBalance += amount;
        Debug.Log($"Added {amount} currency. New balance: {currentBalance}");
        // update UI here afterwards
    }

    public bool SpendCurrency(int amount)
    {
        if (CanAfford(amount))
        {
            currentBalance -= amount;
            Debug.Log($"Spent {amount} currency. New balance: {currentBalance}");
            // update UI here
            return true;
        }

        Debug.Log("Not enough currency for this transaction.");
        return false;
    }
}
