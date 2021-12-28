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
    public Axis movingAxis = Axis.X;
    public GameObject leftPost, rightPost;
    public float movementSpeed, leftPosition, rightPosition;
    public TextMeshPro scoreboard;
    public float RewardOnCatch = 1f;

    private Rigidbody rb;
    private Vector3 agentStartPosition;
    private Vector3 agentPosition;
    private Vector3 defaultVector3 = new Vector3(0.0f, 0.0f, 0.0f);

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

        switch (movingAxis)
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
        ActionSegment<int> actionsOutDiscrete = actionsOut.DiscreteActions;
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
        
        bool moveLeft = (actionsDiscrete[0] == 1);
        bool moveRight = (actionsDiscrete[0] == 2);
        
        if (moveLeft && !IsAgentAtLeftMax())
        {
            transform.position -= GetMovementVector();
        }
        if (moveRight && !IsAgentAtRightMax())
        {
            transform.position += GetMovementVector();
        }
    }

    private bool IsAgentAtLeftMax()
    {
        bool isAgentAtLeftMax = false;
        float positionToUse;

        switch (movingAxis)
        {
            case Axis.X:
                positionToUse = transform.position.x;
                break;
            default:
                positionToUse = transform.position.z;
                break;
        }

        SetPostPositions();
        isAgentAtLeftMax = (positionToUse < leftPosition);

        return isAgentAtLeftMax;
    }

    private bool IsAgentAtRightMax()
    {
        bool isAgentAtRightMax = false;
        float positionToUse;

        switch (movingAxis)
        {
            case Axis.X:
                positionToUse = transform.position.x;
                break;
            default:
                positionToUse = transform.position.z;
                break;
        }

        SetPostPositions();
        isAgentAtRightMax = (positionToUse > rightPosition);

        return isAgentAtRightMax;
    }

    public void SetPostPositions()
    {
        switch (movingAxis)
        {
            case Axis.X:
                leftPosition = leftPost.transform.position.x;
                rightPosition = rightPost.transform.position.x;
                break;
            default:
                leftPosition = leftPost.transform.position.z;
                rightPosition = rightPost.transform.position.z;
                break;
        }

        if (leftPosition > rightPosition)
            SwapPlace();
    }

    private void SwapPlace()
    {
        leftPosition += rightPosition;
        rightPosition = leftPosition - rightPosition;
        leftPosition -= rightPosition;
    }

    private Vector3 GetMovementVector()
    {
        Vector3 movementVector = defaultVector3;
        float movementForce = movementSpeed * Time.deltaTime;
        
        switch (movingAxis)
        {
            case Axis.X:
                movementVector.x = movementForce;
                break;
            default:
                movementVector.z = movementForce;
                break;
        }

        return movementVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            spawner.ClearEnemies();
            AddReward(RewardOnCatch);
            EndEpisode();
        }
    }
}