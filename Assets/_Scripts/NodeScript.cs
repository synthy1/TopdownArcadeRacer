using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    [Header("Speed set when reached a node")]
    public float maxSpeed = 0;

    [Header("Node aiming at (havnt reached)")]
    public float minDistanceToReachWaypoint = 5.0f;

    public NodeScript[] nextNode;
}
