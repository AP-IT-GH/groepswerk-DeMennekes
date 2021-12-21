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
        Debug.Log("------------");
        
        Transform spawnPosition = SetSpawnPosition();
        GameObject go = Instantiate(ball, spawnPosition.localPosition, transform.rotation);
        //GameObject go = Instantiate(ball, transform);
        go.transform.SetParent(this.transform);
        
        Debug.Log("------------");
    }

    private Transform SetSpawnPosition()
    {
        Transform spawnPosition = transform;
        spawnVector = spawnPosition.localPosition;
        
        Debug.Log(spawnVector);
        
        agent.SetPostPositions();
        LeftMostSpawnArea = agent.startWidth;
        RightMostSpawnArea = agent.endWidth;
        
        Debug.Log("LeftMostSpawnArea = " + LeftMostSpawnArea);
        Debug.Log("RightMostSpawnArea = " + RightMostSpawnArea);
        Debug.Log("agent.moveAlong = " + agent.moveAlong);
        
        // if (goalWidth < 0)
        //     goalWidth *= -1;
        
        random = Random.Range(LeftMostSpawnArea, RightMostSpawnArea);
        Debug.Log("random = " + random);
        switch (agent.moveAlong)
        {
            case Axis.Z:
                Debug.Log("Z branch");
                spawnVector.z = random;
                break;
            default:
                Debug.Log("X branch");
                spawnVector.x = random;
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
