using UnityEngine;

namespace InventoryScripts
{
    public class ItemInfo : MonoBehaviour {

        [SerializeField] private string _itemName;
        [SerializeField] private string _itemDescription;
        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;

    }
}
