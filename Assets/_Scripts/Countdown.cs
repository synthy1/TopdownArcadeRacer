using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    //local variables
    float timer = 0;
    [SerializeField] bool gameStart = false;

    //refrences
    TextMeshProUGUI countDownImage = null;

    // Start is called before the first frame update
    void Awake()
    {
        timer = 3;
        gameStart = false;
        Time.timeScale = 0f;
        countDownImage = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart)
        {
            timer -= 0.02f;
            gameObject.SetActive(true);
            countDownImage.text = timer.ToString("0");
            if (timer <= 0)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1f;
                gameStart = true;
            }
        }

        GameManager.instance.GameStart(gameStart);
    }
}
