using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public static GameObject currentPlayerRefrence;
    public static GameManager gameManagerReference;
    [SerializeField] private GameObject playerSpawner;

    [SerializeField] private int deadMushroomsAllowed;
    private GameObject[] deadMushrooms;
    private int currentMushroomIndex = 0;
    [SerializeField] private int timeAllowed;

    [SerializeField] public float timeTillRespawn;

    private bool respawning = true;
    private float currTimeInLoad = 0;

    public int DeadMushroomsAllowed => deadMushroomsAllowed;
    public int deadMushroomCount { get; private set; }

    private GameObject timer;

    private void Awake()
    {
        gameManagerReference = this;    
        deadMushrooms = new GameObject[deadMushroomsAllowed];
        timer = GameObject.Find("Timer");
    }

    public void Respawn()
    {
        respawning = true;
        currTimeInLoad = 0;
        
        if (currentPlayerRefrence)
        {
            if (deadMushrooms[currentMushroomIndex])
                Destroy(deadMushrooms[currentMushroomIndex]);

            deadMushrooms[currentMushroomIndex] = currentPlayerRefrence;
            currentMushroomIndex = (currentMushroomIndex + 1) % deadMushroomsAllowed;
        }
    }

    private void FixedUpdate()
    {
        int pastTime = (int)currTimeInLoad;
        currTimeInLoad += Time.deltaTime;
        /*if (pastTime != (int)currTimeInLoad)
        {
            UpdateCurTime((int)currTimeInLoad);
        }


        if (!respawning && currTimeInLoad > timeAllowed && currentPlayerRefrence.GetComponent<PlayerController>().IsGrounded())
        {
            if (currentPlayerRefrence)
            {
                currentPlayerRefrence.GetComponent<PlayerController>().KillPlayer(PlayerController.DeathType.FloorSpike);
                deadMushroomCount++;
            }
        }*/
        
        if (respawning && currTimeInLoad > timeTillRespawn && playerSpawner && playerPrefab)
        {
            currentPlayerRefrence = Instantiate(playerPrefab, playerSpawner.transform.position, Quaternion.identity);
            currentPlayerRefrence.GetComponent<Animator>().SetBool("Jumping", true);
            currentPlayerRefrence.GetComponent<Animator>().SetTrigger("Jump");
            currentPlayerRefrence.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            currentPlayerRefrence.GetComponent<Rigidbody2D>().AddForce(10 * Vector2.up, ForceMode2D.Impulse);
            respawning = false;
        }
    }

    private void UpdateCurTime(int timeRemaining)
    {
        if (timer)
        {
            timer.GetComponent<TextMeshProUGUI>().text = "" + timeRemaining;
        }
    }
}