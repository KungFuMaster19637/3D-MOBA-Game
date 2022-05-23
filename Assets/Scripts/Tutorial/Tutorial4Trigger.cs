using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial4Trigger : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] enemySpawnLocations;

    private bool spawnStarted;
    private List<GameObject> listOfEnemies = new List<GameObject>();
    private void Start()
    {
        spawnStarted = false;
    }
    private void Update()
    {
        if (spawnStarted)
        {
            CheckEnemiesAlive();
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemy = Instantiate(enemies[i], enemySpawnLocations[i]);
            listOfEnemies.Add(enemy);
        }
        spawnStarted = true;
    }

    private void CheckEnemiesAlive()
    {
        for (int i = 0; i < listOfEnemies.Count; i++)
        {
            if (listOfEnemies[i].GetComponent<EnemyStats>().isDead == false)
            {
                return;
            }
        }
        TutorialManager.Instance.Tutorial4();
    }

    [ContextMenu("Skip Tutorial")]
    public void SkipTutorial4()
    {
        TutorialManager.Instance.Tutorial4();
    }
}
