using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private ModifierController modifierPrefab;
    [SerializeField]
    private RectTransform contentPanel;
    List<ModifierController> modifierControllers = new List<ModifierController>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            ModifierController modifierController = Instantiate(modifierPrefab, contentPanel);
            modifierController.transform.SetParent(contentPanel);
            modifierControllers.Add(modifierController);
        }
    }

}