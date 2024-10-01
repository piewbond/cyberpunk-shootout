using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomStartButton : MonoBehaviour
{
    [SerializeField]
    private Dealer dealer;

    void Update()
    {
        if (!dealer.gameRunning)
        {
            gameObject.SetActive(true);
        }
    }
}
