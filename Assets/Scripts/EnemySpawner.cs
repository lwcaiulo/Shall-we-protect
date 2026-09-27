using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] currentLevelEnemies;

    public GameObject[] enemySpawners;

    public static EnemySpawner Instance;

    public float howLongTillNextSpawn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UiTracking.Instance.enemyCount = currentLevelEnemies.Length;
        UiTracking.Instance.UpdateEnemyUI();

        //Changes spawn rate based on level
        if (LevelManager.Instance.currentLevelIndex == 1)
        {
            howLongTillNextSpawn = 3f;
        }
        else if(LevelManager.Instance.currentLevelIndex == 2)
        {
            howLongTillNextSpawn = 2f;
        }
        else
        {
            howLongTillNextSpawn = 1f;
        }


        StartCoroutine(SpawningEnemies());
    }


    IEnumerator SpawningEnemies()
    {
        for (int i = 0; i < currentLevelEnemies.Length; i++)
        {
            yield return new WaitForSeconds(howLongTillNextSpawn);
            GameObject enemy;
            enemy = Instantiate(currentLevelEnemies[i], enemySpawners[Random.Range(0, enemySpawners.Length)].transform.position, Quaternion.identity);
        }
    }
}
