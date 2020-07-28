using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace ScriptableDialogueSystem.Example.Scripts
{
    public class DialogueExample : DialogueObject
    {
        public void Interact()
        {
            _pageRender.AssignResponseObject(this);
            _pageRender.PlayDialogue(_myDialogue);
        }
    }
}
