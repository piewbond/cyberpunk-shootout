using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI itemNameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemDescriptionText;

    public void UpdateText(Modifier modifier)
    {
        itemNameText.text = modifier.GetModifierName();
        itemDescriptionText.text = modifier.GetModifierDescription();
    }
}
