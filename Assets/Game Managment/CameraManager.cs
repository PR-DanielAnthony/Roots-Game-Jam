using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager cameraManager;
    [SerializeField] private float cameraMaxSpeed;
    [SerializeField] private GameObject[] points;

    private int currentPoint = 0;

    private void Start()
    {
        cameraManager = this;
    }

    private void FixedUpdate()
    {
        if (GameManager.currentPlayerRefrence && points.Length > 1)
        {
            Vector3 playerPos3D = GameManager.currentPlayerRefrence.transform.position;
            Vector2 playerPos = new Vector2(playerPos3D.x, playerPos3D.y);
            Vector2 PointA = points[currentPoint].transform.position;
            Vector2 PointB = points[currentPoint + 1].transform.position;
            float dist = DotProduct(PointB - PointA, playerPos - PointA) / (PointB - PointA).magnitude;
            dist = Mathf.Clamp(dist, 0, (PointB - PointA).magnitude);
            Vector2 camMoveVector = ((PointB - PointA).normalized * dist) + PointA - new Vector2(transform.position.x, transform.position.y);
            Debug.Log(camMoveVector);
            camMoveVector = new Vector2(Mathf.Clamp(camMoveVector.x, -cameraMaxSpeed, cameraMaxSpeed), Mathf.Clamp(camMoveVector.y, -cameraMaxSpeed, cameraMaxSpeed));
            transform.Translate(camMoveVector.x, camMoveVector.y, 0);
        }
    }

    private float DotProduct (Vector2 A, Vector2 B)
    {
        return ((A.x * B.x) + (A.y * B.y));
    }

    public void SetCurrentPoint(int currentPoint)
    {
        this.currentPoint = currentPoint;
    }
}
