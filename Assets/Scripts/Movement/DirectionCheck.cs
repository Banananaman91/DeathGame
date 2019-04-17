using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCheck : MonoBehaviour {

    public bool Collided;
    void OnCollisionEnter2D(Collision2D coll)
    {
        Collided = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Collided = false;
    }
}
