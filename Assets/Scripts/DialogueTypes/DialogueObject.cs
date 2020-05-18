using System;
using MovementNEW;
using UnityEngine;

namespace DialogueTypes
{
    public class DialogueObject : MonoBehaviour, IInteract
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private NpcImages _npcImages;
        [SerializeField] private RenderDialogue _pageRender;
        [SerializeField] private int _startMessage;
        public int PageClass { get; set; }
        public Sprite SpriteObject { get; set; }

        public void Interact(PlayerMovement thePlayer = null) => _pageRender.PlayParagraphCycle(_dialogue, _npcImages,_startMessage);
    }
}
