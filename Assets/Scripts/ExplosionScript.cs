using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private float currentSize;
    public GameObject explosionPrefab;
    private EnemyMovement enemyScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Update()
    {
        transform.localScale += Vector3.one * 5f * Time.deltaTime;
        currentSize = transform.localScale.x;
        if(currentSize > 10)
        {
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Minion"))
        {
            //Remove minion from lists and update minion count before destroying it
            MouseControls.Instance.selectableMinions.Remove(other.transform.GetComponent<MinionMove>());
            MouseControls.Instance.selectedMinions.Remove(other.transform.GetComponent<MinionMove>());
            UiTracking.Instance.minionCount = UiTracking.Instance.minionCount - 1;
            UiTracking.Instance.UpdateMinionUI();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Update enemy count before destroying it
            UiTracking.Instance.enemyCount = UiTracking.Instance.enemyCount - 1;
            UiTracking.Instance.UpdateEnemyUI();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Exploding Enemy")){
            enemyScript = other.GetComponent<EnemyMovement>();
            enemyScript.SpawnExplosion();
            //Update enemy count before destroying it
            UiTracking.Instance.enemyCount = UiTracking.Instance.enemyCount - 1;
            UiTracking.Instance.UpdateEnemyUI();
            Destroy(other.gameObject);
        }
    }

}
