using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCHaracterController : MonoBehaviour {
    public float sensitivity=3f;
    public float moveSpeed = 3f;
    public GameObject eyes;
    private float moveFB,moveLR,rotX,rotY;
    private CharacterController player;
    public float jumpForce=2f;
	// Use this for initialization
	void Start () {

        player = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        Movement();


	}

    public void Movement()
    {

        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        moveLR = Input.GetAxis("Horizontal") * moveSpeed;
        //get input from mouse
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;
        Vector3 movement = new Vector3(moveLR, 0, moveFB); //player movement control
        transform.Rotate(0, rotX, 0); //rotate player
        movement = transform.rotation * movement; //fix orientation
        eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0); //rotate camera on the y axis

        player.Move(movement * Time.deltaTime); //move the player
    }
    public void Jump()
    {
    }

}
