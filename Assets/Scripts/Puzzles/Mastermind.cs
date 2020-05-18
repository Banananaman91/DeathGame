using System;
using System.Collections;
using System.Text;
using DialogueTypes;
using MovementNEW;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Puzzles
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Mastermind : MonoBehaviour, IInteract
    {
        [SerializeField] private Image[] _bookImages;
        [SerializeField] private GameObject _puzzlePanel;
        [SerializeField] private Text _numberOfRight;
        [SerializeField] private DeathMovement _movement;
        [SerializeField] private RenderDialogue _pageRender;
        [SerializeField] private GameObject _rayCastObject;
        [SerializeField] private GameObject _dialogueBox;
        [SerializeField, TextArea] private string[] _textBox;
        [SerializeField, TextArea] private string[] _winText;
        [SerializeField, TextArea] private string[] _loseText;
        [SerializeField] Tilemap hiddenDoor;
        [SerializeField] private AudioClip _musicTrack;
        [SerializeField] private AudioSource _trackSource;
        [SerializeField] private int _maxBookCaseDistance;
        [SerializeField] private float _bookCaseAdjustY;
        [SerializeField] private float _sentenceSpeed;
        [SerializeField] private int _maxGuesses;
        [SerializeField] private int _amountRight;
        private Color[] _colorOrder;
        private int _book0, _book1, _book2, _book3;
        private int _bookGuess0, _bookGuess1, _bookGuess2, _bookGuess3;
        private int _howManyRight;
        private int _currentCount;
        private BoxCollider2D MyCollider => gameObject.GetComponent<BoxCollider2D>();
        private bool IsDialogueBoxNotNull => _dialogueBox != null;
        private bool IsRayCastObjectNotNull => _rayCastObject != null;
        private bool IsMovementNotNull => _movement != null;
        private bool IsPuzzlePanelNotNull => _puzzlePanel != null;
        private bool IsTrackSourceNotNull => _trackSource != null;
        private bool _bookCaseMovement;
        private float _bookCaseOriginalY; 
        
        // Use this for initialization
        private void Start()
        {
            _colorOrder = new Color[4];
            _colorOrder[0] = Color.white;
            _colorOrder[1] = Color.yellow;
            _colorOrder[2] = Color.cyan;
            _colorOrder[3] = Color.magenta;

            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(false);
            _bookCaseOriginalY = transform.position.y; //Starts the moement of the book case

            if (IsTrackSourceNotNull) _trackSource.clip = _musicTrack; // Music track plays
        }

        // Update is called once per frame
        private void Update()
        {

            if (_bookCaseMovement)
            {
                var position = transform.position;
                position = new Vector3(position.x, position.y - _bookCaseAdjustY, position.z);//Animates the book case and moves it slowly down
                transform.position = position;
                if (_bookCaseOriginalY - transform.position.y > _maxBookCaseDistance)
                    _bookCaseMovement = false;
            }

            if (_puzzlePanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) //selectedColors    compare peg to pegGuess
                {
                    _bookGuess0++;
                    if (_bookGuess0 > 3)
                    {
                        _bookGuess0 = 0;
                        _bookImages[0].color = _colorOrder[0];
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha2)) //selectedColors    compare peg to pegGuess
                {
                    _bookGuess1++;
                    if (_bookGuess1 > 3)
                    {
                        _bookGuess1 = 0;
                        _bookImages[1].color = _colorOrder[0];
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha3)) //selectedColors    compare peg to pegGuess
                {
                    _bookGuess2++;
                    if (_bookGuess2 > 3)
                    {
                        _bookGuess2 = 0;
                        _bookImages[2].color = _colorOrder[0];
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha4)) //selectedColors    compare peg to pegGuess
                {
                    _bookGuess3++;
                    if (_bookGuess3 > 3)
                    {
                        _bookGuess3 = 0;
                        _bookImages[3].color = _colorOrder[0];
                    }
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    CheckColors();
                }
                UpdateColors();
            }
        }
        
        private void CheckColors()
        {
            _howManyRight = 0;
            if (_bookGuess0 == _book0)
            {
                _howManyRight++;
            }
            if (_bookGuess1 == _book1)
            {
                _howManyRight++;
            }
            if (_bookGuess2 == _book2)
            {
                _howManyRight++;
            }
            if (_bookGuess3 == _book3)
            {
                _howManyRight++;
            }
            if (_howManyRight == _amountRight)
            {
                if (IsMovementNotNull) _movement.enabled = true;
                if (IsPuzzlePanelNotNull) _puzzlePanel.SetActive(false);
                MyCollider.enabled = true;
                _currentCount = 0;
                SetTile(hiddenDoor, gameObject.transform.position);
                StartCoroutine(RunWinCycle());
                _bookCaseMovement = true;
                if (IsTrackSourceNotNull) _trackSource.Play();//Sound PLays after the mastermind puszzle is solved
            }
            if (_currentCount == _maxGuesses)
            {
                if (IsMovementNotNull) _movement.enabled = true;
                if (IsPuzzlePanelNotNull) _puzzlePanel.SetActive(false);
                MyCollider.enabled = true;
                _currentCount = 0;
                StartCoroutine(RunLoseCycle());
            }
            _currentCount++;
            _numberOfRight.text = _howManyRight.ToString();
        }

        private void UpdateColors()
        {
            _bookImages[0].color = _colorOrder[_bookGuess0];
            _bookImages[1].color = _colorOrder[_bookGuess1];
            _bookImages[2].color = _colorOrder[_bookGuess2];
            _bookImages[3].color = _colorOrder[_bookGuess3];
        }

        private void RandomColour()
        {
            _book0 = UnityEngine.Random.Range(0, 4);
            _book1 = UnityEngine.Random.Range(0, 4);
            _book2 = UnityEngine.Random.Range(0, 4);
            _book3 = UnityEngine.Random.Range(0, 4);
        }

        private void SetColours()
        {
            _bookGuess0 = 0;
            _bookGuess1 = 0;
            _bookGuess2 = 0;
            _bookGuess3 = 0;
        }

        public void Interact(PlayerMovement playerInteraction)
        {
            SetColours();
            RandomColour();
            if (IsMovementNotNull) _movement.enabled = false;
            StartCoroutine(RunParagraphCycle());
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
            _puzzlePanel.SetActive(true);
        }

        private IEnumerator RunWinCycle()
        {
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(true);
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(false);
            int paragraphCounter = 0;
            Coroutine currentRoutine = null;
            while (paragraphCounter < _winText.Length)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(Play(_winText[paragraphCounter]));
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
        }

        private IEnumerator RunLoseCycle()
        {
            if (IsDialogueBoxNotNull) _dialogueBox.SetActive(true);
            if (IsRayCastObjectNotNull) _rayCastObject.SetActive(false);
            int paragraphCounter = 0;
            Coroutine currentRoutine = null;
            while (paragraphCounter < _loseText.Length)
            {
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(Play(_loseText[paragraphCounter]));
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
        }

        private TileBase SetTile(Tilemap tilemap, Vector2 cellWorldPos)
        {
            tilemap.SetTile(tilemap.WorldToCell(cellWorldPos),null);
            return null;
        }
    }
}

