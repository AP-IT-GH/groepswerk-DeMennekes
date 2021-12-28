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
    public float leftWidth, rightWidth;

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
        isAgentAtNegativeMax = (positionToUse < leftWidth);

        return isAgentAtNegativeMax;
    }

    private bool IsAgentAtRightMax()
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
        isAgentAtPositiveMax = (positionToUse > rightWidth);

        return isAgentAtPositiveMax;
    }

    public void SetPostPositions()
    {
        switch (moveAlong)
        {
            default:
                leftWidth = leftPost.transform.position.x;
                rightWidth = rightPost.transform.position.x;
                break;
            case Axis.Y:
                leftWidth = leftPost.transform.position.y;
                rightWidth = rightPost.transform.position.y;
                break;

            case Axis.Z:
                leftWidth = leftPost.transform.position.z;
                rightWidth = rightPost.transform.position.z;
                break;
        }

        if (leftWidth > rightWidth)
            SwapPlace();
    }

    private void SwapPlace()
    {
        leftWidth += rightWidth;
        rightWidth = leftWidth - rightWidth;
        leftWidth -= rightWidth;
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