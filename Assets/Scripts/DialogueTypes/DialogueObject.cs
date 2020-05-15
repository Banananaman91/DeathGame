using System;
using UnityEngine;

namespace DialogueTypes
{
    public class DialogueObject : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private NpcImages _npcImages;
        [SerializeField] private RenderDialogue _pageRender;
        [SerializeField] private int _startMessage;
        public int PageClass { get; set; }
        public Sprite SpriteObject { get; set; }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Interact();
            if (Input.GetKeyDown(KeyCode.Alpha1)) _startMessage = 1;
        }

        public void Interact(DeathMovement thePlayer = null) => _pageRender.PlayParagraphCycle(_dialogue, _npcImages,_startMessage);
    }
}
