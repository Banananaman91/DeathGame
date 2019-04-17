using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTeleport : MonoBehaviour {
    public GameObject endPoint;
    GameObject player;
    PlayerController playerController;
    float charSpeed, timer;
    bool timerOn = false;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        charSpeed = playerController.speed;
	}
	
	// Update is called once per frame
	void Update () {
        if(timerOn == true)
        {
            timer += Time.deltaTime;
            playerController.speed = 0;
            if(timer > 1.5)
            {
                playerController.speed = charSpeed;
                timerOn = false;
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            player.transform.position = endPoint.transform.position;            //teleport player
    }
}
