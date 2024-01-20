using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class FinishStats : MonoBehaviour
{
    //components
    GameObject[] aiPlayerID;
    GameObject[] playerID;
    GameObject[] racers;
    public GameObject firstPlace; 
    public TextMeshProUGUI firstPName; 
    public TextMeshProUGUI firstPFT; 
    public GameObject secondPlace;
    public TextMeshProUGUI secondPName;
    public TextMeshProUGUI secondPFT;
    public GameObject thirdPlace;
    public TextMeshProUGUI thirdPName;
    public TextMeshProUGUI thirdPFT;
    public GameObject forthPlace;
    public TextMeshProUGUI forthPName;
    public TextMeshProUGUI forthPFT;

    //local variables
    bool statsShown = false;


    // Start is called before the first frame update
    void Awake()
    {
        playerID = GameObject.FindGameObjectsWithTag("Player");
        aiPlayerID = GameObject.FindGameObjectsWithTag("Ai");
        racers = playerID.Concat(aiPlayerID).ToArray();
        racers = racers.OrderByDescending(x => x.GetComponent<RaceTimes>().carScore).ToArray();

    }

    // Update is called once per frame
    void Update()
    {
        if (!statsShown)
        {
            //first place
            firstPName.text = racers[0].GetComponent<RaceTimes>().ID;
            firstPFT.text = racers[0].GetComponent<RaceTimes>().fastestLap.ToString("0.000");

            //second place
            secondPName.text = racers[1].GetComponent<RaceTimes>().ID;
            secondPFT.text = racers[1].GetComponent<RaceTimes>().fastestLap.ToString("0.000");

            //second place
            thirdPName.text = racers[2].GetComponent<RaceTimes>().ID;
            thirdPFT.text = racers[2].GetComponent<RaceTimes>().fastestLap.ToString("0.000");

            //second place
            forthPName.text = racers[3].GetComponent<RaceTimes>().ID;
            forthPFT.text = racers[3].GetComponent<RaceTimes>().fastestLap.ToString("0.000");
        }
    }

}   

