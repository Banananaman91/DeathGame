﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Message Dialogue", order = 1), Serializable]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private List<Message> _messages;
        public List<Message> Messages
        {
            get => _messages;
            set => _messages = value;
        }
    }
}
