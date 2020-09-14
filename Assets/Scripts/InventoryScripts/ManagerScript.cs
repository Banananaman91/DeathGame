using System;
using MovementNEW;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class ManagerScript : MonoBehaviour {

        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private InventoryScript _inventoryScript;
        [SerializeField] private GameObject[] _disableUiObjects;
        [Header("Variables to display pick up information")]
        [SerializeField] private Text _nameOfItem;
        [SerializeField] private Text _descriptionOfItem;
        [SerializeField] private Image _imageOfItem;
        [Header("Scripts to disable player while in inventory")]
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
                    foreach (var uiObject in _disableUiObjects)
                    {
                        if (uiObject.activeSelf) uiObject.SetActive(false);
                    }
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
                    foreach (var uiObject in _otherUiObjects)
                    {
                        if (uiObject.activeSelf) uiObject.SetActive(false);
                    }
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
            _imageOfItem.sprite = item.ObjectSprite;
        }
    }
}
