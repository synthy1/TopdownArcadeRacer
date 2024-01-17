using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car Setings")]
    public float accelerationFactor = 30.0f;
    public float driftFactor = 0.95f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20f;

    [Header("Sprites")]
    public SpriteRenderer carSpriteRenderer;
    public SpriteRenderer carShadowRenderer;

    [Header("Jumping")]
    public AnimationCurve jumpCurve;
    public ParticleSystem jumpParticalSystem;

    //local vairiables
    float accelerationInput = 0f;
    float steeringInput = 0f;

    float rotationAngle = 0f;

    float velocityVsUp = 0f;

    bool isJumping = false;

    //comnponents
    Rigidbody2D carRB2D;
    Collider2D carCollider;
    CarSFXHandler carSFXHandler;

    private void Awake()
    {
        carRB2D = GetComponent<Rigidbody2D>();
        carCollider = GetComponentInChildren<Collider2D>();
        carSFXHandler = GetComponent<CarSFXHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        if (isJumping && accelerationInput < 0f)
        {
            accelerationInput = 0f;
        }

        if(accelerationInput == 0)
        {
            carRB2D.drag = Mathf.Lerp(carRB2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRB2D.drag = 0;
        }

        velocityVsUp = Vector2.Dot(transform.up, carRB2D.velocity);

        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        if (carRB2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0 && !isJumping)
        {
            return;
        }


        //create force for engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //apply force
        carRB2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedBeforAllowTurningFactor = (carRB2D.velocity.magnitude / 8);
        minSpeedBeforAllowTurningFactor = Mathf.Clamp01(minSpeedBeforAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforAllowTurningFactor;

        carRB2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 fowardVelocity = transform.up * Vector2.Dot(carRB2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRB2D.velocity, transform.right);

        carRB2D.velocity = fowardVelocity + rightVelocity * driftFactor;

    }

    public float GetVelocityMagnitude()
    {
        return carRB2D.velocity.magnitude;
    }

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRB2D.velocity);
    }

    public bool isTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if(isJumping)
        {
            return false;
        }

        if(accelerationInput < 0 && velocityVsUp > 0)
        {
            isBraking = true;
            return true;
        }

        if(Mathf.Abs(GetLateralVelocity()) > 4.0f)
        {
            return true;
        }

        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public void Jump(float jumpHeightScale, float jumpPushScale)
    {
        if(!isJumping)
        {
            StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
        }
    }

    private IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale)
    {
        isJumping = true;

        float jumpStartTime = Time.time;
        float jumpDuration = carRB2D.velocity.magnitude * 0.05f;

        jumpHeightScale = jumpHeightScale * carRB2D.velocity.magnitude * 0.05f;
        jumpHeightScale = Mathf.Clamp(jumpHeightScale, 0.0f, 1.0f);

        carCollider.enabled = false;

        carSFXHandler.PlayJumpSFX();

        carShadowRenderer.sortingLayerName = "Flying";
        carSpriteRenderer.sortingLayerName = "Flying";

        carRB2D.AddForce(carRB2D.velocity.normalized * jumpPushScale * 10, ForceMode2D.Impulse);

        while(isJumping)
        {
            //calculate percentage of how far we are into the jump between 0 - 1.0
            float jumpCompletedPercentage = (Time.time - jumpStartTime) / jumpDuration;
            jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);

            //scale car during jump
            carSpriteRenderer.transform.localScale = Vector3.one + Vector3.one * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

            //scale shadow during jump
            carShadowRenderer.transform.localScale = carSpriteRenderer.transform.localScale * 0.75f;

            //offset the shadow
            carShadowRenderer.transform.localPosition = new Vector3(1, -1, 0.0f) * 3 * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

            if(jumpCompletedPercentage == 1.0f)
            {
                break;
            }

            yield return null;
        }

        if (Physics2D.OverlapCircle(transform.position, 1.5f))
        {
            isJumping = false;

            Jump(0.2f, 0.6f);
        }
        else
        {

            //handdeling landing
            carSpriteRenderer.transform.localScale = Vector3.one;

            //resetting shadow
            carShadowRenderer.transform.localPosition = Vector3.zero;
            carShadowRenderer.transform.localScale = carSpriteRenderer.transform.localScale;

            carCollider.enabled = true;

            carShadowRenderer.sortingLayerName = "Default";
            carSpriteRenderer.sortingLayerName = "Default";

            if(jumpHeightScale > 0.2f)
            {
                jumpParticalSystem.Play();

                carSFXHandler.PlayLandingSFX();
            }

            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jump"))
        {
            JumpData jumpData = collision.GetComponent<JumpData>();
            Jump(jumpData.jumpHeightScale, jumpData.jumpPushScale);
        }
    }

}
