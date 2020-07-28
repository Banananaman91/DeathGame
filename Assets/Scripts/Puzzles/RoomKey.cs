using System;
using System.Collections;
using System.Text;
using MovementNEW;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Puzzles
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RoomKey : MonoBehaviour, IInteract {

        [SerializeField] private GameObject _key;
        [SerializeField] private Tilemap _hiddenDoor;
        [SerializeField] private DeathMovement _movement;
        [SerializeField] private RenderDialogue _pageRender;
        [SerializeField] private GameObject _rayCastObject;
        [SerializeField] private GameObject _dialogueBox;
        [SerializeField, TextArea] private string[] _textBox;
        [SerializeField, TextArea] private string[] _unlockText;
        [SerializeField] private AudioClip _mTrack;
        [SerializeField] private AudioSource _trackSource;
        [SerializeField] private float _sentenceSpeed;
        [SerializeField] private int _maxBookCaseDistance;
        [SerializeField] private float _bookCaseAdjustY;
        private BoxCollider2D MyCollider => GetComponent<BoxCollider2D>();
        private bool IsMovementNotNull => _movement != null;
        private bool IsKeyNotNull => _key != null;
        private bool IsDialogueBoxNotNull => _dialogueBox != null;
        private bool IsRayCastObjectNotNull => _rayCastObject != null;
        private bool IsTrackSourceNotNull => _trackSource != null;
        private bool _bookCaseMovement; //Book Case Movement/any other object moving
        private float _bookCaseStartY;      //Starting possition of the book case
        
        private void Start()
        {
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(false);
            _bookCaseStartY = transform.position.y; //Starts the moement of the book case
            if (IsTrackSourceNotNull) _trackSource.clip = _mTrack;
        }

        private void Update()
        {
            if (_bookCaseMovement)
            {
                var position = transform.position;
                position = new Vector3(position.x, position.y + _bookCaseAdjustY, position.z);//Animates the book case and moves it slowly down
                transform.position = position;
                if (_bookCaseStartY - transform.position.y < -_maxBookCaseDistance) _bookCaseMovement = false;
            }
        }

        public void Interact(PlayerMovement playerInteraction)
        {
            if (IsMovementNotNull) _movement.enabled = false;
            if (IsKeyNotNull)
            {
                if (_key.activeSelf == false)
                {
                    StartCoroutine(RunParagraphCycle());
                }

                else
                {
                    StartCoroutine(RunUnlockCycle());
                }
            }
            else
            {
                StartCoroutine(RunParagraphCycle());
            }
        }

        private TileBase SetTile(Tilemap tilemap, Vector2 cellWorldPos)
        {
            tilemap.SetTile(tilemap.WorldToCell(cellWorldPos), null);
            return null;
        }

        private IEnumerator Play(String pageText)
        {
            var sb = new StringBuilder();
            var letters = pageText.ToCharArray();
            foreach (var letter in letters)
            {
                sb.Append(letter);
                //_pageRender.RenderPageText(sb.ToString());
                yield return new WaitForSeconds(_sentenceSpeed);
            }
            yield return null;
        }

        private IEnumerator RunParagraphCycle()
        {
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(true);
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(false);
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
            MyCollider.enabled = false;
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(true);
            MyCollider.enabled = true;
            if (IsMovementNotNull) _movement.enabled = true;
        }

        private IEnumerator RunUnlockCycle()
        {
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(true);
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(false);
            int paragraphCounter = 0;
            Coroutine currentRoutine = null;
            while (paragraphCounter < _unlockText.Length)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(Play(_unlockText[paragraphCounter]));
                ++paragraphCounter;
                yield return new WaitForSeconds(_sentenceSpeed);
                while (!Input.GetKeyDown(KeyCode.E))
                {
                    yield return null;
                }
                yield return null;
            }
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            SetTile(_hiddenDoor, gameObject.transform.position);
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(false);
            MyCollider.enabled = false;
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(true);
            MyCollider.enabled = true;
            if (IsMovementNotNull) _movement.enabled = true;
            _bookCaseMovement = true;
            if (IsTrackSourceNotNull) _trackSource.Play();
        }
    }
}
