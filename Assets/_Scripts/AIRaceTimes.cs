using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRaceTimes : MonoBehaviour
{
    //comnponents
    Rigidbody2D carRB2D;
    PolygonCollider2D carCollider;
    CarSFXHandler carSFXHandler;

    [Header("ID")]
    public string ID;

    [Header("LapLogic")]
    [SerializeField] float lapTime = 0f;
    [SerializeField] bool lapStart;
    [SerializeField] int firstLap = 0;
    [SerializeField] float RecordLapTime;

    [Header("CarScore")]
    public int carScore = 0;
    public float fastestLap = 99999999;
    bool checkPointReached;


    private void Awake()
    {
        carRB2D = GetComponent<Rigidbody2D>();
        carCollider = GetComponentInChildren<PolygonCollider2D>();
        carSFXHandler = GetComponent<CarSFXHandler>();
        carScore = 0;
        fastestLap = 50.00f;
    }

    private void Update()
    {
        if (!GameManager.instance.IsGameOver())
        {
            if (firstLap == 0)
            {
                if (GameManager.instance.HasGameStarted())
                {
                    lapTime += Time.deltaTime;

                    if (carScore > 0)
                    {
                        firstLap++;
                    }
                    lapStart = true;
                }
            }
            else if (firstLap != 0 && GameManager.instance.HasGameStarted())
            {
                lapTime += Time.deltaTime;
            }
        }
    }
    public void RecordCurrentLapTime(float timetaken)
    {
        RecordLapTime = timetaken;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Goal"))
        {
            if (checkPointReached)
            {
                lapStart = false;
                RecordCurrentLapTime(lapTime);
                if (!lapStart)
                {
                    if (RecordLapTime < fastestLap)
                    {
                        fastestLap = RecordLapTime;
                        lapTime = 0;
                        lapStart = true;
                    }
                    else
                    {
                        lapTime = 0;
                        lapStart = true;
                    }
                }
                carScore++;
                checkPointReached = false;
            }
            else if (carScore == 12)
            {
                GameManager.instance.GameOver(true);
                carScore = 0;
            }
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            checkPointReached = true;
        }
    }
}
