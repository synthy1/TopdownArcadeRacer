using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CarAiPlayer : MonoBehaviour
{
    public enum AIMode { followPlayer, followWaypoints };

    [Header("Ai Settings")]
    public AIMode aiMode;
    public float maximumSpeed = 16.0f;

    //local variables
    Vector3 targetPosition = Vector3.zero;
    Transform targetTransform = null;

    //waypoints
    NodeScript currentNode = null;
    NodeScript[] allNodes;

    //components
    TopDownCarController topDownCarController;

    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>();
        allNodes = FindObjectsOfType<NodeScript>();    
    }

    // Update is called once per frame and is frame dependent
    void FixedUpdate()
    {
        Vector2 inputVector = Vector2.zero;

        switch (aiMode)
        {
            case AIMode.followPlayer:
                FollowPlayer();
                break;
            case AIMode.followWaypoints:
                FollowNodes();
                break;
        }

        inputVector.x = TurnTowardTarget();
        inputVector.y = ApplyThrottleAndBreak(inputVector.x);

        //send inputs to the controller
        topDownCarController.SetInputVector(inputVector);
    }

    void FollowPlayer()
    {
        if (targetTransform == null)
        {
            targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }
    }

    void FollowNodes()
    {
        //picks closest node
        if (currentNode== null)
        {
            currentNode = FindClosesNode();
        }

        //set target possition
        if (currentNode != null)
        {
            targetPosition = currentNode.transform.position;

            //store how close to node we are
            float distanceToNode = (targetPosition - transform.position).magnitude;

            if(distanceToNode <= currentNode.minDistanceToReachWaypoint)
            {
                if (currentNode.maxSpeed > 0)
                {
                    maximumSpeed = currentNode.maxSpeed;
                }
                else maximumSpeed = 1000;

                //if we are close switch to next node
                currentNode = currentNode.nextNode[Random.Range(0, currentNode.nextNode.Length)];
            }
        }
    }

    NodeScript FindClosesNode()
    {
        return allNodes
            .OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
            .FirstOrDefault();
    }

    float TurnTowardTarget()
    {
        Vector2 vectorToTarget = targetPosition - transform.position;
        vectorToTarget.Normalize();

        //calculates angle towards target
        float angleToTarget = Vector2.SignedAngle(transform.up, vectorToTarget);
        angleToTarget *= -1;

        //removes jitter
        float steerAmmount = angleToTarget / 45.0f;

        steerAmmount = Mathf.Clamp(steerAmmount, -1.0f, 1.0f);

        return steerAmmount;
    }

    float ApplyThrottleAndBreak(float inputX)
    {
        if(topDownCarController.GetVelocityMagnitude() > maximumSpeed)
        {
            return 0;
        }

        return 1.05f - Mathf.Abs(inputX) / 1.0f;
    }
}
