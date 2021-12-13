using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public string SpawnAxis = "z";
    public float xPosition = 0;
    public float yPosition = 10;
    public float zPosition = 0;
    private Vector3 Spawnpoint;
    private float LeftMostSpawnArea = 0f;
    private float RightMostSpawnArea = 10f;
    public GameObject Ball;
    private bool isAlive = false;
    
    private GameObject Balls;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        SetSpawnPosition();
        if (!isAlive)
        {
            GameObject go = Instantiate(Ball, transform);
            go.transform.SetParent(Balls.transform);
            isAlive = true;
        }
    }

    private void SetSpawnPosition()
    {
        float random = Random.Range(LeftMostSpawnArea, RightMostSpawnArea);
        Spawnpoint.x = xPosition;
        Spawnpoint.y = yPosition;
        Spawnpoint.z = zPosition;
        switch (SpawnAxis)
        {
            case "x":
                Spawnpoint.x = random;
                break;
            case "y":
                Spawnpoint.y = random;
                break;
            default:
                Spawnpoint.z = random;
                break;
        }

        transform.localPosition = Spawnpoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearBalls()
    {
        foreach (Transform ball in Balls.transform)
        {
            GameObject.Destroy(ball.gameObject);
        }

    }

    public void DestroyBall()
    {
        ClearBalls();
        isAlive = false;
    }
}
