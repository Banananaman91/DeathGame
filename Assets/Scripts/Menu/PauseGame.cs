using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    [SerializeField] GameObject pause;
    public bool _pauseState = false;

    public void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update () {

        if (_pauseState == false)
        {
            pause.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pause.SetActive(true);
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseState == false)
            {
                _pauseState = true;
            }
            else
            {
                _pauseState = false;
            }
        }
	}
}
