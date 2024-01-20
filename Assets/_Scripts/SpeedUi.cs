using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedUi : MonoBehaviour
{
    //local variables
    float speed;

    //refrences
    Rigidbody2D playerRB2D;

    //local refrences
    TextMeshProUGUI speedTxt;

    // Start is called before the first frame update
    void Awake()
    {
        speedTxt = GetComponentInChildren<TextMeshProUGUI>();
        playerRB2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = playerRB2D.velocity.magnitude;

        speedTxt.text = speed.ToString("0");
    }
}
