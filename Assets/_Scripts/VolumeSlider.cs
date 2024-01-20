using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    //components
    Slider slider;

    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (this.gameObject.CompareTag("SFX"))
        {
            slider.value = GameManager.instance._sFXVolume;
        }

        else if (this.gameObject.CompareTag("Music"))
        {
            slider.value = GameManager.instance._musicVolume;
        }
    }

    public void UpdateMusicVolume()
    {
        GameManager.instance._musicVolume = slider.value;
    }
    public void UpdateSFXVolume()
    {
        GameManager.instance._sFXVolume = slider.value;
    }
}
