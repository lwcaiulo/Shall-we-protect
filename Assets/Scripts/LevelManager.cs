using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{


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
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(currentLevelIndex + 1);
    }
}
