using UnityEngine;

[CreateAssetMenu(fileName = "SavedPlayerUpgrades", menuName = "Scriptable Objects/SavedPlayerUpgrades")]
public class SavedPlayerUpgrades : ScriptableObject
{
    //Scriptable object to keep track of between scene and session info
    public int amountOfSpeedUpgrades;
    public int amountOfSizeUpgrades;

    public int currentLevel;

    public float currentScore;
    public float highScore;

}
