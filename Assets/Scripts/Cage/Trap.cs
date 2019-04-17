using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    [SerializeField] DeathMovement movement;

    private void OnTriggerEnter2D(Collider2D trap)
    {
        movement.enabled = false;
    }
}
