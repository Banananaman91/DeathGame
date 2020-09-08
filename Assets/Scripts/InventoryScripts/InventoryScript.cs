using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class InventoryScript : MonoBehaviour {

        [SerializeField] private GameObject _buttonPrefab;
        [SerializeField] private ManagerScript _manager;
        [SerializeField] private Transform _parentObject;
        private readonly List<GameObject> _items = new List<GameObject>();
        public List<GameObject> Items => _items;
        
        public void AddItem(ItemPickUp newItem)
        {
            var invSlot = Instantiate(_buttonPrefab, _parentObject);
            _items.Add(newItem.gameObject);
            var button = invSlot.GetComponentInChildren<Button>();
            button.image.sprite = newItem.ObjectSprite;
            //button.onClick.AddListener(() => _manager.LoadItemInformation(newItem)); //Make it so item information is loaded using ItemPickUp
        }
    }
}
