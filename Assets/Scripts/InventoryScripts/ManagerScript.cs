using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {

    public GameObject invPanel, journalInvPanel, itemInfo;    //reference to a GameObject
    public InventoryScript inventoryScript;                     //reference to InventoryScript
    public JournalInventoryScript journalScript;
    [SerializeField] Text nameOfItem, descriptionOfItem;        //Serialize Text fields for information panel
    [HideInInspector]
    public bool active = false;    // bool to check if inventory is open or closed


    // Use this for initialization
    void Start () {
        invPanel.SetActive(false);                  //sets the GameObject as unactive
        journalInvPanel.SetActive(false);           //sets the GameObject as unactive
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))        //if statement that activates when button I is pressed
        {
            invPanel.SetActive(true);           //sets GameObject as active
            journalInvPanel.SetActive(true);    //sets GameObject as active
            if (active == false)                //if statement that checks the bool
            {
                active = true;                  //changes the bool
            }
            else if(active == true)             //if statement that checks the bool
            {
                active = false;                 //changes the bool
                invPanel.SetActive(false);      //sets the GameObject as unactive
                journalInvPanel.SetActive(false);      //sets the GameObject as unactive
                itemInfo.SetActive(false);
            }
        }
    }

    public void LoadInfo(int _buttonNumber)         //function that Loads information onto the Description panel
    {
        nameOfItem.text = inventoryScript._items[_buttonNumber].GetComponent<ItemInfo>().itemName;                   //Loads name of item that was pressed onto Description panel
        descriptionOfItem.text = inventoryScript._items[_buttonNumber].GetComponent<ItemInfo>().itemDescription;     //Loads description of item that was pressed onto Description panel
    }

    public void LoadGood()
    {
        nameOfItem.text = "Good Journal";
        descriptionOfItem.text = "";
        foreach (GameObject page in journalScript.GoodPages)
        {
            if(page != null) descriptionOfItem.text += page.GetComponent<ItemInfo>().itemDescription + System.Environment.NewLine;
        }
    }
    public void LoadEvil()
    {
        nameOfItem.text = "Evil Journal";
        descriptionOfItem.text = "";
        foreach (GameObject page in journalScript.EvilPages)
        {
            if (page != null) descriptionOfItem.text += page.GetComponent<ItemInfo>().itemDescription + System.Environment.NewLine;
        }
    }
    public void LoadDeceitful()
    {
        nameOfItem.text = "Deceitful Journal";
        descriptionOfItem.text = "";
        foreach (GameObject page in journalScript.DeceitfulPages)
        {
            if (page != null) descriptionOfItem.text += page.GetComponent<ItemInfo>().itemDescription + System.Environment.NewLine;
        }
    }
    public void LoadUnfortunate()
    {
        nameOfItem.text = "Unfortunate Journal";
        descriptionOfItem.text = "";
        foreach (GameObject page in journalScript.UnfortunatePages)
        {
            if (page != null) descriptionOfItem.text += page.GetComponent<ItemInfo>().itemDescription + System.Environment.NewLine;
        }
    }
}
