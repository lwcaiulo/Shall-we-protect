using UnityEngine;

public class MouseControls : MonoBehaviour
{
    public Vector3 positionOfMouse;
    public GameObject mouseObject;
    public float distance = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Sets ray from camera to the direction of the mouses position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //If Raycast has hit the something (The floor)
            if(Physics.Raycast(ray, out hit, distance))
            {
                //Draw the line and then move the object to that position.
                Debug.DrawLine(ray.origin, hit.point);
                //Only tracking x and z so it doesn't clip into ground.
                //Make sure object ignores raycast so it doesn't fly to me
                mouseObject.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);
            }
        }

    }
}
