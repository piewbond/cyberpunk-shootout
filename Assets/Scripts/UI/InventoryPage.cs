using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    private InventoryItem modifierPrefab;
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    List<InventoryItem> inventoryItems = new List<InventoryItem>();

    void Start()
    {
    }

    void Update()
    {
        UpdateModifers();
        if (player.IsActivePlayer())
            SetItemsClickable(true);
        else
            SetItemsClickable(false);
        
    }

    public void UpdateModifers()
    {
        List<Modifier> modifiers = player.GetModifiers();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (i < modifiers.Count)
            {
                inventoryItems[i].SetData(modifiers[i].GetSprite(), 1, modifiers[i]);
            }
            else
            {
                inventoryItems[i].ResetData();
            }
        }
    }

    private void SetItemsClickable(bool clickable) {
        foreach (InventoryItem item in inventoryItems) {
            item.SetClickable(clickable);
        }
    }

}
