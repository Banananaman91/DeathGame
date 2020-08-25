using InventoryScripts;
using MovementNEW;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Interaction
{
    public class FurnitureInteract : DialogueObject , IInteract
    {
        [Header("Inventory objects")]
        [SerializeField] private GameObject[] _objectsInside;
        [SerializeField] private InventoryScript _inventoryScript;
        [SerializeField] private JournalInventoryScript _journalInventory;
        [SerializeField] private bool _hiddenObjectCheck;
        [SerializeField] private GameObject _hiddenObject;
        [SerializeField] private int _hiddenObjectPosition;
        private int _collectedObject;
        private bool _noObjectsLeft;
        
        private void OnValidate()
        {
            if (!_hiddenObjectCheck) _hiddenObjectPosition = -1;
            if (_hiddenObjectCheck && !_hiddenObject) Debug.LogWarning(gameObject.name + ": _hiddenObject is null, make it not null");
        }
        public void Interact(PlayerMovement playerInteraction)
        {
            _pageRender.AssignResponseObject(this);
            _pageRender.PlayDialogue(_myDialogue);
        }

        public void SwapDialogue(Dialogue dialogueObject)
        {
            _myDialogue = dialogueObject;
        }
        
        //array dialogues
        //list prerequisite objects
        //int found items count
        
        //void CheckInv(GameObject inv)
        //if prereq == null return
        //var itemInv = getcomponent inventory
        //var jourInv = getcomponent journalinventory
        //itemsCount = 0
        //if itemInv
            //foreach item in prereq
                //if itemInv contains item itemsCount++
        //Same for JourInv
        //ifnot do nothing
        
        //if itemsCount == prereq.count
        //do something
    }
}
