using UnityEngine;

public class RetrieveInfo : MonoBehaviour
{
    //This is used to retrive data from player prefs in title screen
    public SavedPlayerUpgrades playerUpgradeScript;
    public ScoreScript scoreScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Retrieves saved info and sets to scriptable object
        playerUpgradeScript.amountOfSpeedUpgrades = PlayerPrefs.GetInt("Speed", 0);
        playerUpgradeScript.amountOfSizeUpgrades = PlayerPrefs.GetInt("Size", 0);
        playerUpgradeScript.currentLevel = PlayerPrefs.GetInt("Level", 1);
        playerUpgradeScript.currentScore = PlayerPrefs.GetFloat("Score", 0);
        playerUpgradeScript.currentHighscore = PlayerPrefs.GetFloat("Highscore", 0);

        UiTracking.Instance.ContinueButton();

}


}
