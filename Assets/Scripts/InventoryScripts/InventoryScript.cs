using System;
using System.Collections.Generic;
using System.Linq;
using MovementNEW;
using Pages;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class InventoryScript : MonoBehaviour {
        [SerializeField] private PlayerMovement _thePlayer;
        [SerializeField] private ManagerScript _manager;
        [Header("Inventory variables")]
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private Transform _parentObject;
        [Header("Journal Inventory variables")]
        [SerializeField] private Button[] _pageButtons = new Button[0]; 
        [SerializeField] private Page[] _pages = new Page[0];
        [SerializeField] private RenderDialogue[] _versionDialogues = new RenderDialogue[0];
        private readonly List<ItemPickUp> _items = new List<ItemPickUp>();
        public IEnumerable<ItemPickUp> Items => _items;
        public IEnumerable<Page> Pages => _pages;

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
            var num = page.PageClass == 0 ? 0 : Mathf.Floor(_pages.Length / page.PageClass);
            if (page.PureEvil) num = _versionDialogues.Length;
            _pageButtons[page.PageClass].onClick.AddListener(() => _versionDialogues[(int)num].PlayDialogue(page.MyDialogue));
            _pageButtons[page.PageClass].GetComponentInChildren<Text>().text = page.Clue;
        }
    }
}
