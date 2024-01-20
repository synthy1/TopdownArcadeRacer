using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PostToggleScript : MonoBehaviour
{
    int toggle = 0;
    public void PostToggle()
    {
        if(toggle == 0)
        {
            toggle = 1;
        }
        else if(toggle == 1)
        {
            toggle = 0;
        }

        GameManager.instance.PostPOn(toggle);
    }

    private void Awake()
    {
        toggle = GameManager.instance._isPostPOn;
        onToggleColor();
    }

    public void onToggleColor()
    {
        if (toggle == 0)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        if (toggle == 1)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
    }
}
