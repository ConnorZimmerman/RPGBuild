using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{

    public string dialogue;
    private DialogueManager dMan;

    public string[] dialogLines;

    public GameObject thePlayer;

    public GameObject theVillager;

    public Transform target;

    public VillagerMovement villagerScript;

    //private VillagerMovement villagerScript;

    public bool dialogLock;

    //public Transform target;

    // Use this for initialization
    void Start()
    {
        //villagerScript = GetComponent<VillagerMovement>();

        dMan = FindObjectOfType<DialogueManager>();

        dialogLock = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Select"))
            {
                if (!dMan.dialogActive)
                {
                        dialogLock = true;
                    if(dialogLock == true)
                    {
                        //villagerScript.anim.speed = 0;

                        //transform.LookAt(transform.position + new Vector3(0, 0, 1), target.transform.position - transform.position);

                         villagerScript.anim.SetFloat("IdolDirectionX", villagerScript.directionInt);
                          villagerScript.anim.SetFloat("IdolDirectionY", villagerScript.directionInt);
                          villagerScript.anim.SetBool("DialogueActive", true);
                        

                        dMan.ShowDialogue();

                        dMan.currentLine = 0;
                        dMan.dialogLines = dialogLines;
                        //transform.LookAt(target);
                    }


                }


                if (transform.parent.GetComponent<VillagerMovement>() != null)
                {
                    transform.parent.GetComponent<VillagerMovement>()
                        .canMove = false;
                }

            }

        }
    }

}