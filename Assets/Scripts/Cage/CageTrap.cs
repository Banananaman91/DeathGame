using System;
using System.Collections;
using System.Text;
using DialogueScripts;
using UnityEngine;
using UnityEngine.Video;

namespace Cage
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CageTrap : MonoBehaviour {
        [SerializeField] private DeathMovement _movement;
        [SerializeField] private Dialogue[] _pages;
        [SerializeField, TextArea] private string[] _textBox; // string array for inputting separate dialogue boxes
        [SerializeField, TextArea] private string[] _hiddenTextBox;
        [SerializeField, TextArea] private string[] _warningTextBox;
        [SerializeField] private RenderDialogue _pageRender;
        [SerializeField] private GameObject _rayCastObject;
        [SerializeField] private GameObject _dialogueBox;
        [SerializeField] private VideoPlayer _outro;
        private BoxCollider2D MyCollider => gameObject.GetComponent<BoxCollider2D>();
        [SerializeField] private PauseGame _end;
        [SerializeField] private float _sentenceSpeed;
        CircleCollider2D _myCircleCollider;
        private bool _warning;
        private bool IsMovementNotNull => _movement != null;
        private bool IsRayCastObjectNotNull => _rayCastObject != null;
        private bool IsDialogueBoxNotNull => _dialogueBox != null;
        private bool IsColliderNotNull => MyCollider != null;
        private bool IsEndPauseNotNull => _end != null;
        
        private void Start()
        {
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(false);
        }
        
        public void Interact(DeathMovement playerInteraction)
        {
            if (IsMovementNotNull) _movement.enabled = false;
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(true);
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(false);
            int activateCount = 0;
            foreach (Dialogue page in _pages)
            {
                BoxCollider2D objectCollider = page.MyCollider;
                if (objectCollider.enabled == false) activateCount++;
            }
            if (activateCount == _pages.Length)
            {
                if (_warning)
                {
                    StartCoroutine(RunHiddenParagraphCycle());       
                    //outro.Play();
                    _rayCastObject.SetActive(false);
                }
                else StartCoroutine(RunWarning());
            }
            else StartCoroutine(RunParagraphCycle());
        }
        
        private IEnumerator Play(String pageText)
        {
            var sb = new StringBuilder();
            var letters = pageText.ToCharArray();
            foreach (var letter in letters)
            {
                sb.Append(letter);
                _pageRender.RenderPageText(sb.ToString());
                yield return new WaitForSeconds(_sentenceSpeed);
            }
            yield return null;
        }
        
        private IEnumerator RunParagraphCycle()     
        {
            int paragraphCounter = 0;
            Coroutine currentRoutine = null;
            while (paragraphCounter < _textBox.Length)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(Play(_textBox[paragraphCounter]));
                ++paragraphCounter;
                yield return new WaitForSeconds(_sentenceSpeed);
                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
                yield return null;
            }
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(false);
            if (IsColliderNotNull) MyCollider.enabled = false;
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(true);
            if (IsColliderNotNull) MyCollider.enabled = true;
            if (IsMovementNotNull) _movement.enabled = true;
        }
        
        private IEnumerator RunHiddenParagraphCycle()
        {
            int paragraphCounter = 0;
            Coroutine currentRoutine = null;
            while (paragraphCounter < _hiddenTextBox.Length)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(Play(_hiddenTextBox[paragraphCounter]));
                ++paragraphCounter;
                yield return new WaitForSeconds(_sentenceSpeed);
                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
                yield return null;
            }
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(false);
            if (IsColliderNotNull) MyCollider.enabled = false;
            if (IsEndPauseNotNull) _end.PauseState = true;
        }
        
        private IEnumerator RunWarning()
        {
            int paragraphCounter = 0;
            Coroutine currentRoutine = null;
            while (paragraphCounter < _warningTextBox.Length)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(Play(_warningTextBox[paragraphCounter]));
                ++paragraphCounter;
                yield return new WaitForSeconds(_sentenceSpeed);
                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
                yield return null;
            }
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(false);
            if (IsColliderNotNull) MyCollider.enabled = false;
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(true);
            if (IsColliderNotNull) MyCollider.enabled = true;
            if (IsMovementNotNull) _movement.enabled = true;
            _warning = true;
        }
    }
}
