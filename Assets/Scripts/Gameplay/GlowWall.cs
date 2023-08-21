using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowWall : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount == 0)
        {
            gameObject.layer = 8;
        }
    }
}
