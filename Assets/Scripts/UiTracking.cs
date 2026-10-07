using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiTracking : MonoBehaviour
{
    public int minionCount;
    public int enemyCount;
    public float corePercent;

    public SavedPlayerUpgrades playerUpgradeScript;
    private ScoreScript scoreScript;

    public TextMeshProUGUI minionTracker;
    public TextMeshProUGUI enemyTracker;
    public TextMeshProUGUI coreTextBox;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI continueText;
    public Button continueButton;

    public static UiTracking Instance;
    public GameObject upgradePopUp;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        scoreScript = GetComponent<ScoreScript>();
        //Updates continue button text and disallows access if player can't continue
        if (continueButton != null)
        {
            if(playerUpgradeScript.currentLevel <= 1)
            {
                continueButton.interactable = false;
                continueText.text = "Continue";
            }
            else
            {
                continueButton.interactable = true;
                continueText.text = "Level " + playerUpgradeScript.currentLevel;
            }
        }
    }


    public void UpdateEnemyUI()
    {
        enemyTracker.text = "Enemies left: " + enemyCount;
        if (enemyCount < 1 && corePercent != 0)
        {
            //At end of all but last 2 levels
            if(LevelManager.Instance.currentLevelIndex < 6)
            {
                LevelManager.Instance.gameIsPaused = true;
                Time.timeScale = 0;
                scoreScript.UpdateScore();
                scoreText.text = "Current Score: " + playerUpgradeScript.currentScore;
                upgradePopUp.SetActive(true);

            }
            //At the end of the second last level
            else if(LevelManager.Instance.currentLevelIndex == 6)
            {
                scoreScript.UpdateScore();
                LevelManager.Instance.NextLevel();
            }
            //End of last level
            else if(LevelManager.Instance.currentLevelIndex == 7)
            {
                playerUpgradeScript.currentLevel = 0;
                playerUpgradeScript.amountOfSizeUpgrades = 0;
                playerUpgradeScript.amountOfSpeedUpgrades = 0;
                scoreScript.UpdateScore();
                scoreScript.EndOfGameScoreCalc();
                SceneManager.LoadScene("Win Screen");
            }

        }
    }

    public void UpdateMinionUI()
    {
        minionTracker.text = "Minions: " + minionCount;

    }

    public void UpdateCorePercent()
    {
        corePercent = (CoreLife.Instance.currentLife / CoreLife.Instance.maximumLife) * 100;
        coreTextBox.text = "Life Remaining: " + corePercent + "%";
    }

    public void IncreaseSpeed()
    {
        playerUpgradeScript.amountOfSpeedUpgrades += 1;
        LevelManager.Instance.NextLevel();
    }

    public void IncreaseSize()
    {
        playerUpgradeScript.amountOfSizeUpgrades += 1;
        LevelManager.Instance.NextLevel();
    }
}
