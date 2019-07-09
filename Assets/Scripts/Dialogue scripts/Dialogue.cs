using System;
using System.Collections;
using System.Text;
using UnityEngine;

namespace Dialogue_scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Dialogue : Interacted
    {
        [TextArea] [SerializeField] private string[] _textBox; // string array for inputting separate dialogue boxes
        [TextArea] [SerializeField] private string[] _hiddenTextBox;
        [SerializeField] private RenderDialogue _pageRender;
        [SerializeField] private GameObject _rayCast;
        [SerializeField] private GameObject _dialogueBox;
        [SerializeField] private JournalInventoryScript _joInvScript;
        [SerializeField] private GameObject _accessItem;
        [SerializeField] private GameObject _hiddenItem;
        [SerializeField] private GameObject _item;
        [SerializeField] private DeathMovement _movement;
        [SerializeField] private int _itemClass;
        [SerializeField] public int pageClass;
        private BoxCollider2D _myCollider;
        private SpriteRenderer _myRenderer;
        [SerializeField] private bool isIntro;


        private void Start()
        {
            _dialogueBox.SetActive(false);

            _myCollider = GetComponent<BoxCollider2D>();

            _myRenderer = gameObject.GetComponent<SpriteRenderer>();

            if (_hiddenItem != null) _hiddenItem.SetActive(false);

            if (_item != null) _item.SetActive(false);
        }



        public void Interact(DeathMovement playerInteraction)
        {
            if (_movement != null) _movement.enabled = false;
            if (_accessItem != null)
            {
                if (_accessItem.activeSelf == false) StartCoroutine(RunParagraphCycle());

                else StartCoroutine(RunHiddenParagraphCycle());
            }

            else if (_item != null)
            {
                if (_item.activeSelf == true) StartCoroutine(RunHiddenParagraphCycle());

                else StartCoroutine(RunParagraphCycle());
            }

            else StartCoroutine(RunParagraphCycle());
        }

        public IEnumerator Play(String pageText)
        {
            var sb = new StringBuilder();

            var letters = pageText.ToCharArray();

            foreach (var letter in letters)
            {
                sb.Append(letter);

                _pageRender.RenderPageText(sb.ToString());

                yield return new WaitForSeconds(0.0375f);
            }

       
            yield return null;
        }

        public IEnumerator RunParagraphCycle()
        {
            _dialogueBox.SetActive(true);

            _rayCast.SetActive(false);

            int paragraphCounter = 0;

            Coroutine currentRoutine = null;

            while (paragraphCounter < _textBox.Length)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);

                currentRoutine = StartCoroutine(Play(_textBox[paragraphCounter]));

                ++paragraphCounter;

                yield return new WaitForSeconds(0.0375f);

                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }

                yield return null;
            }

            if (currentRoutine != null) StopCoroutine(currentRoutine);

            _dialogueBox.SetActive(false);

            if (_itemClass >= 1)
            {
                _joInvScript.AddPage(gameObject);

                _myRenderer.enabled = false;
            }

            _myCollider.enabled = false;

            _rayCast.SetActive(true);

            if (_itemClass == 0)
            {
                _myCollider.enabled = true;
                if (_item != null) _item.SetActive(true);
                if (isIntro == true)
                {
                    _myRenderer.enabled = false;
                    _myCollider.enabled = false;
                }
            }
            if (_movement != null) _movement.enabled = true;
        
        }

        public IEnumerator RunHiddenParagraphCycle()
        {
            _dialogueBox.SetActive(true);

            _rayCast.SetActive(false);

            int paragraphCounter = 0;

            Coroutine currentRoutine = null;

            while (paragraphCounter < _hiddenTextBox.Length)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);

                currentRoutine = StartCoroutine(Play(_hiddenTextBox[paragraphCounter]));

                ++paragraphCounter;

                yield return new WaitForSeconds(0.0375f);

                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }

                yield return null;
            }

            if (currentRoutine != null) StopCoroutine(currentRoutine);

            _dialogueBox.SetActive(false);

            _myCollider.enabled = false;

            _rayCast.SetActive(true);

            _myCollider.enabled = true;

            if (_hiddenItem != null) _hiddenItem.SetActive(true);

            if (_accessItem != null) _accessItem = null;

            if (_movement != null) _movement.enabled = true;
        }
    }
}
