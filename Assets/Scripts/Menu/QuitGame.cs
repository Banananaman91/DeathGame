using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {

    public void QuitToMenu ()
    {
        SceneManager.LoadScene(0);
    }

    public void GameExit ()
    {
        Application.Quit();
    }
}
