using System;
using MovementNEW;
using UnityEngine;

namespace DialogueTypes
{
    public class DialogueObject : MonoBehaviour
    {
        [Header("Dialogue")]
        [SerializeField] protected Dialogue _dialogue;
        [SerializeField] protected NpcImages _npcImages;
        [SerializeField] protected RenderDialogue _pageRender;
        [SerializeField] protected RenderDialogue _alternativePageRender;
        [SerializeField] protected int _startMessage;

        public virtual void ResponseTrigger()
        {
            throw new Exception("ResponseTrigger not initialised");
        }
    }
}
