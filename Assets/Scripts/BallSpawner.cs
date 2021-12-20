using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;
    public Agent agent;
    
    private Vector3 spawnVector;
    private float LeftMostSpawnArea;
    private float RightMostSpawnArea = 10f;
    private float random;
    private const string ENEMYTAG = "Ball";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnBall()
    {
        Transform spawnPosition = SetSpawnPosition();
        GameObject go = Instantiate(ball, spawnPosition);
        Debug.Log("Ball spawned");
        Debug.Log("------------");
    }

    private Transform SetSpawnPosition()
    {
        Transform spawnPosition = transform;
        spawnVector = spawnPosition.localPosition;
        Debug.Log("------------");
        Debug.Log(spawnVector);
        
        agent.SetPostPositions();
        LeftMostSpawnArea = agent.startWidth;
        RightMostSpawnArea = agent.endWidth;
        float goalWidth = LeftMostSpawnArea - RightMostSpawnArea;
        
        Debug.Log("LeftMostSpawnArea = " + LeftMostSpawnArea);
        Debug.Log("RightMostSpawnArea = " + RightMostSpawnArea);
        Debug.Log("agent.moveAlong = " + agent.moveAlong);
        
        random = Random.Range(0, goalWidth);
        switch (agent.moveAlong)
        {
            default:
                spawnVector.x = random;
                break;
            case Axis.Y:
                spawnVector.y = random;
                break;
            case Axis.Z:
                spawnVector.z = random;
                break;
        }
        Debug.Log(spawnVector);
        spawnPosition.localPosition = spawnVector;
        return spawnPosition;
        // transform.localPosition = spawnpoint;
    }
    
    private void OnEnable()
    {
        //Balls = transform.Find("Balls").gameObject;
    }
}
