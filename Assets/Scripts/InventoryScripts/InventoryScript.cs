using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class InventoryScript : MonoBehaviour {

        [SerializeField] private Button[] _itemPlace;                       //creates an array of Buttons
        [SerializeField] private GameObject[] _items;                       //creates an array of GameObjects
        [SerializeField] private ManagerScript _manager;                    //reference to ManagerScript
        public GameObject[] Items => _items;

        private void Awake()
        {
            for (int i = 0; i < _items.Length; i++)              //loop which goes through all the items GameObjects                                           
            {
                _itemPlace[i].gameObject.SetActive(false);       //turns off buttons in inventory at the start of the game
            }
        }

        public void AddItem(ItemPickUp newItem)                                                           //function that adds item into inventory (requires a GameObject)
        {
           for(var i = 0; i <= _itemPlace.Length; i++)
           {
               if (_items[i] != null) continue;
               _items[i] = newItem.gameObject;
               _itemPlace[i].gameObject.SetActive(true);
               _itemPlace[i].image.sprite = newItem.ObjectSprite;
               _itemPlace[i].onClick.AddListener(() => _manager.LoadItemInformation(i));
               return;
           }
        }
    }
}
