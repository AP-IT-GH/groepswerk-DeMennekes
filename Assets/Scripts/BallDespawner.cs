using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDespawner : MonoBehaviour
{
    public float RewardOnGoal = -2.0f;
    public Agent agent;
    
    private const string ENEMYTAG = "Ball";

    private void OnTriggerEnter(Collider collision)
    {
        bool ballScored = collision.gameObject.CompareTag(ENEMYTAG);
        
        if (ballScored)
        {
            Destroy(collision.gameObject);
            agent.RewardAgent(RewardOnGoal);
            agent.EndEpisodeActions();
        }
    }
}
