using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineController : MonoBehaviour
{
    [SerializeField]
    GameObject[] bulletObjects;

    void Start()
    {

    }

    public void ShowBullets(int liveBullets, int blankBullets)
    {
        for (int i = 0; i < bulletObjects.Length; i++)
        {
            SpriteRenderer spriteRenderer = bulletObjects[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                if (i < liveBullets)
                {
                    spriteRenderer.color = Color.green;
                }
                else if (i < liveBullets + blankBullets)
                {
                    spriteRenderer.color = Color.red;
                }
                else
                {
                    spriteRenderer.color = Color.clear;
                }
            }
        }
    }

}
