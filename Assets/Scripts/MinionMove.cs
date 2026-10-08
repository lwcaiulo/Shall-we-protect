using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;

public class MinionMove : MonoBehaviour
{
    //Resources of individual minion
    NavMeshAgent minionAgent;
    public MeshRenderer minionRenderer;
    Animator animator;
    AudioSource minionWalkSoundSource;
    public GameObject minionBody;


    public bool minionIsSelected = false;
    public bool minionIsMoving = false;

    public Vector3 moveHere;

    public float waitTillStop = 1f;
    private float timer = 0;

    public bool isCurrentlyInvincible = false;
    public bool isSoloMinion;

    //Public colors so it can be easily changed in editor
    public Color movingColor;
    public Color selectedColor;
    public Color waitingColor;


    private void Awake()
    {

        if (gameObject.TryGetComponent(out InvincibilityScript soloInvincibilityScript))
        {
            isSoloMinion = true;
        }

        //Grabs recourses from minion without having to in editor
        minionAgent = GetComponent<NavMeshAgent>();
        minionRenderer = minionBody.GetComponent<MeshRenderer>();
        animator = minionBody.GetComponent<Animator>();
        minionWalkSoundSource = gameObject.GetComponent<AudioSource>();


        //Changes the colour for all child limbs on the model
        //Mainly done this way as model is a child of empty object holding this script
        //Had issues with animation and rotating so had to do it this way,,,,
        foreach (Renderer limbRenderers in GetComponentsInChildren<Renderer>())
        {
            if (limbRenderers.CompareTag("Minion Part"))
            {
                limbRenderers.material.color = waitingColor;
            }

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
                    //Stops walking sound
                    minionWalkSoundSource.Stop();
                    timer = 0;
                }
            }
            else 
            {
                timer = timer + Time.deltaTime;
                
            }

        }
        //For animator to know when minion is moving or not
        animator.SetBool("isMoving", minionIsMoving);
    }

    //Declare that Minion is selected. Toggled in Mouse Script
    public void MinionSelected()
    {
        minionIsSelected = true;
        foreach (Renderer limbRenderers in GetComponentsInChildren<Renderer>())
        {
            if (limbRenderers.CompareTag("Minion Part"))
            {
                limbRenderers.material.color = selectedColor;
            }

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
                if (limbRenderers.CompareTag("Minion Part"))
                {
                    limbRenderers.material.color = movingColor;
                }

            }
        }
        else
        {
            foreach (Renderer limbRenderers in GetComponentsInChildren<Renderer>())
            {
                if (limbRenderers.CompareTag("Minion Part"))
                {
                    limbRenderers.material.color = waitingColor;
                }

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
            if (limbRenderers.CompareTag("Minion Part"))
            {
                limbRenderers.material.color = movingColor;
            }

        }
        //plays walking sound
        minionWalkSoundSource.Play();
        minionAgent.SetDestination(moveHere);
        minionIsSelected = false;
    }

}
