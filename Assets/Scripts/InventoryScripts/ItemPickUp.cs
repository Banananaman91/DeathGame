using MovementNEW;
using UnityEngine;

namespace InventoryScripts
{
    public class ItemPickUp : MonoBehaviour, IInteract
    {
        [SerializeField] private InventoryScript _invScript;        //access InventoryScript class
        [SerializeField] private Sprite _objectSprite;
        public Sprite ObjectSprite => _objectSprite;

        public void Interact(PlayerMovement playerInteraction)
        {
            if (_invScript == null) return;
            _invScript.AddItem(this);      //go to InventoryScript void AddItem()
        }
    }
}