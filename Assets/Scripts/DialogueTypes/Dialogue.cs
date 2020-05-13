using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueTypes
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 1), Serializable]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private string _npcName;
        [SerializeField] private List<Message> _messages;
        public String NpcName => _npcName;
        public List<Message> Messages => _messages;
    }
}
