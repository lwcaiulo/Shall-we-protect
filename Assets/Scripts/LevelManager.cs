using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{


    public string currentLevel;

    public static LevelManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentLevel = SceneManager.GetActiveScene().name;
    }


}
