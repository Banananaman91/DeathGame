using InventoryScripts;
using MovementNEW;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

public class FurnitureInteract : DialogueObject , IInteract
{
    [Header("Inventory objects")]
    [SerializeField] private GameObject[] _objectsInside;
    [SerializeField] private InventoryScript _inventoryScript;
    [SerializeField] private JournalInventoryScript _journalInventory;
    [SerializeField] private bool _hiddenObjectCheck;
    [SerializeField] private GameObject _hiddenObject;
    [SerializeField] private int _hiddenObjectPosition;
    [Header("Starting Message location")]
    [SerializeField] private int _unlockedStartMessage;
    [SerializeField] private int _unlockedEndMessage;
    public int _collectedObject;
    private bool _noObjectsLeft;

    private void OnValidate()
    {
        if (!_hiddenObjectCheck) _hiddenObjectPosition = -1;
        if (_hiddenObjectCheck && !_hiddenObject) Debug.LogWarning(gameObject.name + ": _hiddenObject is null, make it not null");
    }

    public void Interact(PlayerMovement playerInteraction)
    {

        // if (_collectedObject == _hiddenObjectPosition && (!_journalInventory.Books.Contains(_hiddenObject) &&
        //                                                   !_inventoryScript.Items.Contains(_hiddenObject))) _startMessage = _unlockedEndMessage;
        //
        // else if (_collectedObject == _hiddenObjectPosition && (_journalInventory.Books.Contains(_hiddenObject) ||
        //                                                        _inventoryScript.Items.Contains(_hiddenObject))) _startMessage = _unlockedStartMessage;
        //
        _pageRender.AssignResponseObject(this);
        _pageRender.PlayDialogue(_myDialogue);
    }
}
