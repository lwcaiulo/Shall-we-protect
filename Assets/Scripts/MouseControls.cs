using UnityEngine;
using System.Collections.Generic;

public class MouseControls : MonoBehaviour
{
    public MinionMove movementScript;
    public GameObject locationArrowPrefab;
    public GameObject locationArrow;

    private Vector3 spawnLocation;
    public Vector3 walkHereLocation;


    // Update is called once per frame
    void Update()
    {
        //When Left Mouse is pressed
        if (Input.GetMouseButtonDown(0))
        {
            //Destroys location arrow if it exists
            if(locationArrow != null)
            {
                Destroy(locationArrow);
            }

            //Sets ray from camera to the direction of the mouses position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //If Raycast has hit the something (The floor)
            if (Physics.Raycast(ray, out hit))
            {
                //draws ray. (Start point, direction, color, duration)
                Debug.DrawRay(ray.origin, ray.direction, Color.yellow, 1f);

                if (hit.collider.CompareTag("Minion"))
                {
                    Debug.Log("Minion");
                    //If the previous selection isn't the new selection
                    if(movementScript != hit.collider.GetComponent<MinionMove>())
                    {
                        //Will deselect previous minion if there was one
                        if(movementScript != null)
                        {
                            movementScript.MinionDeselected();
                            movementScript = null;
                        }
                        //Sets new selection
                        movementScript = hit.collider.GetComponent<MinionMove>();
                        movementScript.MinionSelected();
                    }
                }
                //Deselects previous minion if the player clicks anywhere but a minion
                else if (movementScript != null)
                {
                    movementScript.MinionDeselected();
                    movementScript = null;
                }
            }
        }

        //Right mouse
        if (Input.GetMouseButtonDown(1))
        {
            //Sets ray from camera to the direction of the mouses position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //draws ray. (Start point, direction, color, duration)
                Debug.DrawRay(ray.origin, ray.direction, Color.green, 5f);

                if (hit.collider.CompareTag("Ground"))
                {
                    //Draw the line and then move the object to that position.
                    //Only tracking x and z so it doesn't clip into ground.
                    //Make sure object ignores raycast so it doesn't fly to me
                    spawnLocation = new Vector3(hit.point.x, 0.25f, hit.point.z);
                    SpawnArrow();
                    Debug.Log("Ground");
                }
            }
        }
    }



    public void SpawnArrow()
    {
        //Spawns location arrow if there is none and a minion is selected
        if (locationArrow == null && movementScript != null)
        {
            locationArrow = Instantiate(locationArrowPrefab, spawnLocation, Quaternion.identity);
            movementScript.SetMoveTo();
        }
        //Destroys location arrow if one exists and no minion is selected 
        else if(movementScript == null)
        {
            Destroy(locationArrow);
        }
        //Changes location of existing location arrow if minion is selected and a arrow already exists
        else
        {
            locationArrow.transform.position = spawnLocation;
        }


    }
}


