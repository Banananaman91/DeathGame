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
    private Vector3 _playerInput;
    private Quaternion _playerRotation;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CameraMovement cameraScript;
    
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (_playerInput == Vector3.zero) return;
        _playerInput = _playerInput.normalized;
        _playerRotation = Quaternion.LookRotation(_playerInput);
        playerRb.velocity = (cameraScript.CameraF * _playerInput.z + cameraScript.CameraR * _playerInput.x) * speed;
        playerRb.rotation = _playerRotation;
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
