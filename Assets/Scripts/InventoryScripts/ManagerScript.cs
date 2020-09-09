using System;
using MovementNEW;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class ManagerScript : MonoBehaviour {

        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private InventoryScript _inventoryScript;
        [SerializeField] private Text _nameOfItem, _descriptionOfItem;
        [Header("Variables to disable player while in inventory")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerInteract _playerInteract;
        public bool Active { get; private set; }    // bool to check if inventory is open or closed
        private bool IsInventoryPanelNotNull => _inventoryPanel != null;

        private void Awake()
        {
            _inventoryPanel.SetActive(true);
        }

        // Use this for initialization
        void Start ()
        {
            if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(false);
        }
#if UNITY_STANDALONE
        // Update is called once per frame
        void Update () {
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
            {
                if (!Active)
                {
                    Active = true;
                    if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(true);
                    _playerMovement.enabled = false;
                    _playerInteract.enabled = false;
                }
                else if(Active)
                {
                    Active = false;
                    if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(false);
                    _playerMovement.enabled = true;
                    _playerInteract.enabled = true;
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
            }
            else if(Active)             //if statement that checks the bool
            {
                Active = false;                 //changes the bool
                if (IsInventoryPanelNotNull) _inventoryPanel.SetActive(false);      //sets the GameObject as unactive
            }
        }
#endif

        public void LoadItemInformation(ItemPickUp item)
        {
            _nameOfItem.text = item.ItemName;
            _descriptionOfItem.text = item.ItemDescription;
        }

        public void LoadPageRiddles(ItemInfo item)
        {
            _descriptionOfItem.text = "";
            _nameOfItem.text = item.ItemName;
            var itemDescription = item.ItemDescription.ToLower();
            foreach (var page in _inventoryScript.PageObjects)
            {
                if (page == null) continue;
                bool foundPage = page.GetComponent<ItemInfo>().ItemName.ToLower().Contains(itemDescription);
                if (foundPage)
                {
                    Debug.Log("Found page");
                    _descriptionOfItem.text += page.GetComponent<ItemInfo>().ItemDescription + Environment.NewLine;
                }
            }
            //_itemInformation.SetActive(true);
        }
    }
}
