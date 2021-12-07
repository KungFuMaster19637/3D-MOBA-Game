using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemySpawn;
    private float spawnRadius = 0.4f;
    //SpawnRadius has to be equal to returnposition in EnemyCombat to make this work

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
        if (gameObject.transform.childCount < 1)
        {
            Vector3 spawnPos = Random.insideUnitSphere.normalized * spawnRadius + transform.position;
            Debug.Log(spawnPos + transform.position);
            GameObject spawnedEnemy = Instantiate(enemySpawn, spawnPos, Quaternion.identity);
            spawnedEnemy.transform.parent = gameObject.transform;
        }
        yield return new WaitForSeconds(8f);
        StartCoroutine(SpawnEnemy());
    }
}
