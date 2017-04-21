using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour {

    private PlayerController thePlayer;

    private CameraController theCamera;

    public Vector2 startDirection;

    public string pointName;

    //public Reload reload;

    // Use this for initialization
    void Start() {
        thePlayer = FindObjectOfType<PlayerController>();

        if(thePlayer.startPoint == pointName)
        {
            thePlayer.transform.position = transform.position;

            //thePlayer.lastMove = new Vector2(0, -1f);

            theCamera = FindObjectOfType<CameraController>();
            theCamera.transform.position = new Vector3(transform.position.x, transform.position.y,
                theCamera.transform.position.z);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
