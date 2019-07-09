using System.Collections;
using System.Collections.Generic;
using DialogueScripts;
using InventoryScripts;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    [SerializeField] private DeathMovement _thePlayer;
    
    private bool _interact;
    
    private void Update()
    {

        _interact = (Input.GetKeyDown(KeyCode.E));

        if (!_interact) return;

        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale = _thePlayer.rayDir, 1.5f);
        Debug.DrawRay(transform.position, transform.localScale = _thePlayer.rayDir);
        // If it hits something...
        if (hit.collider != null)
        {
            Dialogue interactable = hit.collider.GetComponent<Dialogue>();
            ItemPickUp _itemInteractable = hit.collider.GetComponent<ItemPickUp>();
            CageTrap cageInteractable = hit.collider.GetComponent<CageTrap>();
            Mastermind masterInteractable = hit.collider.GetComponent<Mastermind>();
            RoomKey keyInteractable = hit.collider.GetComponent<RoomKey>();
            EasterEgg interactabubble = hit.collider.GetComponent<EasterEgg>();

            if (interactable == null && _itemInteractable == null && cageInteractable == null && masterInteractable == null && keyInteractable == null && interactabubble == null) return;

            if (interactable != null) interactable.Interact(_thePlayer);
            if (_itemInteractable != null) _itemInteractable.Interact(_thePlayer);
            if (cageInteractable != null) cageInteractable.Interact(_thePlayer);
            if (masterInteractable != null) masterInteractable.Interact(_thePlayer);
            if (keyInteractable != null) keyInteractable.Interact(_thePlayer);
            if (interactabubble != null) interactabubble.Interact(_thePlayer);
        }
        //_wasInteracting = interacting;
    }

}
