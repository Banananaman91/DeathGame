using System;
using Boo.Lang;
using MovementNEW;
using Pages;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class InventoryScript : MonoBehaviour {

        [SerializeField] private PlayerMovement _thePlayer;
        [Header("Inventory variables")]
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private ManagerScript _manager;
        [SerializeField] private Transform _parentObject;
        [Header("Journal Inventory variables")]
        [SerializeField] private Button[] _pageButtons; 
        [SerializeField] private Page[] _pages;
        private readonly List<ItemPickUp> _items = new List<ItemPickUp>();
        public List<ItemPickUp> Items => _items;
        public Page[] Pages => _pages;

        private void OnValidate()
        {
            if (_pages.Length != _pageButtons.Length) _pages = new Page[_pageButtons.Length];
        }

        public void AddItem(ItemPickUp newItem)
        {
            var invSlot = Instantiate(_buttonPrefab, _parentObject);
            _items.Add(newItem);
            var button = invSlot.GetComponentInChildren<Button>();
            button.image.sprite = newItem.ObjectSprite;
            button.onClick.AddListener(() => _manager.LoadItemInformation(newItem));
            _manager.LoadItemInformation(newItem);
        }
        
        public void AddPage(Page page)
        {
            _pages[page.PageClass] = page;
            _pageButtons[page.PageClass].image.overrideSprite = page.SpriteObject;
            _pageButtons[page.PageClass].onClick.AddListener(() => page.Interact(_thePlayer));
            _pageButtons[page.PageClass].GetComponentInChildren<Text>().text = page.Clue;
        }
    }
}
