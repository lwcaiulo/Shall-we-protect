using UnityEngine;
using System.Collections.Generic;

public class MinionMovement : MonoBehaviour
{

    //Currently on Minion for testing. NEeds to be on Script holder and minions put into array
    public MouseControls mouseScript;
    public List<GameObject> movingCreatures;
    public List<bool> minionIsMoving;
    public float walkSpeed = 5f;
    private float speed;
    public bool minionIsWalking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if(minionIsWalking == true)
        {
            speed = walkSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mouseScript.locationArrow.transform.position, speed);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Location Arrow")
        {
            minionIsWalking = false;
            Destroy(other.gameObject);
        }
    }
    
}
