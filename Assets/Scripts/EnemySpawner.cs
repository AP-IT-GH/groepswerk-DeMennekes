using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float SpawnTimeLowerBound = 3.0f;
    public float SpawnTimeHigherBound = 6.0f;
    public GameObject SmallEnemy;
    private float nextActionTime = 0.0f;
    
    private GameObject Enemies;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();

        nextActionTime = GetRandomPeriod();
    }

    private void SpawnEnemy()
    {
        GameObject go = Instantiate(SmallEnemy, transform);
        go.transform.SetParent(Enemies.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (nextActionTime <= Time.time)
        {
            Debug.Log(nextActionTime);
            SpawnEnemy();
            
            nextActionTime = Time.time + GetRandomPeriod();
            
            
        }
    }

    float GetRandomPeriod()
    {
        return Random.Range(SpawnTimeLowerBound, SpawnTimeHigherBound);
    }
    
    private void OnEnable()
    {
        Enemies = transform.Find("Enemies").gameObject;
    }
    
    public void ClearEnemies()
    {
        foreach (Transform enemy in Enemies.transform)
        {
            GameObject.Destroy(enemy.gameObject);
        }

    }
}
