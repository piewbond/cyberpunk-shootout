using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : MonoBehaviour
{
    public virtual void Apply()
    {
        Debug.Log("Applying modifier to player");
    }

}
