using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    [SerializeField] private int zoneId = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(GameManager.currentPlayerRefrence))
        {
            CameraManager.cameraManager.SetCurrentPoint(zoneId);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(GameManager.currentPlayerRefrence))
        {
            CameraManager.cameraManager.SetCurrentPoint(zoneId);
        }
    }
}
