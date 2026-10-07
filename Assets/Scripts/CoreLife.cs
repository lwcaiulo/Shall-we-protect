using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CoreLife : MonoBehaviour
{
    //made a float to get percentage working in ui script
    public float maximumLife = 5;
    public float currentLife;
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
        if(currentLife <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
