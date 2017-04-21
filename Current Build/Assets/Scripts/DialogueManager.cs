﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public GameObject dBox;
    public Text dText;

    public bool dialogActive;

    public string[] dialogLines;
    public int currentLine;

    private PlayerController thePlayer;
    private VillagerMovement villagerScript;


    // Use this for initialization
    void Start() {

        thePlayer = FindObjectOfType<PlayerController>();

        //villagerScript = FindObjectOfType<VillagerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (dialogActive && Input.GetKeyDown(KeyCode.Space) ||
            Input.GetButtonDown("Select"))
        {

            currentLine++;
            }

            if (currentLine >= dialogLines.Length)
            {
                dBox.SetActive(false);
                dialogActive = false;

                currentLine = 0;
                thePlayer.canMove = true;
            }

            dText.text = dialogLines[currentLine];

            /*if(currentLine["false"])
            {

            }*/

        
    }

    /* public void ShowBox(string dialogue)
     {
         dialogActive = true;
         dBox.SetActive(true);
         dText.text = dialogue;
     }*/


    public void ShowDialogue()
    {
        dialogActive = true;
        dBox.SetActive(true);
        thePlayer.canMove = false;
        //Animation.Stop;
    }
        
    
    
}
