using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Axis SpawnAxis = Axis.X;
    public GameObject Ball;
    
    private bool isAlive = false;
    private Vector3 Spawnpoint;
    private float LeftMostSpawnArea = 0f;
    private float RightMostSpawnArea = 10f;
    private GameObject Balls;
    private float random;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Spawnpoint = transform.localPosition;
        random = Random.Range(LeftMostSpawnArea, RightMostSpawnArea);
        switch (SpawnAxis)
        {
            case Axis.Y:
                Spawnpoint.y = random;
                break;
            case Axis.Z:
                Spawnpoint.y = random;
                break;
            default:
                Spawnpoint.x = random;
                break;
        }

        transform.localPosition = Spawnpoint;
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
    
    private void OnEnable()
    {
        Balls = transform.Find("Balls").gameObject;
    }
}
