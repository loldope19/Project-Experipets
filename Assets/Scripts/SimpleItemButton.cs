using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleItemButton : MonoBehaviour
{
    public ItemData containedItem;
    private Button button;

    // Start is called before the first frame update
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (containedItem != null)
        {
            InventoryManager.Instance.UseItem(containedItem);
        }
    }
}
