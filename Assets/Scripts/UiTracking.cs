using UnityEngine;
using TMPro;

public class UiTracking : MonoBehaviour
{
    public int minionCount;
    public int enemyCount;
    public float corePercent;

    public SavedPlayerUpgrades playerUpgradeScript;

    public TextMeshProUGUI minionTracker;
    public TextMeshProUGUI enemyTracker;
    public TextMeshProUGUI coreTextBox;
    public static UiTracking Instance;
    public GameObject upgradePopUp;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    public void UpdateEnemyUI()
    {
        enemyTracker.text = "Enemies left: " + enemyCount;
        if (enemyCount < 1 && corePercent != 0)
        {
            if(LevelManager.Instance.currentLevelIndex < 6)
            {
                LevelManager.Instance.gameIsPaused = true;
                Time.timeScale = 0;
                upgradePopUp.SetActive(true);
            }
            else if(LevelManager.Instance.currentLevelIndex == 6)
            {
                LevelManager.Instance.NextLevel();
            }
            else if(LevelManager.Instance.currentLevelIndex == 7)
            {
                Debug.Log("you win");
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
