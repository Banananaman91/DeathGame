using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    public class DialogueObject : MonoBehaviour
    {
        [Header("Dialogue")]
        [SerializeField] protected Dialogue _myDialogue;
        [SerializeField] protected RenderDialogue _pageRender;
        [SerializeField] protected UnityEvent[] _myEvent;
        private int _eventNum;
        public UnityEvent[] MyEvent => _myEvent;

        public void EventTrigger(int num)
        {
            _myEvent[num].Invoke();
        }
    }
}
