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

        public List<Response> Responses => _responses;

        public int NextMessage
        {
            get => _nextMessage;
            set => _nextMessage = value;
        }
    }
}
