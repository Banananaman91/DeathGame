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
    [Header("Starting Message location")]
    [SerializeField] private int _unlockedStartMessage;
    [SerializeField] private int _unlockedEndMessage;
    private int _collectedObject;
    private bool _noObjectsLeft;

    private void OnValidate()
    {
        if (_hiddenObjectCheck || _hiddenFirstObjectCheck)
        {
            if (!_hiddenObject)
            {
                Debug.LogWarning(gameObject.name + ": _hiddenObject is null, make it not null");
            }
        }
        if (!_hiddenFirstObjectCheck || !_hiddenObjectCheck) return;
        Debug.LogWarning(gameObject.name + ": _hiddenFirstObjectCheck and _hiddenObjectCheck can't both be true, set only one");
        _hiddenFirstObjectCheck = false;
        _hiddenObjectCheck = false;
    }

    public void Interact(PlayerMovement playerInteraction)
    {
        if (_collectedObject == 0 && _hiddenFirstObjectCheck)
        {
            if (!_journalInventory.Books.Contains(_hiddenObject) &&
                !_inventoryScript.Items.Contains(_hiddenObject))
            {
                _startMessage = _unlockedEndMessage;
            }
            else
            {
                _startMessage = _unlockedStartMessage;
                _hiddenFirstObjectCheck = false;
            }
        }
        
        if (_collectedObject != 0 && _hiddenObjectCheck)
        {
            if (!_journalInventory.Books.Contains(_hiddenObject) &&
                !_inventoryScript.Items.Contains(_hiddenObject))
            {
                _startMessage = _unlockedEndMessage;
            }
            else
            {
                _startMessage = _unlockedStartMessage;
                _hiddenObjectCheck = false;
            }
        }
        
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
