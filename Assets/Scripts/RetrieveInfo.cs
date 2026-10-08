using UnityEngine;

public class RetrieveInfo : MonoBehaviour
{
    //This is used to retrive data from player prefs in title screen
    public SavedPlayerUpgrades playerUpgradeScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Retrieves saved info and sets to scriptable object
        playerUpgradeScript.amountOfSpeedUpgrades = PlayerPrefs.GetInt("Speed", 0);
        playerUpgradeScript.amountOfSizeUpgrades = PlayerPrefs.GetInt("Size", 0);
        playerUpgradeScript.currentLevel = PlayerPrefs.GetInt("Level", 0);
        playerUpgradeScript.currentScore = PlayerPrefs.GetFloat("Score", 0);
    }


}
