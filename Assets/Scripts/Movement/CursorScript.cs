using System.Collections;
using System.Collections.Generic;
using InventoryScripts;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    public ManagerScript managerScript;
    public PauseGame pause;
    SpriteRenderer sp;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        sp = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x,Input.mousePosition.y,Camera.main.nearClipPlane));
        if(managerScript != null)
        {
            if (managerScript.Active || pause.PauseState)
            {
                sp.enabled = true;
            }
            else
            {
                sp.enabled = false;
            }
        }
    }
}
