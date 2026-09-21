using UnityEngine;
using UnityEngine.AI;

public class MinionMove : MonoBehaviour
{
    MouseControls mouseScript;
    GameObject scriptHolder;

    NavMeshAgent minionAgent;
    MeshRenderer minionRenderer;

    public bool minionIsSelected = false;
    public bool minionIsMoving = false;

    public Vector3 moveHere;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Grabs mouse script from script holder
        scriptHolder = GameObject.FindWithTag("ScriptHolder");
        mouseScript = scriptHolder.GetComponent<MouseControls>();

        //Grabs navmesh from model
        minionAgent = GetComponent<NavMeshAgent>();
        minionRenderer = GetComponent<MeshRenderer>();


        minionRenderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if(minionIsMoving == true && minionIsSelected == false)
        {
            //Will stop movement and return to idle
            if (minionAgent.destination == null)
            {
                    minionIsMoving = false;
                    minionAgent.ResetPath();
                    MinionDeselected();
            }

        }
    }

    //Declare that Minion is selected. Toggled in Mouse Script
    public void MinionSelected()
    {
        minionIsSelected = true;
        minionRenderer.material.color = Color.yellow;
    }

    //Declare that Minion isn't selected. Toggled in Mouse Script
    public void MinionDeselected()
    {
        minionIsSelected = false;
        //Will keep moving color when deselected if needed
        if(minionIsMoving == true)
        {
            minionRenderer.material.color = Color.green;
        }
        else
        {
            minionRenderer.material.color = Color.red;
        }
    }

    //Sets destination for minion nav agent and resets selection for this and mouse script
    public void MinionMoves()
    {
        minionIsMoving = true;
        minionRenderer.material.color = Color.green;
        minionAgent.SetDestination(moveHere);
        minionIsSelected = false;
    }
}
