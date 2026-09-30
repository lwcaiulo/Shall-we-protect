using UnityEngine;

public class TextFollowPlayer : MonoBehaviour
{

    private GameObject minionToFollow;
    public Camera gameCamera;
    public float textVecticalOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minionToFollow = GameObject.FindGameObjectWithTag("Minion");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 minionOnScreen = gameCamera.WorldToScreenPoint(minionToFollow.transform.position);
        this.transform.position = new Vector3(minionOnScreen.x, minionOnScreen.y + textVecticalOffset, 0f);
    }
}
