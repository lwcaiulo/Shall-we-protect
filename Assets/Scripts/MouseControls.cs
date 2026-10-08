using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class MouseControls : MonoBehaviour
{

    public static MouseControls Instance;

    public SavedPlayerUpgrades playerUpgradeScript;

    //Arrow object and destination when it spawns
    public GameObject locationArrowPrefab;
    public GameObject locationArrow;

    //List for all minions, and one for selected ones to know who is and isn't selected for loops later
    public List<GameObject> minionObjects;
    public List<MinionMove> selectableMinions;
    public List<MinionMove> selectedMinions;
    public Texture[] playerFaces;

    //Selection box
    public RectTransform selectionBox;

    //Arrow Spawn location
    private Vector3 spawnLocation;


    public bool isMouseDragging;
    public bool isMouseButtonDown;

    //Starting point for mouse when dragging
    Vector3 mouseStartingPosition;

    //Adjustable width and height for selectionBox
    private float selectionWidth;
    private float selectionHeight;

    //Used for adjusting Minion range around destination
    public float destinationRange;

    //Used to despawn arrow
    private float arrowTimer;
    private bool doesArrowExist;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //Resets bools
        isMouseButtonDown = false;
        isMouseDragging = false;
        doesArrowExist = false;


        //Puts all game minion game objects into array and then passes the scripts over to the selection list
        GameObject[] minionObjects = GameObject.FindGameObjectsWithTag("Minion");
        for (int i = 0; i < minionObjects.Length; i++)
        {
            MinionMove minionMove = minionObjects[i].transform.GetComponent<MinionMove>();
            selectableMinions.Add(minionMove);

            //Wont run if on last level
            if(selectableMinions[i].isSoloMinion == false)
            {
                //Increases minions size based on upgrades
                minionObjects[i].transform.localScale = Vector3.one * (1 + (playerUpgradeScript.amountOfSizeUpgrades * 0.2f));

                //Randomizes face
                selectableMinions[i].minionRenderer.material.mainTexture = playerFaces[Random.Range(0, playerFaces.Length)];

                //Increases speed based on upgrades
                NavMeshAgent minionAgent = minionObjects[i].GetComponent<NavMeshAgent>();
                minionAgent.speed = 5 + playerUpgradeScript.amountOfSpeedUpgrades;
                minionAgent.acceleration = 4 + playerUpgradeScript.amountOfSpeedUpgrades;
                minionAgent.angularSpeed = 250 + (playerUpgradeScript.amountOfSpeedUpgrades * 25);
            }

        }

        //Sets starting count for minions and updates it to the ui
        UiTracking.Instance.minionCount = selectableMinions.Count;
        UiTracking.Instance.UpdateMinionUI();

        //Sets specific face for solo minion and doesn't give him upgrades for last level
        if(selectableMinions[0].isSoloMinion == true)
        {
            selectedMinions.Add(selectableMinions[0]);
            selectableMinions[0].minionRenderer.material.mainTexture = playerFaces[0];
        }

    }

    void Update()
    {
        //Checks for minion count to fix nav mesh bugs when no minions left
        //Also can't select when game is paused
        if(LevelManager.Instance.gameIsPaused == false && selectableMinions.Count != 0)
        {
            //Checks for mouse button and recieves first mouse position
            if (Input.GetMouseButtonDown(0))
            {
                isMouseButtonDown = true;
                mouseStartingPosition = Input.mousePosition;

                //Deselects all minions after left clicking only if its not the last level solo minion
                for (int i = 0; i < selectableMinions.Count; i++)
                {
                    if (selectableMinions[i].isSoloMinion == false)
                    {
                        selectableMinions[i].MinionDeselected();
                        selectedMinions.Remove(selectableMinions[i]);
                    }
                }
            }

            //Runs drag code when mouse is pressed
            if (isMouseButtonDown == true)
            {
                MouseIsDragging();
            }


            //Left mouse
            if (Input.GetMouseButtonUp(0))
            {
                //Sets ray from camera to the direction of the mouses position.
                //Use this incase player only clicks and doesn't try to select multiple
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (isMouseDragging == false && Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Minion"))
                {
                    selectedMinions.Add(hit.collider.transform.GetComponent<MinionMove>());
                    selectedMinions[0].MinionSelected();
                }

                //Resets button press, selection box, and drag if player releases mouse button
                isMouseDragging = false;
                isMouseButtonDown = false;
                selectionBox.gameObject.SetActive(false);
            }



            //Right mouse
            if (Input.GetMouseButtonDown(1))
            {
                //Sets ray from camera to the direction of the mouses position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        //Draw the line and then move the object to that position.
                        //Only tracking x and z so it doesn't clip into ground.
                        //Make sure object ignores raycast so it doesn't fly to me lol
                        spawnLocation = new Vector3(hit.point.x, 0.25f, hit.point.z);
                        SpawnArrow();
                    }
                }
            }

        }

        //If there are no more minions alive the level will speed up
        if(selectableMinions.Count == 0 && UiTracking.Instance.enemyCount > 0 && CoreLife.Instance.isDestroyed == false)
        {
            LevelManager.Instance.SkipLevel();
        }


        //Deletes arrow after certain amount of time
        if(doesArrowExist == true)
        {
            if(arrowTimer < 2)
            {
                arrowTimer = arrowTimer + Time.deltaTime;
            }
            else
            {
                Destroy(locationArrow);
                doesArrowExist = false;
                arrowTimer = 0;

            }
        }
    }

    //Runs when mouse is being dragged
    private void MouseIsDragging()
    {
        //If the mouse moves from the original position while mouse is down then the player is dragging it
        if (Vector3.Distance(Input.mousePosition, mouseStartingPosition) > 1 && isMouseDragging == false)
        {
            isMouseDragging = true;
            //Makes selection box appear
            selectionBox.gameObject.SetActive(true);
        }

        if (isMouseDragging == true)
        {
            //Logs comparison between when mouse was originally clicked vs where its at as its being dragged
            selectionWidth = Input.mousePosition.x - mouseStartingPosition.x;
            selectionHeight = Input.mousePosition.y - mouseStartingPosition.y;

            //Changes size of selection box based on mouse position while dragging.
            //Mathf.abs makes sure the number can never be negative. That way the rectangle doesn't flip
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(selectionWidth), Mathf.Abs(selectionHeight));

            //Sets the selection box dragged corner to the mouses position if anchored to the bottom left of screen
            selectionBox.anchoredPosition = (mouseStartingPosition + Input.mousePosition) / 2;

            //Won't run on last level, as solo minion doesn't need to be selected
            if (selectableMinions[0].isSoloMinion == false)
            {
                SelectingMinions();
            }


        }
    }

    //Method to find who is and isn't selected
    public void SelectingMinions()
    {

        //Gets all sides of the selection box
        float leftSideOfBox = selectionBox.anchoredPosition.x - (selectionBox.sizeDelta.x / 2);
        float rightSideOfBox = selectionBox.anchoredPosition.x + (selectionBox.sizeDelta.x / 2);
        float topSideOfBox = selectionBox.anchoredPosition.y + (selectionBox.sizeDelta.y / 2);
        float bottomSideOfBox = selectionBox.anchoredPosition.y - (selectionBox.sizeDelta.y / 2);

        for (int i = 0; i < selectableMinions.Count; i++)
        {
            //Gets minions position based on where they are in relation to the camera
            Vector3 minionPosition = Camera.main.WorldToScreenPoint(selectableMinions[i].transform.position);

            //If minion is in between all the sides of the selection box.
            //Aka if they are in the selection box
            if (minionPosition.x > leftSideOfBox && minionPosition.x < rightSideOfBox && minionPosition.y > bottomSideOfBox && minionPosition.y < topSideOfBox)
            {
                //If not already selected yet, select it
                if(!selectedMinions.Contains(selectableMinions[i]))
                {
                    selectedMinions.Add(selectableMinions[i]);
                    selectableMinions[i].MinionSelected();
                }
            }
            else
            {
                //if was selected but no longer in window, deselect it
                if (selectedMinions.Contains(selectableMinions[i]))
                {
                    selectableMinions[i].MinionDeselected();
                    selectedMinions.Remove(selectableMinions[i]);
                }
            }
        }

    }


    //For spawning arrow visual for player and telling minions where to go
    public void SpawnArrow()
    {
        //Spawns location arrow if there is none and a minion is selected
        if (locationArrow == null && selectedMinions.Count > 0)
        {
            locationArrow = Instantiate(locationArrowPrefab, spawnLocation, Quaternion.identity);
            TellMinionsWhereToGo();
            doesArrowExist = true;
        }

        //Destroys location arrow if one exists and no minion is selected 
        else if(selectedMinions.Count == 0)
        {
            Destroy(locationArrow);
            doesArrowExist = false;
            arrowTimer = 0;
        }

        //Changes location of existing location arrow if minion is selected and a arrow already exists
        else
        {
            locationArrow.transform.position = spawnLocation;
            TellMinionsWhereToGo();
            arrowTimer = 0;
        }
    }

    //As the name implies, this'll tell each minion where to go based on where the arrow is
    public void TellMinionsWhereToGo()
    {
        //Records arrow position
         Vector3 arrowPosition = locationArrow.gameObject.transform.position;

        //Sets size of circle around arrow position
        destinationRange = 1 + selectedMinions.Count / 3;
        
        //Won't run if only 1 is selected as the circle location was offset from where player actually wanted to send minion
        if(selectedMinions.Count > 1)
        {
            //Sets destination of each selected minion
            for (int i = 0; i < selectedMinions.Count; i++)
            {
                //Sets minions destinations to be a circle.
                //Will continue to set destinations of each minion in a circle shape around the target position
                selectedMinions[i].moveHere = new Vector3
                    (arrowPosition.x + destinationRange * Mathf.Cos(2 * Mathf.PI * i / selectedMinions.Count),
                    arrowPosition.y,
                    arrowPosition.z + destinationRange * Mathf.Sin(2 * Mathf.PI * i / selectedMinions.Count));

                selectedMinions[i].MinionMoves();
            }

        }
        //For only 1 minion
        else
        {
            selectedMinions[0].moveHere = arrowPosition;
            selectedMinions[0].MinionMoves();
        }
        //Resets selection if none were selected but right mouse was pressed
        if (selectableMinions[0].isSoloMinion == false)
        {
            selectedMinions.Clear();
        }
    }



}


