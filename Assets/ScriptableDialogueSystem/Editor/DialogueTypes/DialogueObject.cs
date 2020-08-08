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
        [SerializeField] protected UnityEvent _myEvent;

        public UnityEvent MyEvent => _myEvent;

        public void ResponseTrigger()
        {
            _myEvent.Invoke();
        }
    }
}
