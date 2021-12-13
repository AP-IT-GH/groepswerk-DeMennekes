using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class Agent : Unity.MLAgents.Agent
{
    public Axis moveAlong = Axis.X;
    public float goalWidth = 10.0f;
    public float movementSpeed = 1.0f;
    public TextMeshPro tmp;
    private Rigidbody rb;
    private Vector3 agentStartPosition;
    public BallSpawner spawner;

    private bool canJump = true;

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = agentStartPosition;
        
        spawner.ClearBalls();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agentStartPosition = this.transform.localPosition;
    }
    
    // Update is called once per frame
    void Update()
    {
        tmp.text = GetCumulativeReward().ToString();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var actionsOutDiscrete = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.LeftArrow))
            actionsOutDiscrete[0] = 1;
        
        if (Input.GetKey(KeyCode.RightArrow))
            actionsOutDiscrete[0] = 2;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionsDiscrete = actions.DiscreteActions;
        if (actionsDiscrete[0] == 1 && !IsAgentAtAMaximum())
        {
            transform.position -= getMovementVector();
        }
        else if (actionsDiscrete[0] == 2 && !IsAgentAtAMaximum())
        {
            transform.position += getMovementVector();
        }
    }

    private Vector3 getMovementVector()
    {
        Vector3 movementVector = new Vector3();
        switch (moveAlong)
        {
            case Axis.X:
                movementVector = new Vector3(movementSpeed * Time.deltaTime, 0.0f, 0.0f);
                break;
            case Axis.Y:
                movementVector = new Vector3(0.0f,movementSpeed * Time.deltaTime, 0.0f);
                break;
            case Axis.Z:
                movementVector = new Vector3(0.0f, 0.0f ,movementSpeed * Time.deltaTime);
                break;
        }

        return movementVector;
    }

    private bool IsAgentAtAMaximum()
    {
        bool isAgentAtMaximum = false;
        var position = transform.position;
        var radius = (goalWidth / 2.0f);
        var negativeRadius = (goalWidth / -2.0f);
        switch (moveAlong)
        {
            case Axis.X:
                isAgentAtMaximum = (position.x < negativeRadius || position.x > radius);
                break;
            case Axis.Y:
                isAgentAtMaximum = (position.y < negativeRadius || position.y > radius);
                break;
            case Axis.Z:
                isAgentAtMaximum = (position.z < negativeRadius || position.z > radius);
                break;
        }

        return isAgentAtMaximum;
    }
}
