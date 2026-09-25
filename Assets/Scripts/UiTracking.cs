using UnityEngine;
using TMPro;

public class UiTracking : MonoBehaviour
{
    public int minionCount;
    public int enemyCount;

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
    }

    public void UpdateMinionUI()
    {
        minionTracker.text = "Minions: " + minionCount;
    }

    public void UpdateCorePercent()
    {
        coreTextBox.text = "Life Remaining: " + CoreLife.Instance.currentLife;
    }
}
