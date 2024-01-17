using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarSFXHandler : MonoBehaviour
{
    [Header("Mixers")]
    public AudioMixer audioMixer;

    [Header("Audio scources")]
    public AudioSource tireScreachAudio;
    public AudioSource engineAudio;
    public AudioSource carHitAudio;
    public AudioSource carJumpAudio;
    public AudioSource carLandingAudio;

    float desiredEnginePitch = 0.5f;
    float tireScreachPitch = 0.5f;

    TopDownCarController topDownCarController;


    private void Awake()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioMixer.SetFloat("SFXVolume", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();

        UpdateTireScreachSFX();
    }

    void UpdateEngineSFX()
    {
        float velocityMag = topDownCarController.GetVelocityMagnitude();

        float desiredEngineVolume = velocityMag * 0.05f;

        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        engineAudio.volume = Mathf.Lerp(engineAudio.volume, desiredEngineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMag * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEngineVolume, 0.5f, 2.0f);
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateTireScreachSFX()
    {
        if (topDownCarController.isTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if (isBraking)
            {
                tireScreachAudio.volume = Mathf.Lerp(tireScreachAudio.volume, 1.0f, Time.deltaTime * 10);
                tireScreachPitch = Mathf.Lerp(tireScreachPitch, 0.5f, Time.deltaTime * 10f);
            }
            else
            {
                tireScreachAudio.volume = Mathf.Abs(lateralVelocity) * 0.05f;
                tireScreachPitch = Mathf.Abs(lateralVelocity) * 0.1f;
            }
        }

        else
        {
            tireScreachAudio.volume = Mathf.Lerp(tireScreachAudio.volume, 0, Time.deltaTime * 10);
        }
    }

    public void PlayJumpSFX()
    {
        carJumpAudio.Play();
    }

    public void PlayLandingSFX()
    {
        carLandingAudio.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float relativeVelocity = collision.relativeVelocity.magnitude;

        float volume = relativeVelocity * 0.1f;

        carHitAudio.pitch = Random.Range(0.95f, 1.05f);
        carHitAudio.volume = volume;

        if (!carHitAudio.isPlaying)
        {
            carHitAudio.Play();
        }
    }
}
