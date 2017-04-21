using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Reload : MonoBehaviour
{

    private CameraController theCamera;

    public Vector2 startDirection;

    public string pointName;

    public PlayerHealthManager playerHealth;

    public PlayerController thePlayer;

    private DialogueManager theDM;

    public float waitToReload;

    private static bool reloadExists;

    public bool reloadIs;


    void Start()

    {
        FindObjectOfType<PlayerController>();

        theDM = FindObjectOfType<DialogueManager>();

        if (!reloadExists)
        {
            reloadExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        reloadIs = false;

    if(playerHealth.playerCurrentHealth <= 0)
        {
            waitToReload -= Time.deltaTime;
            reloadIs = true;

            thePlayer.lastMove = new Vector2(0, -1f);


            if (waitToReload <= 0)
            {

                thePlayer.swingBig.SetActive(false);
                thePlayer.swingBig.transform.localRotation = new Quaternion(0, 0, 0, 0);



                playerHealth.playerCurrentHealth = playerHealth.playerMaxHealth;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                playerHealth.gameObject.SetActive(true);

                waitToReload = 2;

                if(thePlayer.startPoint == pointName)
                {
                    thePlayer.transform.position = transform.position;
                    //thePlayer.lastMove = startDirection;

                    theDM.dialogActive = false;
                    theDM.dBox.SetActive(false);
                    thePlayer.canMove = true;


                    theCamera = FindObjectOfType<CameraController>();
                    theCamera.transform.position = new Vector3(transform.position.x, transform.position.y,
                        theCamera.transform.position.z);
                }
         

            }

        
                
            }

        }

    
}


