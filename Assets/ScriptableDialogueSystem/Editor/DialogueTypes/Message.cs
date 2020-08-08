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
        public String NpcName => _npcName;
        public string NpcMood => _npcMood;
        public string MessageText => _messageText;
        public List<Response> Responses => _responses;

        public int NextMessage => _nextMessage;
    }
}
