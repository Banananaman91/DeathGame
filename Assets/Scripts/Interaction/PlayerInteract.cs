using System.Collections;
using System.Collections.Generic;
using Cage;
using DialogueTypes;
using InventoryScripts;
using MovementNEW;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    [SerializeField] private PlayerMovement _thePlayer;
    
    private bool _interact;
    
    private void Update()
    {
#if UNITY_STANDALONE
        _interact = (Input.GetKeyDown(KeyCode.Space));
#endif
        if (!_interact) return;
        _interact = false;
        // Cast a ray straight down.
        RaycastHit hit;
        Physics.Raycast(transform.position, _thePlayer.RayDir, out hit, 1.5f);
        Debug.DrawRay(transform.position, _thePlayer.RayDir);
        // If it hits something...
        if (hit.collider == null) return;
        IInteract interactable = hit.collider.GetComponent<IInteract>();

        interactable?.Interact(_thePlayer);
        //_wasInteracting = interacting;
    }
#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE
    public void Interact()
    {
        _interact = true;
    }
#endif
}
