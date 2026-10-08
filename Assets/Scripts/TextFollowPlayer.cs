using UnityEngine;

public class TextFollowPlayer : MonoBehaviour
{
    //This is just so the upgrade timer text follows the player on the last level


    private GameObject minionToFollow;
    public Camera gameCamera;
    public float textVecticalOffset;


    void Start()
    {
        //Finds minion
        minionToFollow = GameObject.FindGameObjectWithTag("Minion");
    }


    void Update()
    {
        //Sets position based on where minion is in the perspective of the camera view
        Vector3 minionOnScreen = gameCamera.WorldToScreenPoint(minionToFollow.transform.position);
        this.transform.position = new Vector3(minionOnScreen.x, minionOnScreen.y + textVecticalOffset, 0f);
    }
}
