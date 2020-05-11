﻿using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class InventoryScript : MonoBehaviour {

        [SerializeField] private Button[] _itemPlace;                       //creates an array of Buttons
        [SerializeField] private GameObject[] _items;                       //creates an array of GameObjects
        [SerializeField] private ManagerScript _manager;                    //reference to ManagerScript
        public GameObject[] Items => _items;

        // Use this for initialization
        void Start () {
            for (int i = 0; i < _items.Length; i++)              //loop which goes through all the items GameObjects                                           
            {
                _itemPlace[i].gameObject.SetActive(false);       //turns off buttons in inventory at the start of the game
            }
        }
        public void AddItem(ItemPickUp newItem)                                                           //function that adds item into inventory (requires a GameObject)
        {
            Sprite sprite = newItem.SpriteObject;
            _items[newItem.ItemPlace] = newItem.gameObject;
            _itemPlace[newItem.ItemPlace].gameObject.SetActive(true);
            _itemPlace[newItem.ItemPlace].image.overrideSprite = sprite;
            _itemPlace[newItem.ItemPlace].onClick.AddListener(() => _manager.LoadItemInformation(newItem.ItemPlace));
        }
    }
}
