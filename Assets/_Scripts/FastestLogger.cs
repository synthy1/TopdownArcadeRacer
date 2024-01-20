using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FastestLogger : MonoBehaviour
{

    //components
    TextMeshProUGUI FastestTimeText;

    // Start is called before the first frame update
    private void Awake()
    {
        FastestTimeText = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        FastestTimeText.text = "No Record";
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance._fastestTime < 25.000)
        {
            FastestTimeText.text = GameManager.instance._fastestTime.ToString("0.000");
        }
    }
}
