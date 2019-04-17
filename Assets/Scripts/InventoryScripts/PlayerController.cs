using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;//Floating point variable to store the player's movement speed.


    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;         //turn off cursor
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * speed;

        transform.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, transform.position.z);
    }
}
