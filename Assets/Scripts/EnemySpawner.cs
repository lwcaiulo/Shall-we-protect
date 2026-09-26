using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> levelOneEnemies;

    public GameObject[] currentLevelEnemies;

    public GameObject[] enemySpawners;

    public static EnemySpawner Instance;

    public float howLongTillNextSpawn = 2f;

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
