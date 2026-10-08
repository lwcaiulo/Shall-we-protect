using TMPro;
using UnityEngine;
using System.Collections;
public class InvincibilityScript : MonoBehaviour
{
    public TextMeshProUGUI invincibilityTimerMesh;
    public GameObject invincibilityEffect;
    public MinionMove minionScript;
    public GameObject invincibilitySoundSource;

    public float invincibilityLength;
    float invincibilityTimer = 0f;
    public int amountOfInvincibilitiesRunning = 0;

    // Update is called once per frame
    void Update()
    {
        //Used for time text and effect during invincibility
        //As multiple could run at the same time, this makes it so it'll only stop once there are 0 running
        if (amountOfInvincibilitiesRunning > 0)
        {
            minionScript.isCurrentlyInvincible = true;
            invincibilityTimerMesh.gameObject.SetActive(true);
            invincibilityTimer -= Time.deltaTime;
            invincibilityTimerMesh.text = Mathf.Round(invincibilityTimer) + "";
            invincibilityEffect.SetActive(true);
            invincibilitySoundSource.SetActive(true);

        }
        else
        {
            //Turns off effect and stops timer
            minionScript.isCurrentlyInvincible = false;
            invincibilityEffect.SetActive(false);
            invincibilityTimerMesh.gameObject.SetActive(false);
            invincibilitySoundSource.SetActive(false);
        }
    }
    //This is so it can be called apon by the powerup without the coroutine relying on it
    public void StartInvincibility()
    {
        StartCoroutine(MinionInvincibility());
    }

    //Coroutine for invincibility length
    IEnumerator MinionInvincibility()
    {

        //Resets timer and turns on effect
        amountOfInvincibilitiesRunning += 1;
        invincibilityTimer = invincibilityLength;
        Debug.Log("Its started");

        yield return new WaitForSeconds(invincibilityLength);

        //Reduces amount of invincibilities running by 1
        amountOfInvincibilitiesRunning -= 1;
        Debug.Log("its off now");

    }
}
