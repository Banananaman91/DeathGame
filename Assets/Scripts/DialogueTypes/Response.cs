using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueTypes
{
    [Serializable]
    public class Response
    {
        [SerializeField] private int _next;
        [SerializeField] private string _reply;
        public int Next => _next;
        public string Reply => _reply;
    }
}
