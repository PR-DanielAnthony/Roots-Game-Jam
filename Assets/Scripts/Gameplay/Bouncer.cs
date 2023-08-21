using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private bool factorMomentum;
    [SerializeField] private Vector2 bounceVector;

    private void Start()
    {
    }

    public void setVals(bool factorMomentum, Vector2 bounceVector)
    {
        this.factorMomentum = factorMomentum;
        this.bounceVector = bounceVector;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (factorMomentum)
            {
                Vector2 augmentableVector = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                if (augmentableVector.Equals(Vector2.zero))
                {
                    return;
                }
                augmentableVector = RotateVector(augmentableVector, -transform.rotation.z * 180 / Mathf.PI);
                augmentableVector = augmentableVector.magnitude < bounceVector.magnitude ? augmentableVector * (bounceVector.magnitude / augmentableVector.magnitude) : augmentableVector;
                augmentableVector = new Vector2(augmentableVector.x, Mathf.Abs(augmentableVector.y));
                augmentableVector = RotateVector(augmentableVector, transform.rotation.z * 180 / Mathf.PI);
                collision.gameObject.GetComponent<PlayerController>().BouncePlayer(augmentableVector);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().BouncePlayer(bounceVector);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            if (factorMomentum)
            {
                Vector2 augmentableVector = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                if (augmentableVector.Equals(Vector2.zero))
                {
                    return;
                }
                augmentableVector = RotateVector(augmentableVector, -transform.rotation.z * 180 / Mathf.PI);
                augmentableVector = augmentableVector.magnitude < bounceVector.magnitude ? augmentableVector * (bounceVector.magnitude / augmentableVector.magnitude) : augmentableVector;
                augmentableVector = new Vector2(augmentableVector.x, Mathf.Abs(augmentableVector.y));
                augmentableVector = RotateVector(augmentableVector, transform.rotation.z * 180 / Mathf.PI);
                collision.gameObject.GetComponent<PlayerController>().BouncePlayer(augmentableVector);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerController>().BouncePlayer(bounceVector);
            }
        }
    }

    public static Vector2 RotateVector(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}