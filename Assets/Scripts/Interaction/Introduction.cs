using System;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Interaction
{
    public class Introduction : DialogueObject
    {
        private void OnCollisionEnter(Collision other)
        {
            _myEvent[0].Invoke();
        }
    }
}
