using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class ManagerScript : MonoBehaviour {

        [SerializeField] private GameObject _inventoryPanel, _journalInventoryPanel, _itemInformation;    //reference to a GameObject
        [SerializeField] private InventoryScript _inventoryScript;                     //reference to InventoryScript
        [SerializeField] private JournalInventoryScript _journalScript;
        [SerializeField] private Text _nameOfItem, _descriptionOfItem;        //Serialize Text fields for information panel
        public bool Active { get; private set; }    // bool to check if inventory is open or closed
        private bool IsInventoryPanelNotNull => _inventoryPanel != null;
        private bool IsJournalInventoryPanelNotNull => _journalInventoryPanel != null;
        private bool IsItemInformationNotNull => _itemInformation != null;

        private void Awake()
        {
            _inventoryPanel.SetActive(true);
            _journalInventoryPanel.SetActive(true);
        }

        // Use this for initialization
        void Start ()
        {
            if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(false);                  //sets the GameObject as unactive
            if (IsJournalInventoryPanelNotNull) _journalInventoryPanel.SetActive(false);           //sets the GameObject as unactive
        }
#if UNITY_STANDALONE
        // Update is called once per frame
        void Update () {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))        //if statement that activates when button I is pressed
            {
                if (!Active)                //if statement that checks the bool
                {
                    Active = true;                  //changes the bool
                    if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(true);           //sets GameObject as active
                    if (IsJournalInventoryPanelNotNull) _journalInventoryPanel.SetActive(true);    //sets GameObject as active
                }
                else if(Active)             //if statement that checks the bool
                {
                    Active = false;                 //changes the bool
                    if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(false);      //sets the GameObject as unactive
                    if (IsJournalInventoryPanelNotNull) _journalInventoryPanel.SetActive(false);      //sets the GameObject as unactive
                    if (IsItemInformationNotNull) _itemInformation.SetActive(false);
                }
            }
        }
#elif UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
        public void Inventory()
        {
            if (!Active)                //if statement that checks the bool
            {
                Active = true;                  //changes the bool
                if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(true);           //sets GameObject as active
                if (IsJournalInventoryPanelNotNull) _journalInventoryPanel.SetActive(true);    //sets GameObject as active
            }
            else if(Active)             //if statement that checks the bool
            {
                Active = false;                 //changes the bool
                if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(false);      //sets the GameObject as unactive
                if (IsJournalInventoryPanelNotNull) _journalInventoryPanel.SetActive(false);      //sets the GameObject as unactive
                if (IsItemInformationNotNull) _itemInformation.SetActive(false);
            }
        }
#endif

        public void LoadItemInformation(int buttonNumber)         //function that Loads information onto the Description panel
        {
            _itemInformation.SetActive(true);
            _nameOfItem.text = _inventoryScript.Items[buttonNumber].GetComponent<ItemInfo>().ItemName;                   //Loads name of item that was pressed onto Description panel
            _descriptionOfItem.text = _inventoryScript.Items[buttonNumber].GetComponent<ItemInfo>().ItemDescription;     //Loads description of item that was pressed onto Description panel
        }

        public void LoadPageRiddles(ItemInfo item)
        {
            _descriptionOfItem.text = "";
            _nameOfItem.text = item.ItemName;
            var itemDescription = item.ItemDescription.ToLower();
            foreach (var page in _journalScript.Books)
            {
                if (page == null) continue;
                bool foundPage = page.GetComponent<ItemInfo>().ItemName.ToLower().Contains(itemDescription);
                if (foundPage)
                {
                    Debug.Log("Found page");
                    _descriptionOfItem.text += page.GetComponent<ItemInfo>().ItemDescription + Environment.NewLine;
                }
            }
            _itemInformation.SetActive(true);
        }
    }
}
