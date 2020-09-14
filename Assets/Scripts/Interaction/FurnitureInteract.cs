using InventoryScripts;
using MovementNEW;
using Saving;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Interaction
{
    public class FurnitureInteract : DialogueObject, IInteract
    {
        private SaveHandler SaveHandler => FindObjectOfType<SaveHandler>();
        private int _collectedObject;
        private bool _noObjectsLeft;

        public void Interact(PlayerMovement playerInteraction)
        {
            _pageRender.AssignResponseObject(this);
            _pageRender.PlayDialogue(_myDialogue);
        }

        public void SwapDialogue(Dialogue dialogueObject)
        {
            _myDialogue = dialogueObject;
            SaveDialogue();
        }

        private void SaveDialogue()
        {
            var objectID = GetInstanceID();
            if (SaveHandler.Dialogues.ContainsKey(objectID))
            {
                SaveHandler.Dialogues[objectID] = _myDialogue;
            }
            else
            {
                SaveHandler.Dialogues.Add(GetInstanceID(), _myDialogue);
            }
        }
    }
}
