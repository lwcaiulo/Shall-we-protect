using UnityEngine;
using System.Collections.Generic;

public class MouseControls : MonoBehaviour
{

    public MinionMovement movementScript;
    public GameObject mouseObject;
    public GameObject locationArrowPrefab;
    //public List<GameObject> locationArrows;
    public GameObject locationArrow;
    //public float distance = 50f;
    private Vector3 spawnLocation;
    public Vector3 walkHereLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
     void OnMouseDown()
    {
        //Sets ray from camera to the direction of the mouses position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
     
        //If Raycast has hit the something (The floor)
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {

                    Debug.Log("player");
            }
            else if (hit.collider.CompareTag("Ground"))
            {
                //Draw the line and then move the object to that position.
                //Only tracking x and z so it doesn't clip into ground.
                //Make sure object ignores raycast so it doesn't fly to me
                spawnLocation = new Vector3(hit.point.x, 0.25f, hit.point.z);
                SpawnArrow();
                Debug.Log("Floor");
            }




        }
        
    }

    public void SpawnArrow()
    {

        //Checks to see if a location arrow already exists.
        //If not we spawn it in, otherwise move to prexisting one
        if(locationArrow == null)
        {
            locationArrow = Instantiate(locationArrowPrefab, spawnLocation, Quaternion.identity);
            movementScript.minionIsWalking = true;
        }
        else
        {
            locationArrow.transform.position = spawnLocation;
        }


    }
}


