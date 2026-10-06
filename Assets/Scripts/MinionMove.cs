using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;

public class MinionMove : MonoBehaviour
{

    NavMeshAgent minionAgent;
    public MeshRenderer minionRenderer;
    Animator animator;
    public GameObject minionBody;
    public bool minionIsSelected = false;
    public bool minionIsMoving = false;
    public Vector3 moveHere;
    public float waitTillStop = 1f;
    private float timer = 0;
    public bool isCurrentlyInvincible = false;
    public bool isSoloMinion;

    private void Awake()
    {

        if (gameObject.TryGetComponent(out InvincibilityScript soloInvincibilityScript))
        {
            isSoloMinion = true;
        }

        //Grabs navmesh from model
        minionAgent = GetComponent<NavMeshAgent>();
        minionRenderer = minionBody.GetComponent<MeshRenderer>();
        animator = minionBody.GetComponent<Animator>();


        foreach (Renderer limbRenderers in GetComponentsInChildren<Renderer>())
        {
            limbRenderers.material.color = Color.red;
        }

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

            //If the minion is staying still after start of movement it'll stop there
            //This is so it doesn't get stuck trying to get to an unreachable spot
            if (waitTillStop < timer)
            {
                if (minionAgent.velocity.magnitude < 0.01f)
                {
                    minionIsMoving = false;
                    minionAgent.ResetPath();
                    MinionDeselected();
                    timer = 0;
                }
            }
            else 
            {
                timer = timer + Time.deltaTime;
            }
        }
        animator.SetBool("isMoving", minionIsMoving);
    }

    //Declare that Minion is selected. Toggled in Mouse Script
    public void MinionSelected()
    {
        minionIsSelected = true;
        foreach (Renderer limbRenderers in GetComponentsInChildren<Renderer>())
        {
            limbRenderers.material.color = Color.yellow;
        }
    }

    //Declare that Minion isn't selected. Toggled in Mouse Script
    public void MinionDeselected()
    {
        minionIsSelected = false;
        //Will keep moving color when deselected if needed
        if(minionIsMoving == true)
        {
            foreach (Renderer limbRenderers in GetComponentsInChildren<Renderer>())
            {
                limbRenderers.material.color = Color.green;
            }
        }
        else
        {
            foreach (Renderer limbRenderers in GetComponentsInChildren<Renderer>())
            {
                limbRenderers.material.color = Color.red;
            }
        }
    }

    //Sets destination for minion nav agent and resets selection for this and mouse script
    public void MinionMoves()
    {
        timer = 0;
        minionIsMoving = true;

        foreach (Renderer limbRenderers in GetComponentsInChildren<Renderer>())
        {
            limbRenderers.material.color = Color.green;
        }

        minionAgent.SetDestination(moveHere);
        minionIsSelected = false;
    }

}
