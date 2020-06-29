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
        [SerializeField] private bool _triggerEvent;
        public int Next => _next;
        public string Reply => _reply;

        public bool TriggerEvent => _triggerEvent;
    }
}
