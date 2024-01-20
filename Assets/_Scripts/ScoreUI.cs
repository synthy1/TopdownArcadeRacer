using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    //local variables
    int lap;

    //refrences
    RaceTimes scoreHandler;

    //local refrences
    TextMeshProUGUI scoreTxt;

    // Start is called before the first frame update
    void Awake()
    {
        scoreTxt = GetComponentInChildren<TextMeshProUGUI>();
        scoreHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<RaceTimes>();
    }

    // Update is called once per frame
    void Update()
    {
        lap = scoreHandler.carScore;

        scoreTxt.text = lap.ToString("0") + "/12";
    }
}
