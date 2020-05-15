using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CameraMovement cameraScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;
        if (input != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(input);
            playerRb.velocity = (cameraScript.CameraF * input.z + cameraScript.CameraR * input.x) * speed;
            playerRb.rotation = rotation;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var roomNumber = other.gameObject.GetComponent<RoomNumbers>();
        if (roomNumber)
        {
            cameraScript.ChangeRoom(roomNumber.RoomToCamera);
        }
    }
}
