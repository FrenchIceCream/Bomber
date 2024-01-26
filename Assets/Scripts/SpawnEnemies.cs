using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    public float spawnTime = 5f;
    private float time;
    void Start()
    {
        time = spawnTime;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            time = spawnTime;
        }
    }
}
