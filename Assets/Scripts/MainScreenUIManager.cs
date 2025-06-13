using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenUIManager : MonoBehaviour
{
    public InventoryUI inventoryUI;

    public void FeedButtonPressed()
    {
        inventoryUI.ShowInventory(ItemCategory.Food);
    }

    public void PlayButtonPressed()
    {
        inventoryUI.ShowInventory(ItemCategory.Toy);
    }

    public void CleanButtonPressed()
    {
        inventoryUI.ShowInventory(ItemCategory.Cleaning);
    }
}
