using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private PlayerController.DeathType hazardType;
    [SerializeField] private Vector2 offset = Vector2.zero;
    [SerializeField] private bool right = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(GameManager.currentPlayerRefrence))
        {
            other.GetComponent<PlayerController>().KillPlayer(hazardType, this.gameObject, offset, right);
        }
    }
}
