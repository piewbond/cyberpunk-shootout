using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDescription : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI itemNameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI itemDescriptionText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateText(ModifierController modifierController) {
        itemNameText.text = modifierController.GetModifierName();
        itemDescriptionText.text = modifierController.GetModifierDescription();
    }
}
