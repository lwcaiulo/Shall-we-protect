using UnityEngine;
using TMPro;
public class CoreLife : MonoBehaviour
{
    public int maximumLife = 5;
    public int currentLife;
    public static CoreLife Instance;

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
        currentLife = maximumLife;
        UiTracking.Instance.UpdateCorePercent();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentLife == 0)
        {
            Debug.Log("GameOver");
        }
    }
}
