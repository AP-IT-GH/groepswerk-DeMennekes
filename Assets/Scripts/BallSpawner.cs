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
    private float leftMostSpawnArea, rightMostSpawnArea;
    private float random;
    private Rigidbody rb;
    private float force = 15f;
    
    public void SpawnBall()
    {
        Vector3 spawnPosition = GetLocalSpawnPosition();
        GameObject go = Instantiate(ball, spawnPosition, transform.localRotation);
        
        go.transform.SetParent(this.transform);
        GiveForce(go);
    }

    private void GiveForce(GameObject go)
    {
        switch (agent.movingAxis)
        {
            case Axis.Z:
                go.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(force, 0f, 0f), ForceMode.VelocityChange);
                break;
            default:
                go.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 0f, force), ForceMode.VelocityChange);
                break;
        }
    }

    private Vector3 GetLocalSpawnPosition()
    {
        var localPosition = transform.localPosition;
        
        spawnVector = new Vector3(localPosition.x, y: localPosition.y, localPosition.z);
        
        Debug.Log(spawnVector);
        
        agent.SetPostPositions();
        leftMostSpawnArea = agent.leftPosition;
        rightMostSpawnArea = agent.rightPosition;

        random = Random.Range(leftMostSpawnArea, rightMostSpawnArea);
        switch (agent.movingAxis)
        {
            case Axis.Z:
                spawnVector.z = random;
                break;
            default:
                spawnVector.x = random;
                break;
        }
        return spawnVector;
    }

    public void ClearEnemies()
    {
        foreach (Transform ball in this.transform)
        {
            GameObject.Destroy(ball.gameObject);
        }

    }
    private void OnEnable()
    {
        //Balls = transform.Find("Balls").gameObject;
    }
}
