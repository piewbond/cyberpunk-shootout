using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPController : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    private TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int health = player.health;
        textMeshPro.text = health.ToString();
    }
}
