using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InfoPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject infoText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowInfo(string text)
    {

        TextMeshProUGUI textMeshPro = infoText.GetComponent<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            textMeshPro.text = text;
        }
    }
}
