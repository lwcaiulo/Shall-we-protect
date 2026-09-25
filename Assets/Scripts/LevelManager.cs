using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int currentLevel;

    public static LevelManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        ChangeLevel();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel()
    {
        if(currentLevel == 1)
        {
            for (int i = 0; i < EnemySpawner.Instance.levelOneEnemies.Count; i++)
            {
                EnemySpawner.Instance.currentLevelEnemies.Add(EnemySpawner.Instance.levelOneEnemies[i]);
            }
        }
    }
}
