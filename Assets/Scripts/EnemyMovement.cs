using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public float enemySpeed = 1f;
    public GameObject coreObject;
    private Vector3 corePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finds core on spawn and retrieves its position
        coreObject = GameObject.FindGameObjectWithTag("Core");
        corePosition = new Vector3( coreObject.transform.position.x, 0.5f, coreObject.transform.position.z );
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
            CoreLife.Instance.currentLife = CoreLife.Instance.currentLife - 1;
            UiTracking.Instance.UpdateCorePercent();

            UiTracking.Instance.enemyCount = UiTracking.Instance.enemyCount - 1;
            UiTracking.Instance.UpdateEnemyUI();

            Destroy(this.gameObject);
        }

        //When this enemy collides with minion
        //Put here rather than on the minion because of bugs when multiple minions collide with the same enemy at the same time
        if (collision.gameObject.CompareTag("Minion")){

            //Remove minion from lists and update minion count before destroying it
            MouseControls.Instance.selectableMinions.Remove(collision.transform.GetComponent<MinionMove>());
            MouseControls.Instance.selectedMinions.Remove(collision.transform.GetComponent<MinionMove>());
            UiTracking.Instance.minionCount = UiTracking.Instance.minionCount - 1;
            UiTracking.Instance.UpdateMinionUI();
            Destroy(collision.gameObject);

            //Update enemy count before destroying it
            UiTracking.Instance.enemyCount = UiTracking.Instance.enemyCount - 1;
            UiTracking.Instance.UpdateEnemyUI();
            Destroy(this.gameObject);
        }
    }
}
