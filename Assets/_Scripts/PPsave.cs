using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPsave : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        gameObject.GetComponent<PostProcessVolume>().weight = GameManager.instance._isPostPOn;
    }
}
