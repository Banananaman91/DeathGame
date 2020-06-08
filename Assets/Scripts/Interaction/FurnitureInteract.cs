using System.Collections;
using System.Collections.Generic;
using DialogueTypes;
using InventoryScripts;
using MovementNEW;
using Pages;
using UnityEngine;

public class FurnitureInteract : DialogueObject , IInteract
{
    [SerializeField] private GameObject[] _objectsInside;
    [SerializeField] private InventoryScript _inventoryScript;
    [SerializeField] private JournalInventoryScript _journalInventory;
    [Header("Starting Message location")]
    [SerializeField] private int _unlockedStartMessage;
    [SerializeField] private int _unlockedEndMessage;
    private int _collectedObject;
    private bool _noObjectsLeft;
    public void Interact(PlayerMovement playerInteraction)
    {
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
