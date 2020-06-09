using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogueTypes;
using InventoryScripts;
using MovementNEW;
using Pages;
using UnityEngine;

public class FurnitureInteract : DialogueObject , IInteract
{
    [Header("Inventory objects")]
    [SerializeField] private GameObject[] _objectsInside;
    [SerializeField] private InventoryScript _inventoryScript;
    [SerializeField] private JournalInventoryScript _journalInventory;
    [SerializeField] private bool _hiddenFirstObjectCheck;
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

        if (_collectedObject == _hiddenObjectPosition && (!_journalInventory.Books.Contains(_hiddenObject) &&
                                                          !_inventoryScript.Items.Contains(_hiddenObject))) _startMessage = _unlockedEndMessage;
        
        else if (_collectedObject == _hiddenObjectPosition && (_journalInventory.Books.Contains(_hiddenObject) ||
                                                               _inventoryScript.Items.Contains(_hiddenObject))) _startMessage = _unlockedStartMessage;
        
        _pageRender.PlayParagraphCycle(_dialogue, _npcImages,_startMessage, this);
    }
    
    public override void ResponseTrigger()
    {
        if (_noObjectsLeft) return;
        var pageCheck = _objectsInside[_collectedObject].GetComponent<Page>();
        if (pageCheck != null)
        {
            _journalInventory.AddPage(pageCheck);
            _startMessage = _unlockedStartMessage;
            _collectedObject++;
            if (_collectedObject < _objectsInside.Length) return;
            _startMessage = _unlockedEndMessage;
            _noObjectsLeft = true;
            return;
        }
        var objectCheck = _objectsInside[_collectedObject].GetComponent<ItemPickUp>();
        if (objectCheck != null)
        {
            _inventoryScript.AddItem(objectCheck);
            _startMessage = _unlockedStartMessage;
            _collectedObject++;
            if (_collectedObject < _objectsInside.Length) return;
            _startMessage = _unlockedEndMessage;
            _noObjectsLeft = true;
        }
    }
}
