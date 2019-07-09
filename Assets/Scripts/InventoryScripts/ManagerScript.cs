using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {

    [SerializeField] private GameObject _invPanel, _journalInvPanel, _itemInfo;    //reference to a GameObject
    [SerializeField] private InventoryScript _inventoryScript;                     //reference to InventoryScript
    [SerializeField] private JournalInventoryScript _journalScript;
    [SerializeField] private Text _nameOfItem, _descriptionOfItem;        //Serialize Text fields for information panel
    [HideInInspector]
    public bool active;    // bool to check if inventory is open or closed


    // Use this for initialization
    void Start () {
        _invPanel.SetActive(false);                  //sets the GameObject as unactive
        _journalInvPanel.SetActive(false);           //sets the GameObject as unactive
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))        //if statement that activates when button I is pressed
        {
            _invPanel.SetActive(true);           //sets GameObject as active
            _journalInvPanel.SetActive(true);    //sets GameObject as active
            if (active == false)                //if statement that checks the bool
            {
                active = true;                  //changes the bool
            }
            else if(active)             //if statement that checks the bool
            {
                active = false;                 //changes the bool
                _invPanel.SetActive(false);      //sets the GameObject as unactive
                _journalInvPanel.SetActive(false);      //sets the GameObject as unactive
                _itemInfo.SetActive(false);
            }
        }
    }

    public void LoadInfo(int _buttonNumber)         //function that Loads information onto the Description panel
    {
        _nameOfItem.text = _inventoryScript._items[_buttonNumber].GetComponent<ItemInfo>().itemName;                   //Loads name of item that was pressed onto Description panel
        _descriptionOfItem.text = _inventoryScript._items[_buttonNumber].GetComponent<ItemInfo>().itemDescription;     //Loads description of item that was pressed onto Description panel
    }

    public void LoadPageRiddles(ItemInfo item)
    {
        _descriptionOfItem.text = "";
        _nameOfItem.text = item.itemName;
        foreach (var PAGE in _journalScript._books)
        {
            if (PAGE == null) continue;
            bool foundPage = PAGE.GetComponent<ItemInfo>().itemName.Contains(_nameOfItem.text);
            if (foundPage)
            {
                Debug.Log("Found page");
                _descriptionOfItem.text += PAGE.GetComponent<ItemInfo>().itemDescription + System.Environment.NewLine;
            }
        }
    }
}
