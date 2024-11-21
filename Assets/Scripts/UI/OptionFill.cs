using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionFill : MonoBehaviour
{
    public bool isActive = false;
    public void ToggleActive()
    {
        isActive = !isActive;
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
