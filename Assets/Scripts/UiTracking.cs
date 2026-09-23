using UnityEngine;
using TMPro;

public class UiTracking : MonoBehaviour
{
    public int minionCount;
    public int enemyCount;

    public TextMeshProUGUI minionTracker;
    public TextMeshProUGUI enemyTracker;
    public static UiTracking Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minionTracker.text = "Minions: " + minionCount;
        enemyTracker.text = "Enemies left: " + enemyCount;
    }
}
