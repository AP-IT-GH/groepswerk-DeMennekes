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
    public GameObject leftPost;
    public GameObject rightPost;
    public float movementSpeed = 2.0f;
    public TextMeshPro tmp;
    
    private Rigidbody rb;
    private Vector3 agentStartPosition;
    private Vector3 agentPosition;
    private Vector3 defaultVector3 = new Vector3(0.0f, 0.0f, 0.0f);
    private float startWidth, endWidth;
    
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
        if (actionsDiscrete[0] == 1 && !IsAgentAtNegativeMax())
        {
            transform.localPosition -= GetMovementVector();
        }
        
        if (actionsDiscrete[0] == 2 && !IsAgentAtPositiveMax())
        {
            transform.localPosition += GetMovementVector();
        }
    }
    
    private bool IsAgentAtNegativeMax()
    {
        bool isAgentAtNegativeMax = false;
        float positionToUse = transform.localPosition.x;
        
        switch (moveAlong)
        {
            case Axis.X:
                positionToUse = transform.localPosition.x;
                break;
            case Axis.Y:
                positionToUse = transform.localPosition.y;
                break;
            case Axis.Z:
                positionToUse = transform.localPosition.z;
                break;
        }
        
        SetPostPositions();
        isAgentAtNegativeMax = (positionToUse < startWidth);
        
        return isAgentAtNegativeMax;
    }
    
    private bool IsAgentAtPositiveMax()
    {
        bool isAgentAtPositiveMax = false;
        float positionToUse = transform.localPosition.x;
        
        switch (moveAlong)
        {
            case Axis.X:
                positionToUse = transform.localPosition.x;
                break;
            case Axis.Y:
                positionToUse = transform.localPosition.y;
                break;
            case Axis.Z:
                positionToUse = transform.localPosition.z;
                break;
        }
        
        SetPostPositions();
        isAgentAtPositiveMax = (positionToUse > endWidth);
        
        return isAgentAtPositiveMax;
    }

    private void SetPostPositions()
    {
        switch (moveAlong)
        {
            default:
                startWidth = leftPost.transform.localPosition.x;
                endWidth = rightPost.transform.localPosition.x;
                break;
            case Axis.Y:
                startWidth = leftPost.transform.localPosition.y;
                endWidth = rightPost.transform.localPosition.y;
                break;
            
            case Axis.Z:
                startWidth = leftPost.transform.localPosition.z;
                endWidth = rightPost.transform.localPosition.z;
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
            AddReward(1f);
            EndEpisode();
        }
    }
}
