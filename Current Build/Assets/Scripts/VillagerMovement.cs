using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMovement : MonoBehaviour {

    public float moveSpeed;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    //private Vector2 villagerRotation;

    private Rigidbody2D myRigidbody;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int walkDirection;

    public Animator anim;
    public Vector2 lastMove;

    public Collider2D walkZone;
    private bool hasWalkZone;

    public bool canMove;

    private DialogueManager theDM;
    private DialogueHolder theDH;

    public Transform target;

    //public GameObject thePlayer;

    public GameObject theVillager;

    //private float rotationFloat;
    //private Quaternion z;

    public int directionInt;

    public bool working;

    //private GameObject target;
    // private Vector3 targetPoint;
    //private Quaternion targetRotation;

    // private PlayerController thePlayer;

    //public Vector3 villagerOrientation;

    // Use this for initialization

    private PlayerController thePlayer;

    private bool dialogueDirection;


    void Start() {

        //dialogueDirection = false;

        thePlayer = FindObjectOfType<PlayerController>();

        theDM = FindObjectOfType<DialogueManager>();
        theDH = FindObjectOfType<DialogueHolder>();

        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        walkCounter = Random.Range(walkTime * 0.75f, walkTime * 1.25f);
        waitCounter = Random.Range(waitTime * 0.75f, walkTime * 1.25f);

        ChooseDirection();

        if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }

        canMove = true;
        isWalking = true;

    }

    // Update is called once per frame
    void Update() {

        //rotationFloat = theVillager.transform.eulerAngles.z;

        directionInt = thePlayer.directionInt;

        /*if (theDM.dialogActive)
        {

            anim.SetFloat("IdolDirectionX", directionInt);
            anim.SetFloat("IdolDirectionY", directionInt);
            anim.SetBool("DialogueActive", true);


        }*/

        anim.SetBool("CanMove", canMove);

       
        if (!theDM.dialogActive)
        {
            canMove = true;
            // animOn = true;
            //anim.Play(playable);
            //theDH.dialogLock = false;
            //anim.SetBool("DialogueActive", false);

            anim.speed = 1;
        }

        if (!canMove)
        {
            isWalking = false;

            if(isWalking == false)
            {
                myRigidbody.velocity = Vector2.zero;
                anim.SetBool("Villager Moving", false);
                return;
            }
           
        }

		if(isWalking == true)
        {
            walkCounter -= Time.deltaTime;

            switch(walkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector2(0, moveSpeed);
                    lastMove = new Vector2(0, moveSpeed);
                    Animations();
                    if(hasWalkZone = true && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = Random.Range(walkTime * 0.25f, waitTime * 1.75f);
                    }
                    break;

                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed, 0);
                    lastMove = new Vector2(moveSpeed, 0);
                    Animations();
                    if (hasWalkZone = true && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = Random.Range(walkTime * 0.25f, waitTime * 1.75f);
                    }
                    break;

                case 2:
                    myRigidbody.velocity = new Vector2(0, -moveSpeed);
                    lastMove = new Vector2(0, -moveSpeed);
                    Animations();
                    if (hasWalkZone = true && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = Random.Range(walkTime * 0.25f, waitTime * 1.75f);
                    }
                    break;

                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    lastMove = new Vector2(-moveSpeed, 0);
                    Animations();
                    if (hasWalkZone = true && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = Random.Range(walkTime * 0.25f, waitTime * 1.75f);
                    }
                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = Random.Range(walkTime * 0.25f, waitTime * 1.75f);
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector2.zero;
            Animations();

            if (waitCounter < 0)
            {
                ChooseDirection();  
            }
        }
    }

    private void Animations()
    {
        
            anim.SetFloat("MoveX", walkDirection);
            anim.SetBool("Villager Moving", isWalking);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
            anim.SetBool("DialogueActive", theDM.dialogActive);


        /*if(isWalking = false)
        {
            "Villager Moving" = false;
        }*/


        /*if (canMove == false)
        {
            isWalking = false;
        }*/
    }

    public void ChooseDirection()
    {
            walkDirection = Random.Range(0, 4);
            isWalking = true;
            walkCounter = Random.Range(walkTime * 0.25f, waitTime * 1.75f);
    }

   
}
/*var myForward = transform.TransformDirection(Vector3.forward);
var playerForward = thePlayer.transform.TransformDirection(Vector3.forward);
var dotProduct = Vector3.Dot(myForward, playerForward);
                    if (dotProduct< -0.8)
                    {*/