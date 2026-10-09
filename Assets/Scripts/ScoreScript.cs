using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    //Changeable score bonus
    public float perfectGameBonus = 100;

    public SavedPlayerUpgrades playerUpgradesScript;



    //Called upon to update score
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
        if (playerUpgradesScript.currentScore > playerUpgradesScript.currentHighscore)
        {
            playerUpgradesScript.currentHighscore = playerUpgradesScript.currentScore;
            PlayerPrefs.SetFloat("Highscore", playerUpgradesScript.currentHighscore);
        }
    }
}
