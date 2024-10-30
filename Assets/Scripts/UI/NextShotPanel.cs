using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NextShotPanel : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI textMeshPro;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    bool ShowAlways;

    [SerializeField]
    Weapon weapon;
    void Start()
    {
        ResetPanel();
    }
    void Update()
    {
        if (ShowAlways)
        {
            ShowNext(weapon.isNextShotLive());
        }
    }

    public void ShowNext(bool isLive)
    {
        if (isLive)
        {
            textMeshPro.text = ": Live";
            spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            textMeshPro.text = ": Blank";
            spriteRenderer.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }

    public void ResetPanel()
    {
        textMeshPro.text = "";
        spriteRenderer.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
}
