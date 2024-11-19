using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private float timer = 0;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float alpha = Mathf.PingPong(timer, 1f);
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
}
