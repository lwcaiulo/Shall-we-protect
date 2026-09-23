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
        EnemySpawner.Instance.allEnemies.Add(this.gameObject);
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
            EnemySpawner.Instance.allEnemies.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
