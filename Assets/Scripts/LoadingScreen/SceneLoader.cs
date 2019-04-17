using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    private int[] sNum = { 2 };
    private int sceneToRun;
    [SerializeField] Text spacebar, load;
    public RectTransform LoadingBar;
    private Vector3 LoadingBarScale;
    
	// Use this for initialization
	void Start () {
        spacebar.gameObject.SetActive(false);
        LoadingBarScale = LoadingBar.localScale;
        //sceneToRun = sNum[Random.Range(0, sNum.Length)];
        sceneToRun = 2;
        StartCoroutine(LoadScene(sceneToRun));
    }
	
	// Update is called once per frame
	void Update () {

	}
    
    IEnumerator LoadScene(int _sceneToRun){
        AsyncOperation async = SceneManager.LoadSceneAsync(_sceneToRun);
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            LoadingBarScale.x = async.progress;
            LoadingBar.localScale = LoadingBarScale;

            if(async.progress == 0.9f)
            {
                LoadingBarScale.x = 1;
                LoadingBar.localScale = LoadingBarScale;
                load.text = "Loaded";
                spacebar.gameObject.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    async.allowSceneActivation = true;
                }

            }
            yield return null;
        }
    }
}
