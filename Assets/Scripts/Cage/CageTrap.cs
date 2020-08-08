using System.Linq;
using MovementNEW;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Cage
{
    public class CageTrap : DialogueObject, IInteract
    {
        [SerializeField] private GameObject _trap;
        [SerializeField] private GameObject[] _requiredItems;
        [SerializeField] private int _trapTrigger;
        private int _itemCount;
        public void Interact(PlayerMovement playerInteraction)
        {
            foreach (var item in _requiredItems)
            {
                if (playerInteraction.JournalInventory.Books.Contains(item)) _itemCount++;
            }

            if (_itemCount != _requiredItems.Length)
            {
                _itemCount = 0;
            }
            else _pageRender.PlayDialogue(_myDialogue);
        }
    }
}
