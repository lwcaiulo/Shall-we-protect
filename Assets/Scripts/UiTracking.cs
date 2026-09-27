using UnityEngine;
using TMPro;

public class UiTracking : MonoBehaviour
{
    public int minionCount;
    public int enemyCount;
    public float corePercent;

    public TextMeshProUGUI minionTracker;
    public TextMeshProUGUI enemyTracker;
    public TextMeshProUGUI coreTextBox;
    public static UiTracking Instance;

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
            LevelManager.Instance.NextLevel();
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
}
