using DialogueTypes;
using MovementNEW;
using UnityEngine;

namespace Cage
{
    public class CageTrap : DialogueObject, IInteract
    {
        [SerializeField] private GameObject _trap;
        public void Interact(PlayerMovement playerInteraction)
        {
            _pageRender.PlayParagraphCycle(_dialogue, _npcImages,_startMessage, this);
        }

        public override void ResponseTrigger()
        {
            _trap.SetActive(true);
        }
    }
}
