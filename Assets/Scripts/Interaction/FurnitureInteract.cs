using InventoryScripts;
using MovementNEW;
using Saving;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Interaction
{
    public class FurnitureInteract : DialogueObject, IInteract
    {
        [Header("Inventory objects")] [SerializeField]
        private GameObject[] _objectsInside;

        [SerializeField] private InventoryScript _inventoryScript;
        [SerializeField] private bool _hiddenObjectCheck;
        [SerializeField] private GameObject _hiddenObject;
        [SerializeField] private int _hiddenObjectPosition;
        private SaveHandler SaveHandler => FindObjectOfType<SaveHandler>();
        private int _collectedObject;
        private bool _noObjectsLeft;

        private void OnValidate()
        {
            if (!_hiddenObjectCheck) _hiddenObjectPosition = -1;
            if (_hiddenObjectCheck && !_hiddenObject)
                Debug.LogWarning(gameObject.name + ": _hiddenObject is null, make it not null");
        }

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
