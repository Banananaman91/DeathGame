using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueTypes
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 1), Serializable]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private List<Message> _messages;
        public List<Message> Messages => _messages;
    }
}
