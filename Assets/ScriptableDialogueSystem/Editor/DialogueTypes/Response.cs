using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    [Serializable]
    public class Response
    {
        [SerializeField] private int _next;
        [SerializeField] private string _reply;
        [SerializeField] private bool _triggerEvent;
        public int Next
        {
            get => _next;
            set => _next = value;
        }

        public string Reply
        {
            get => _reply;
            set => _reply = value;
        }

        public bool TriggerEvent
        {
            get => _triggerEvent;
            set => _triggerEvent = value;
        }
    }
}
