using System.Collections;
using System.Collections.Generic;
using Cage;
using DialogueTypes;
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
            IInteract interactable = hit.collider.GetComponent<IInteract>();


            if (interactable == null) return;

            interactable.Interact(_thePlayer);

        }
        //_wasInteracting = interacting;
    }

}
