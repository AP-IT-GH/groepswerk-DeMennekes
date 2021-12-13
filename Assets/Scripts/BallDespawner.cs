using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDespawner : MonoBehaviour
{
    public float RewardOnGoal = -2.0f;
    public Agent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("With ball");
            Destroy(collision.gameObject);
            agent.AddReward(RewardOnGoal);
        }
    }
}
