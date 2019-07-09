using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    [SerializeField] GameObject pause;
    public bool PauseState { get; set; }

    public void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update () {

        if (PauseState == false)
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
            if (PauseState == false)
            {
                PauseState = true;
            }
            else
            {
                PauseState = false;
            }
        }
	}
}
