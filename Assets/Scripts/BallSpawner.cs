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
    private bool isActive = false;
    
    public void SpawnBall()
    {
        Vector3 spawnPosition = GetLocalSpawnPosition();
        DebugInfo("Reached spawnball");
        DebugInfo(spawnPosition);
        if (!isActive)
        {
            GameObject go = Instantiate(ball, spawnPosition, transform.localRotation);
            go.transform.SetParent(this.transform);

            isActive = true;
        }
        //GiveForce(go);
    }

    public void GiveForce(GameObject go)
    {
        Vector3 forceToAdd;
        switch (agent.movingAxis)
        {
            case Axis.Z:
                forceToAdd = new Vector3(force, 0f, 0f);
                break;
            default:
                forceToAdd = new Vector3(0f, 0f, force);
                break;
        }
        
        go.gameObject.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.VelocityChange);
    }

    private Vector3 GetLocalSpawnPosition()
    {
        Vector3 localPosition = transform.position;
        
        spawnVector = localPosition;
        
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

        isActive = false;
    }
    private void OnEnable()
    {
        //Balls = transform.Find("Balls").gameObject;
    }

    private void DebugInfo(object toLog)
    {
        Debug.Log(toLog.ToString());
    }
}
