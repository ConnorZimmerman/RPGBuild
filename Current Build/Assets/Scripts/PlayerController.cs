using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    private float currentMoveSpeed;

    private Animator anim;

    private Rigidbody2D myRigidbody;

    private bool playerMoving;
    public Vector2 lastMove;
    private Vector2 moveInput;

    private static bool playerExists;

    public static bool attacking;

    public float attackTime;
    private float attackTimeCounter;

    public string startPoint;

    private bool attackLock;

    private float axisHorizontal;

    private float axisVertical;

    private float axisInput;

    public GameObject swingBig;

    public bool canMove;

    private DialogueManager theDM;

    private bool directionRight;
    private bool directionLeft;

    private bool directionUp;
    private bool directionDown;

    public int directionInt;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private SFXManager sfxMan;

    // Use this for initialization
    void Start()
    {
        sfxMan = FindObjectOfType<SFXManager>();

        theDM = FindObjectOfType<DialogueManager>();

        attackLock = false;

        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        canMove = true;

        lastMove = new Vector2(0, -1f);


        /*if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }*/

        if (boundBox == null)
        {
            boundBox = FindObjectOfType<BoundsScript>().GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(hasWalkZone == true)
        {
            myRigidbody.velocity = Vector2.zero;
            canMove = false;
        }*/

        if (directionUp == true)
        {
            directionInt = 0;
        }
        if (directionRight == true)
        {
            directionInt = 1;
        }
        if (directionDown == true)
        {
            directionInt = 2;
        }
        if (directionLeft == true)
        {
            directionInt = 3;
        }

        playerMoving = false;

        if (!theDM.dialogActive)
        {
            canMove = true;
            anim.speed = 1;
        }

        if (theDM.dialogActive)
        {
            anim.speed = 0;
        }

        if (!canMove)
        {
            myRigidbody.velocity = Vector2.zero;
            anim.SetBool("PlayerMoving", false);
            return;
        }

        axisInput = Input.GetAxisRaw("Attack");

        axisHorizontal = Input.GetAxisRaw("Horizontal");
        axisVertical = Input.GetAxisRaw("Vertical");
     
        if (axisHorizontal > 0.2f)
        {
            axisHorizontal = 0.5f;
        }

        if (axisHorizontal < -0.2f)
        {
            axisHorizontal = -0.5f;
        }

        if (axisVertical > 0.2f)
        {
            axisVertical = 0.5f;
        }

        if (axisVertical < -0.2f)
        {
            axisVertical = -0.5f;
        }
        

        if (!attacking)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 
                Input.GetAxisRaw("Vertical")).normalized;

            if(axisHorizontal > 0.2f || axisHorizontal < -0.2f
                || axisVertical > 0.2f || axisVertical < -0.2f && moveInput != Vector2.zero)
            {
                myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, 
                    moveInput.y * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(axisHorizontal, axisVertical);
            }
            else
            {
                myRigidbody.velocity = Vector2.zero;
            }

            if (Input.GetAxisRaw("Horizontal") > 0.2f)
            {
                directionRight = true;
                directionLeft = false;
                directionUp = false;
                directionDown = false;

            }
            else if (Input.GetAxisRaw("Horizontal") < -0.2f)
            {
                directionLeft = true;
                directionRight = false;
                directionUp = false;
                directionDown = false;

            }

            if (Input.GetAxisRaw("Vertical") > 0.2f)
            {
                directionUp = true;
                directionLeft = false;
                directionRight = false;
                directionDown = false;
            }
            else if (Input.GetAxisRaw("Vertical") < -0.2f)
            {
                directionDown = true;
                directionUp = false;
                directionLeft = false;
                directionRight = false;
            }
        }

        if (axisInput <= -0.2f)
        {

            if (attackLock == false)
            {
                sfxMan.playerAttack.Play();
                attackTimeCounter = attackTime;
                attacking = true;
                myRigidbody.velocity = Vector2.zero;
                anim.SetBool("Attack", true);
                attackLock = true;
            }
        }

        if (axisInput >= 0f)
        {
            attackLock = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            sfxMan.playerAttack.Play();
            attackTimeCounter = attackTime;
            attacking = true;
            myRigidbody.velocity = Vector2.zero;
            anim.SetBool("Attack", true);
        }
    
        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if (attackTimeCounter <= 0)
        {
            attacking = false;
            anim.SetBool("Attack", false);
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }

        float clampX = Mathf.Clamp(transform.position.x, minBounds.x,
           maxBounds.x);
        float clampY = Mathf.Clamp(transform.position.y, minBounds.y,
            maxBounds.y);
        transform.position = new Vector3(clampX, clampY, transform.position.z);

        anim.SetFloat("MoveX", axisHorizontal);
        anim.SetFloat("MoveY", axisVertical);
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

    public void SetBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;

        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }
}

