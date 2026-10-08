using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    //Used for scaling explosion size
    private float currentSize;

    public GameObject playerDeathEffect;
    private EnemyMovement enemyScript;
    public GameObject enemyDeathEffect;


    private void Update()
    {
        //Increases size of explosion in steps
        transform.localScale += Vector3.one * 5f * Time.deltaTime;
        currentSize = transform.localScale.x;

        //Destroys explosion after a certain size
        if(currentSize > 10)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destoys minion if minion enters explosion
        if (other.gameObject.CompareTag("Minion"))
        {
            MinionMove minionScript = other.gameObject.GetComponent<MinionMove>();

            //Won't destroy minion if it is currrently invincible
            if (minionScript.isCurrentlyInvincible == false)
            {
                //Remove minion from lists and update minion count before destroying it
                MouseControls.Instance.selectableMinions.Remove(other.transform.GetComponent<MinionMove>());
                MouseControls.Instance.selectedMinions.Remove(other.transform.GetComponent<MinionMove>());
                UiTracking.Instance.minionCount = UiTracking.Instance.minionCount - 1;
                UiTracking.Instance.UpdateMinionUI();
                Destroy(other.gameObject);

                //Spawns minion death effect
                GameObject playerDeath;
                playerDeath = Instantiate(playerDeathEffect, other.transform.position, Quaternion.identity);
            }
        }
        //Destroys other enemy if they enter explosion
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Update enemy count before destroying it
            UiTracking.Instance.enemyCount = UiTracking.Instance.enemyCount - 1;
            UiTracking.Instance.UpdateEnemyUI();

            //Spawns enemy death effect
            GameObject enemyDeath;
            enemyDeath = Instantiate(enemyDeathEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        //Destroys other exploding enenmy if they enter explosion
        if (other.gameObject.CompareTag("Exploding Enemy")){

            //Spawns new explosion from destroyed enemy
            enemyScript = other.GetComponent<EnemyMovement>();
            enemyScript.SpawnExplosion();

            //Update enemy count before destroying it
            UiTracking.Instance.enemyCount = UiTracking.Instance.enemyCount - 1;
            UiTracking.Instance.UpdateEnemyUI();

            //Spawns enemy death effect
            GameObject enemyDeath;
            enemyDeath = Instantiate(enemyDeathEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

}
