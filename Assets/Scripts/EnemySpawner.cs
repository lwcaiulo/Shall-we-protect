using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    //Arrays to put level enemies and spawners
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


    void Start()
    {
        //Update enemy count ui
        UiTracking.Instance.enemyCount = currentLevelEnemies.Length;
        UiTracking.Instance.UpdateEnemyUI();

        //Changes spawn rate based on level
        if (LevelManager.Instance.currentLevelIndex == 1 || LevelManager.Instance.currentLevelIndex == 7)
        {
            howLongTillNextSpawn = 3f;
        }
        else if(LevelManager.Instance.currentLevelIndex == 2)
        {
            howLongTillNextSpawn = 2f;
        }
        else if(LevelManager.Instance.currentLevelIndex == 4)
        {
            howLongTillNextSpawn = 6f;
        }
        else
        {
            howLongTillNextSpawn = 1f;
        }


        StartCoroutine(SpawningEnemies());
    }

    //Coroutine to spawn an enemy at spawn location every x seconds
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
