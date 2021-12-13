using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class Agent : Unity.MLAgents.Agent
{
    private float SmallJumpAmount = 8.0f;
    public float BigJumpAmount = 16.0f;
    public TextMeshPro tmp;
    private Rigidbody rb;
    private Vector3 agentStartPosition;
    public BallSpawner spawner;

    private bool canJump = true;

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = agentStartPosition;
        
        spawner.ClearBalls();
        //DestroyEnemies();
    }

    private static void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }
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
        if (Input.GetKey(KeyCode.Space))
            actionsOutDiscrete[0] = 1;
        
        if (Input.GetKey(KeyCode.UpArrow))
            actionsOutDiscrete[0] = 2;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionsDiscrete = actions.DiscreteActions;
        if (actionsDiscrete[0] == 1)
        {
            Debug.Log("Small jump");
            SmallJump();
        }
        else if (actionsDiscrete[0] == 2)
        {
            Debug.Log("Big jump");
            BigJump();
        }
        
    }

    private void SmallJump()
    {
        if (canJump)
        {
            canJump = false;
            rb.AddForce(Vector2.up * SmallJumpAmount, ForceMode.Impulse);
            AddReward(-0.005f);
        }
    }
    
    private void BigJump()
    {
        if (canJump)
        {
            canJump = false;
            rb.AddForce(Vector2.up * SmallJumpAmount, ForceMode.Impulse);
            AddReward(-0.015f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        
        if (collision.gameObject.CompareTag("Plane"))
            canJump = true;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");
            AddReward(-1f);
            EndEpisode();
        }
    }
}
