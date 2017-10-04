using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class MovePlayer : MonoBehaviour {

    public float speed = 10.0f;
    public Rigidbody rb;
    public float tilt;
    public Boundary boundary;
    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = move * speed;
       // rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),0.0f,Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        //rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
