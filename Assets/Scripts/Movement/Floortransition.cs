using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floortransition : MonoBehaviour
{
    [SerializeField] private GameObject _otherFloor;
    [SerializeField] private bool _onOrOff;

    private void OnCollisionStay(Collision other)
    {
        _otherFloor.SetActive(_onOrOff);
    }
}
