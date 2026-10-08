using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class CoreLife : MonoBehaviour
{
    //made a float to get percentage working in ui script
    public float maximumLife = 5;

    public float currentLife;

    //For destroying animation
    private GameObject coreModel;
    private float lifeSize = 5;
    public bool isDestroyed = false;

    //Sounds
    AudioSource soundEffectSource;
    AudioList audioScript;

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

        //Updates core percent ui at the start of the game
        UiTracking.Instance.UpdateCorePercent();

        coreModel = GameObject.FindGameObjectWithTag("Core");
        lifeSize = 5;
        isDestroyed = false;

        //Retrieve audio script and source
        GameObject soundHolder = GameObject.FindWithTag("Sound Effects");
        audioScript = soundHolder.GetComponent<AudioList>();
        soundEffectSource = soundHolder.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Starts destruction animation. Is destroyed bool so it doesn't run every frame.
        if(currentLife <= 0 && isDestroyed == false)
        {
            //This prevents game from being paused by player
            LevelManager.Instance.gameHasStarted = false;

            Time.timeScale = 0;
            isDestroyed=true;

            StartCoroutine(CoreDestroyed());
        }
    }

    //Coroutine for destruction animation
    IEnumerator CoreDestroyed()
    {
        soundEffectSource.Stop();
        soundEffectSource.PlayOneShot(audioScript.coreDestroySound, 0.3f);

        for (int i = 0; i < 4; i++)
        {
            lifeSize -= 1.25f;
            coreModel.transform.localScale = Vector3.one * lifeSize;
            yield return new WaitForSecondsRealtime(0.5f);
        }

        SceneManager.LoadScene("Game Over");

    }



}
