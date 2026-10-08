using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    public bool gameIsPaused;
    public int currentLevelIndex;

    public GameObject pauseScreen;

    //Used so player can't select during intro text
    public bool gameHasStarted;
    public GameObject introTutorialText;
    public GameObject skipVisual;

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
        //Gets current index for keeping track of current level for saving purposes
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void Start()
    {
        //Will pause game if level starts with text to read
        if(introTutorialText != null)
        {
            gameHasStarted = false;
            gameIsPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            gameIsPaused = false;
            gameHasStarted = true;
            Time.timeScale = 1;
        }
        

        if(pauseScreen != null)
        {
            pauseScreen.SetActive(false);
        }

        scoreScript = GetComponent<ScoreScript>();

        //Updates score only on certain screens
        if (SceneManager.GetActiveScene().name == "Win Screen")
        {
            scoreText.text = "Score: " + playerUpgradeScript.currentScore;
            highscoreText.text = "Highscore: " + scoreScript.highScore;
        }
        if(SceneManager.GetActiveScene().name == "Title Screen")
        {
            highscoreText.text = "Highscore: " + scoreScript.highScore;
        }


        if(skipVisual != null)
        {
            skipVisual.SetActive(false);
        }



        

    }

    private void Update()
    {
        //Pauses game
        if(Input.GetKeyDown("escape") && pauseScreen != null && gameHasStarted == true && Time.timeScale != 5)
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

    //Used when progressing from 1 level to the next
    public void NextLevel()
    {
        if(currentLevelIndex != 7)
        {

            playerUpgradeScript.currentLevel = playerUpgradeScript.currentLevel + 1;

            PlayerPrefs.SetInt("Speed", playerUpgradeScript.amountOfSpeedUpgrades);
            PlayerPrefs.SetInt("Size", playerUpgradeScript.amountOfSizeUpgrades);
            PlayerPrefs.SetInt("Level", playerUpgradeScript.currentLevel);
            PlayerPrefs.SetFloat("Score", playerUpgradeScript.currentScore);

            SceneManager.LoadScene(currentLevelIndex + 1);
        }


    }

    //Resets saved info and starts new game
    public void StartNewGame()
    {
        playerUpgradeScript.currentLevel = 1;
        playerUpgradeScript.amountOfSizeUpgrades = 0;
        playerUpgradeScript.amountOfSpeedUpgrades = 0;
        playerUpgradeScript.currentScore = 0;

        //Resets player prefs
        PlayerPrefs.SetInt("Speed", 0);
        PlayerPrefs.SetInt("Size", 0);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetFloat("Score", 0);


        SceneManager.LoadScene(1);
    }


    //Continuing off previous game
    public void ContinueGame()
    {
        SceneManager.LoadScene(playerUpgradeScript.currentLevel);
    }

    //Retry of level
    public void RestartGame()
    {
        SceneManager.LoadScene(playerUpgradeScript.currentLevel);
    }

    //Quits game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Closed");
    }

    //Returns to title screen
    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    //If level has intro text, they'd activate this when wanting to close it and start the game
    public void StartGameFromIntro()
    {
        introTutorialText.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
        gameHasStarted = true;
    }

    //Speeds up game when no more minions are left
    public void SkipLevel()
    {
        skipVisual.SetActive(true);
        Time.timeScale = 5;
    }
}
