using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DefaultNamespace;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

[SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
public class Agent : Unity.MLAgents.Agent
{
    public Axis moveAlong = Axis.X;
    public GameObject leftPost;
    public GameObject rightPost;
    public float movementSpeed;
    public TextMeshPro scoreboard;

    private Rigidbody rb;
    private Vector3 agentStartPosition;
    private Vector3 agentPosition;
    private Vector3 defaultVector3 = new Vector3(0.0f, 0.0f, 0.0f);
    public float startWidth, endWidth;

    public BallSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        agentStartPosition = this.transform.position;
        SetAgentConstraints(); // Can agent rotate or move?
    }

    private void SetAgentConstraints()
    {
        rb = GetComponent<Rigidbody>();

        RigidbodyConstraints rigidBodyConstraints;
        
        RigidbodyConstraints freezeRotations = RigidbodyConstraints.FreezeRotationY |
                                               RigidbodyConstraints.FreezeRotationX |
                                               RigidbodyConstraints.FreezeRotationZ;
        RigidbodyConstraints freezePositionY = RigidbodyConstraints.FreezePositionY;
        RigidbodyConstraints defaultFreezes = freezeRotations | freezePositionY;

        switch (moveAlong)
        {
            case Axis.Z:
                rigidBodyConstraints = defaultFreezes | RigidbodyConstraints.FreezePositionX;
                break;
            default:
                rigidBodyConstraints = defaultFreezes | RigidbodyConstraints.FreezePositionZ;
                break;
        }

        rb.constraints = rigidBodyConstraints;
    }

    public override void OnEpisodeBegin()
    {
        this.transform.position = agentStartPosition; // Set agent on startposition
        spawner.SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        scoreboard.text = GetCumulativeReward().ToString(CultureInfo.CurrentCulture);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actionsOutDiscrete = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            actionsOutDiscrete[0] = 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            actionsOutDiscrete[0] = 2;
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionsDiscrete = actions.DiscreteActions;
        if (actionsDiscrete[0] == 1 && !IsAgentAtPositiveMax())
        {
            transform.position -= GetMovementVector();
        }

        if (actionsDiscrete[0] == 2 && !IsAgentAtNegativeMax())
        {
            transform.position += GetMovementVector();
        }
    }

    private bool IsAgentAtNegativeMax()
    {
        bool isAgentAtNegativeMax = false;
        float positionToUse = transform.position.x;

        switch (moveAlong)
        {
            case Axis.X:
                positionToUse = transform.position.x;
                break;
            case Axis.Y:
                positionToUse = transform.position.y;
                break;
            case Axis.Z:
                positionToUse = transform.position.z;
                break;
        }

        SetPostPositions();
        isAgentAtNegativeMax = (positionToUse < startWidth);

        return isAgentAtNegativeMax;
    }

    private bool IsAgentAtPositiveMax()
    {
        bool isAgentAtPositiveMax = false;
        float positionToUse = transform.position.x;

        switch (moveAlong)
        {
            case Axis.X:
                positionToUse = transform.position.x;
                break;
            case Axis.Y:
                positionToUse = transform.position.y;
                break;
            case Axis.Z:
                positionToUse = transform.position.z;
                break;
        }

        SetPostPositions();
        isAgentAtPositiveMax = (positionToUse > endWidth);

        return isAgentAtPositiveMax;
    }

    public void SetPostPositions()
    {
        switch (moveAlong)
        {
            default:
                startWidth = leftPost.transform.position.x;
                endWidth = rightPost.transform.position.x;
                break;
            case Axis.Y:
                startWidth = leftPost.transform.position.y;
                endWidth = rightPost.transform.position.y;
                break;

            case Axis.Z:
                startWidth = leftPost.transform.position.z;
                endWidth = rightPost.transform.position.z;
                break;
        }

        if (startWidth > endWidth)
            SwapPlace();
    }

    private void SwapPlace()
    {
        startWidth += endWidth;
        endWidth = startWidth - endWidth;
        startWidth -= endWidth;
    }

    private Vector3 GetMovementVector()
    {
        Vector3 movementVector = defaultVector3;

        switch (moveAlong)
        {
            case Axis.X:
                movementVector.x = movementSpeed * Time.deltaTime;
                break;
            case Axis.Y:
                movementVector.y = movementSpeed * Time.deltaTime;
                break;
            case Axis.Z:
                movementVector.z = movementSpeed * Time.deltaTime;
                break;
        }

        return movementVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball caught");
            spawner.ClearEnemies();
            // Destroy(collision.gameObject);
            AddReward(1f);
            EndEpisode();
        }
    }
}