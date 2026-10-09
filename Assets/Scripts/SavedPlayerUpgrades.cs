using UnityEngine;

[CreateAssetMenu(fileName = "SavedPlayerUpgrades", menuName = "Scriptable Objects/SavedPlayerUpgrades")]
public class SavedPlayerUpgrades : ScriptableObject
{
    //Scriptable object to keep track of between scene
    public int amountOfSpeedUpgrades;
    public int amountOfSizeUpgrades;

    public int currentLevel;

    public float currentScore;

    public float currentHighscore;

}
