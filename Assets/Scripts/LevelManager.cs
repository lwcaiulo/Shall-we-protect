using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    public bool gameIsPaused;
    public int currentLevelIndex;

    public SavedPlayerUpgrades playerUpgradeScript;
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
            playerUpgradeScript.currentLevel = playerUpgradeScript.currentLevel + 1;
            SceneManager.LoadScene(currentLevelIndex + 1);
        }


    }

    public void StartNewGame()
    {
        playerUpgradeScript.currentLevel = 1;
        playerUpgradeScript.amountOfSizeUpgrades = 0;
        playerUpgradeScript.amountOfSpeedUpgrades = 0;
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(playerUpgradeScript.currentLevel);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(playerUpgradeScript.currentLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Closed");
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
    }
}
