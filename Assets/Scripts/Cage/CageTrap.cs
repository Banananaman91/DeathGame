using System.Linq;
using DialogueTypes;
using MovementNEW;
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
                _alternativePageRender.PlayParagraphCycle(_dialogue, _npcImages,_startMessage, this);
            }
            else _pageRender.PlayParagraphCycle(_dialogue, _npcImages,_trapTrigger, this);
        }

        public override void ResponseTrigger()
        {
            _trap.SetActive(true);
        }
    }
}
