using Boo.Lang;
using MovementNEW;
using Pages;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class InventoryScript : MonoBehaviour {

        [Header("Inventory variables")]
        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private ManagerScript _manager;
        [SerializeField] private Transform _parentObject;
        private readonly List<ItemPickUp> _items = new List<ItemPickUp>();
        public List<ItemPickUp> Items => _items;
        //
        [Header("Journal Inventory variables")]
        [SerializeField] private Button[] _pageButtons;
        [Tooltip("In inspector write down the size of array to fit number of available pages")]
        [SerializeField] private GameObject[] _pageObjects;
        [SerializeField] private PlayerMovement _thePlayer;
        public GameObject[] PageObjects => _pageObjects;
        
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
            _pageObjects[page.PageClass] = page.gameObject;
            _pageButtons[page.PageClass].image.overrideSprite = page.SpriteObject;
            _pageButtons[page.PageClass].onClick.AddListener(() => page.Interact(_thePlayer));
            _pageButtons[page.PageClass].GetComponentInChildren<Text>().text = page.Clue;
        }
    }
}
