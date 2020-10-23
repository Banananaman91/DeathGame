using System;
using MovementNEW;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Interaction
{
    public class Introduction : DialogueObject
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.GetComponent<PlayerMovement>()) return;
            _pageRender.AssignResponseObject(this);
            _pageRender.PlayDialogue(_myDialogue);
        }
    }
}
