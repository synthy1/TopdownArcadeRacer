using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class WheelSmokeHandler : MonoBehaviour
{
    float particleEmitionRate = 0;

    TopDownCarController topDownCarController;

    ParticleSystem particleSystemSmoke;
    ParticleSystem.EmissionModule emissionModule;

    // Start is called before the first frame update
    void Start()
    {
        topDownCarController = GetComponentInParent<TopDownCarController>();

        particleSystemSmoke = GetComponentInParent<ParticleSystem>();

        emissionModule = particleSystemSmoke.emission;

        emissionModule.rateOverTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        particleEmitionRate = Mathf.Lerp(particleEmitionRate,0,Time.deltaTime * 5);
        emissionModule.rateOverTime = particleEmitionRate;

        if(topDownCarController.isTireScreeching(out float lateralVelocity, out bool isBraking))
        {
            if(isBraking)
            {
                particleEmitionRate = 30f;
            }

            else
            {
                particleEmitionRate = Mathf.Abs(lateralVelocity) * 2;
            }
        }
    }
}
