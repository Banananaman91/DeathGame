using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemPickUp : MonoBehaviour {
    public InventoryScript invScript;        //access InventoryScript class
    ItemInfo itemInfo;
    private SpriteRenderer _sprite;
    private BoxCollider2D _collider;

	// Use this for initialization
	void Start () {
        itemInfo = gameObject.GetComponent<ItemInfo>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Interact(DeathMovement playerInteraction)
    {
         invScript.AddItem(gameObject);      //go to InventoryScript void AddItem()
        _sprite.enabled = false;
        _collider.enabled = false;
    }
}