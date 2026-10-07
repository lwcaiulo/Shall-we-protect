using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    public bool gameIsPaused;
    public int currentLevelIndex;

    public GameObject pauseScreen;

    public SavedPlayerUpgrades playerUpgradeScript;
    public static LevelManager Instance;
    private ScoreScript scoreScript;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

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
        if(pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }
        scoreScript = GetComponent<ScoreScript>();


        if (SceneManager.GetActiveScene().name == "Win Screen")
        {
            Debug.Log("Winnn");
            scoreText.text = "Score: " + playerUpgradeScript.currentScore;
            highscoreText.text = "Highscore: " + playerUpgradeScript.highScore;
        }
        if(SceneManager.GetActiveScene().name == "Title Screen")
        {
            highscoreText.text = "Highscore: " + playerUpgradeScript.highScore;
        }
    }

    private void Update()
    {
        //Pauses game
        if(Input.GetKeyDown("escape") && pauseScreen != null)
        {
            if (gameIsPaused == false)
            {
                gameIsPaused = true;
                Time.timeScale = 0;
                pauseScreen.SetActive(true);
            }
            else
            {
                gameIsPaused = false;
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
        }
    }

    public void NextLevel()
    {
        if(currentLevelIndex != 7)
        {

            playerUpgradeScript.currentLevel = playerUpgradeScript.currentLevel + 1;
            SceneManager.LoadScene(currentLevelIndex + 1);
        }


    }

    public void StartNewGame()
    {
        playerUpgradeScript.currentLevel = 1;
        playerUpgradeScript.amountOfSizeUpgrades = 0;
        playerUpgradeScript.amountOfSpeedUpgrades = 0;
        playerUpgradeScript.currentScore = 0;
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
