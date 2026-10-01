using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    public bool gameIsPaused;
    public int currentLevelIndex;

    public static LevelManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void Start()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
    }

    public void NextLevel()
    {
        if(currentLevelIndex != 7){
            SceneManager.LoadScene(currentLevelIndex + 1);
        }

    }
}
