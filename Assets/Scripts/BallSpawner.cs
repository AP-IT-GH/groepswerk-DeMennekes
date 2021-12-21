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
        
        Vector3 spawnPosition = GetLocalSpawnPosition();
        Debug.Log($"local position: {spawnPosition}");
        GameObject go = Instantiate(ball, spawnPosition, transform.localRotation);
        //GameObject go = Instantiate(ball, transform);
        go.transform.SetParent(this.transform);
        
        Debug.Log("------------");
    }

    private Vector3 GetLocalSpawnPosition()
    {
        var localPosition = transform.localPosition;
        Debug.Log($"current local pos: {localPosition}");
        spawnVector = new Vector3(localPosition.x, y: localPosition.y, localPosition.z);
        
        Debug.Log(spawnVector);
        
        agent.SetPostPositions();
        LeftMostSpawnArea = agent.startWidth;
        RightMostSpawnArea = agent.endWidth;
        
        Debug.Log("LeftMostSpawnArea = " + LeftMostSpawnArea);
        Debug.Log("RightMostSpawnArea = " + RightMostSpawnArea);
        Debug.Log("agent.moveAlong = " + agent.moveAlong);

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
        return spawnVector;
    }
    
    private void OnEnable()
    {
        //Balls = transform.Find("Balls").gameObject;
    }
}
