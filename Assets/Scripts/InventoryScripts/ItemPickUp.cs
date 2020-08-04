using MovementNEW;
using UnityEngine;

namespace InventoryScripts
{
    public class ItemPickUp : MonoBehaviour, IInteract
    {
        [SerializeField] private InventoryScript _invScript;        //access InventoryScript class
        [SerializeField] private int _itemPlace;
        [SerializeField] private ItemPickUp thisItem;
        [SerializeField] private Sprite _objectSprite;
        public int ItemPlace => _itemPlace;
        public Sprite ObjectSprite => _objectSprite;

        public void Interact(PlayerMovement playerInteraction)
        {
            if (_invScript == null) return;
            _invScript.AddItem(thisItem);      //go to InventoryScript void AddItem()
        }
    }
}