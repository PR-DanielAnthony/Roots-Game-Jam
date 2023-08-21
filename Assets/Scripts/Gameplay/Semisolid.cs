using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Semisolid : MonoBehaviour
{
    private BoxCollider2D semiSolidCollider;
    private float topOfSemisolid;

    private void Start()
    {
        semiSolidCollider = gameObject.GetComponent<BoxCollider2D>();
        topOfSemisolid = semiSolidCollider.bounds.center.y;
    }

    private void FixedUpdate()
    {
        if (GameManager.currentPlayerRefrence)
        {
            float playerPos = GameManager.currentPlayerRefrence.transform.position.y;
            semiSolidCollider.enabled = topOfSemisolid + semiSolidCollider.bounds.size.y < playerPos - .45;
        }
    }
}
