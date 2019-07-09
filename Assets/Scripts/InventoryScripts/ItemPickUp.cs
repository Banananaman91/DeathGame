﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemPickUp : MonoBehaviour {
    [SerializeField] private InventoryScript _invScript;        //access InventoryScript class
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] public int _itemPlace;

    public void Interact(DeathMovement playerInteraction)
    {
        if (_invScript == null || _sprite == null || _collider == null) return;
        _invScript.AddItem(gameObject);      //go to InventoryScript void AddItem()
        _sprite.enabled = false;
        _collider.enabled = false;
    }
}