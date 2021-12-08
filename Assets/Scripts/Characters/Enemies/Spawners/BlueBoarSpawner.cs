using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBoarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawn;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
        if (gameObject.transform.childCount < 1)
        {
            Vector3 spawnPos = transform.position;
            GameObject spawnedEnemy = Instantiate(enemySpawn, spawnPos, Quaternion.identity);
            spawnedEnemy.transform.parent = gameObject.transform;
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnEnemy());
    }
}
