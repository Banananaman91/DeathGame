using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    [Serializable]
    public class Message
    {
        [SerializeField] private string _npcName;
        [SerializeField] private string _messageText;
        [SerializeField] private string _npcMood;
        [SerializeField] private List<Response> _responses;
        [SerializeField] private int _nextMessage;
        [SerializeField] private bool _triggerEvent;
        [SerializeField] private int _eventNum;
        public String NpcName
        {
            get => _npcName;
            set => _npcName = value;
        }

        public string NpcMood
        {
            get => _npcMood;
            set => _npcMood = value;
        }

        public string MessageText
        {
            get => _messageText;
            set => _messageText = value;
        }

        public List<Response> Responses
        {
            get => _responses;
            set => _responses = value;
        }

        public int NextMessage
        {
            get => _nextMessage;
            set => _nextMessage = value;
        }

        public bool TriggerEvent
        {
            get => _triggerEvent;
            set => _triggerEvent = value;
        }

        public int EventNum
        {
            get => _eventNum;
            set => _eventNum = value;
        }
    }
}
