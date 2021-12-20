using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class Agent : Unity.MLAgents.Agent
{
    public Axis moveAlong = Axis.X;
    public GameObject LeftPost;
    public GameObject RightPost;


    public float movementSpeed = 2.0f;
    public TextMeshPro tmp;
    private Rigidbody rb;
    private Vector3 agentStartPosition;
    public BallSpawner spawner;

    public override void OnEpisodeBegin()
    {
        Debug.Log("Episode started");
        this.transform.localPosition = agentStartPosition;
        
        //spawner.ClearBalls();
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
        tmp.text = GetCumulativeReward().ToString(CultureInfo.CurrentCulture);
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
        if (actionsDiscrete[0] == 1 && !IsAgentAtAMaximum())
        {
            transform.localPosition -= GetMovementVector();
        }
        
        if (actionsDiscrete[0] == 2 && !IsAgentAtAMaximum())
        {
            transform.localPosition += GetMovementVector();
        }
    }

    private Vector3 GetMovementVector()
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
        var position = transform.localPosition;
        float radius, negativeRadius, goalWidth = GetGoalWidth();
        switch (moveAlong)
        {
            case Axis.X:
                radius = (goalWidth / 2.0f) + agentStartPosition.x;
                negativeRadius = (goalWidth / -2.0f) + agentStartPosition.x;
                isAgentAtMaximum = (position.x < negativeRadius || position.x > radius);
                break;
            case Axis.Y:
                radius = (goalWidth / 2.0f) + agentStartPosition.y;
                negativeRadius = (goalWidth / -2.0f) + agentStartPosition.y;
                isAgentAtMaximum = (position.y < negativeRadius || position.y > radius);
                break;
            case Axis.Z:
                radius = (goalWidth / 2.0f) + agentStartPosition.z;
                negativeRadius = (goalWidth / -2.0f) + agentStartPosition.z;
                isAgentAtMaximum = (position.z < negativeRadius || position.z > radius);
                break;
        }

        return isAgentAtMaximum;
    }
    
    private float GetGoalWidth()
    {
        float result,startWidth, endWidth;
        switch (moveAlong)
        {
            default:
                startWidth = LeftPost.transform.localPosition.x;
                endWidth = RightPost.transform.localPosition.x;
                break;
            case Axis.Y:
                startWidth = LeftPost.transform.localPosition.y;
                endWidth = RightPost.transform.localPosition.y;
                break;
            
            case Axis.Z:
                startWidth = LeftPost.transform.localPosition.z;
                endWidth = RightPost.transform.localPosition.z;
                break;
        }

        if (startWidth < endWidth)
            result = endWidth - startWidth;
        else
            result = startWidth - endWidth;
        return result;
    }
}
