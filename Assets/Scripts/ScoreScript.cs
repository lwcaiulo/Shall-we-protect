using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public float perfectGameBonus = 100;
    public SavedPlayerUpgrades playerUpgradesScript;




    public void UpdateScore()
    {
        //Increases score based on core percent, minion count, and a bonus if the player had a perfect game
        playerUpgradesScript.currentScore = playerUpgradesScript.currentScore + UiTracking.Instance.corePercent + (MouseControls.Instance.selectableMinions.Count * 10);
        if(UiTracking.Instance.corePercent == 100)
        {
            playerUpgradesScript.currentScore = playerUpgradesScript.currentScore + perfectGameBonus;
        }
    }

    //Updates the highscore
    public void EndOfGameScoreCalc()
    {
        if (playerUpgradesScript.currentScore > playerUpgradesScript.highScore)
        {
            playerUpgradesScript.highScore = playerUpgradesScript.currentScore;
        }
    }
}
