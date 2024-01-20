using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    //refrence

    public GameObject uiEndscreen;
    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.IsGameOver() == true)
        {
            uiEndscreen.SetActive(true);
            Time.timeScale = 0.2f;
        }
        else uiEndscreen.SetActive(false);



    }
}
