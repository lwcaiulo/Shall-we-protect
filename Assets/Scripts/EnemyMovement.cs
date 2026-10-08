using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed;

    public GameObject coreObject;
    public GameObject explosionPrefab;
    public GameObject playerDeathEffect;
    public GameObject enemyDeathEffect;
    public GameObject coreDamageEffect;
    private MinionMove minionScript;
    private Vector3 corePosition;


    AudioSource soundEffectSource;
    AudioList audioScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds core on spawn and retrieves its position
        coreObject = GameObject.FindGameObjectWithTag("Core");
        corePosition = new Vector3(coreObject.transform.position.x, 0.5f, coreObject.transform.position.z);

        //Changes enemy speed for first 2 levels
        if (LevelManager.Instance.currentLevelIndex == 1 
            || LevelManager.Instance.currentLevelIndex == 2
            || this.gameObject.CompareTag("Exploding Enemy"))
        {
            enemySpeed = 2;
        }
        else
        {
            enemySpeed = 3;
        }

        //Orients enemies towards the core
        gameObject.transform.LookAt(corePosition);

        //Retrieve audio script and source
        GameObject soundHolder = GameObject.FindWithTag("Sound Effects");
        audioScript = soundHolder.GetComponent<AudioList>();
        soundEffectSource = soundHolder.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moves towards the core
        float movementSpeed = enemySpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, corePosition, movementSpeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //When the enemy collides with the core it'll die and take one life from the core
        if (collision.gameObject == coreObject) 
        {
            //Take an extra life if exploding enemy and spawns explosion
            if (this.gameObject.CompareTag("Exploding Enemy"))
            {
                SpawnExplosion();
                CoreLife.Instance.currentLife = CoreLife.Instance.currentLife - 1;
            }

            CoreLife.Instance.currentLife = CoreLife.Instance.currentLife - 1;
            UiTracking.Instance.UpdateCorePercent();

            UiTracking.Instance.enemyCount = UiTracking.Instance.enemyCount - 1;
            UiTracking.Instance.UpdateEnemyUI();

            //Spawn enemy death partical effect
            GameObject enemyDeath;
            enemyDeath = Instantiate(enemyDeathEffect, this.transform.position, Quaternion.identity);

            //Spawn core partical effect
            GameObject coreDamage;
            coreDamage = Instantiate(coreDamageEffect, new Vector3(collision.transform.position.x, collision.transform.position.y + 0.5f, collision.transform.position.z + 0.4f), Quaternion.identity);

            //Plays audio when enemy collides with core
            if(CoreLife.Instance.currentLife > 0)
            {
                soundEffectSource.PlayOneShot(audioScript.coreDamageSound, 0.1f);
                soundEffectSource.PlayOneShot(audioScript.enemyDeathSound, 0.1f);
            }

            Destroy(this.gameObject);
        }

        //When this enemy collides with minion
        //Put here rather than on the minion because of bugs when multiple minions collide with the same enemy at the same time
        if (collision.gameObject.CompareTag("Minion")){

            //Spawns explosion
            if(this.gameObject.CompareTag("Exploding Enemy"))
            {
                SpawnExplosion();
            }

            MinionMove minionScript = collision.gameObject.GetComponent<MinionMove>(); 

            //Won't destroy minion if it is currrently invincible
            if(minionScript.isCurrentlyInvincible == false){
                //Remove minion from lists and update minion count before destroying it
                MouseControls.Instance.selectableMinions.Remove(collision.transform.GetComponent<MinionMove>());
                MouseControls.Instance.selectedMinions.Remove(collision.transform.GetComponent<MinionMove>());
                UiTracking.Instance.minionCount = UiTracking.Instance.minionCount - 1;
                UiTracking.Instance.UpdateMinionUI();

                soundEffectSource.PlayOneShot(audioScript.minionDeathSound, 0.1f);

                Destroy(collision.gameObject);
                //Spawns player partical effect
                GameObject playerDeath;
                playerDeath = Instantiate(playerDeathEffect, collision.transform.position, Quaternion.identity);
            }
            //Update enemy count before destroying it
            UiTracking.Instance.enemyCount = UiTracking.Instance.enemyCount - 1;
            UiTracking.Instance.UpdateEnemyUI();

            //Spawns enemy partical effect
            GameObject enemyDeath;
            enemyDeath = Instantiate(enemyDeathEffect, this.transform.position, Quaternion.identity);

            soundEffectSource.PlayOneShot(audioScript.enemyDeathSound, 0.1f);

            Destroy(this.gameObject);
        }
    }
    //Spawns explosion and plays sound
    public void SpawnExplosion() {
        GameObject explosion;
        soundEffectSource.PlayOneShot(audioScript.explosionSound, 0.5f);
        explosion = Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
    }
}
