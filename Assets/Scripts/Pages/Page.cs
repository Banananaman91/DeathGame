using System;
using MovementNEW;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Pages
{
    public class Page : DialogueObject, IInteract
    {
        [Header("Page Information")]
        [SerializeField] private int _pageClass;
        [SerializeField] private bool _pureEvil;
        [SerializeField] private Sprite _spriteObject;
        [TextArea]
        [SerializeField] private string _clue;

        public bool PureEvil => _pureEvil;


        public int PageClass => _pageClass;
        public Sprite SpriteObject => _spriteObject;
        public string Clue => _clue;
        public void Interact(PlayerMovement playerInteraction)
        {
            _pageRender.PlayDialogue(_myDialogue);
        }
    }
}
