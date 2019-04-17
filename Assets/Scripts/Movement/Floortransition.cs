using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floortransition : MonoBehaviour {
    [SerializeField] private GameObject _endPoint; //FIXING UR SHIT DAWG I GOT YOU
    [SerializeField] private GameObject _player;

    //modified this
    public void MovePlayerToEndpoint()
    {
        _player.transform.position = _endPoint.transform.position;            //teleport player
    }
}
