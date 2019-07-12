using UnityEngine;

namespace InventoryScripts
{
    public class ItemPickUp : MonoBehaviour, IInteract
    {
        [SerializeField] private InventoryScript _invScript;        //access InventoryScript class
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private int _itemPlace;
        [SerializeField] private ItemPickUp thisItem;
        public Sprite SpriteObject => gameObject.GetComponent<SpriteRenderer>().sprite;
        public int ItemPlace => _itemPlace;

        public void Interact(DeathMovement playerInteraction)
        {
            if (_invScript == null || _sprite == null || _collider == null) return;
            _invScript.AddItem(thisItem);      //go to InventoryScript void AddItem()
            _sprite.enabled = false;
            _collider.enabled = false;
        }
    }
}