using System.Collections;
using MovementNEW;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzles
{
    public class Mastermind : DialogueObject, IInteract
    {
        [SerializeField] private Image[] _bookImages;
        [SerializeField] private GameObject _puzzlePanel;
        [SerializeField] private Text _numberOfRight;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private int _maxGuesses;
        [SerializeField] Color[] _colorOrder = { Color.white, Color.yellow, Color.cyan, Color.magenta };
        private int[] _book;
        private int[] _bookGuess;
        private int _howManyRight;
        private int _currentCount;
        private bool IsMovementNotNull => _movement != null;
        private bool IsPuzzlePanelNotNull => _puzzlePanel != null;

        // Use this for initialization
        private void OnValidate()
        {
            if (_book == null || _book.Length != _bookImages.Length) _book = new int[_bookImages.Length];
            if (_bookGuess == null || _bookGuess.Length != _bookImages.Length) _bookGuess = new int[_bookImages.Length];
            
        }

        public void IncreaseColour(int num)
        {
            if (_bookGuess[num] == _colorOrder.Length - 1) _bookGuess[num] = 0;
            else _bookGuess[num]++;
            _bookImages[num].color = _colorOrder[num];
            UpdateColors();
        }

        public void DecreaseColour(int num)
        {
            if (_bookGuess[num] == 0) _bookGuess[num] = _colorOrder.Length - 1;
            else _bookGuess[num]--;
            _bookImages[num].color = _colorOrder[num];
            UpdateColors();
        }
        
        public void CheckColors()
        {
            _howManyRight = 0;

            for (var i = 0; i < _bookGuess.Length; i++)
            {
                if (_bookGuess[i] == _book[i]) _howManyRight++;
            }
            
            if (_howManyRight == _bookImages.Length)
            {
                if (IsMovementNotNull) _movement.enabled = true;
                if (IsPuzzlePanelNotNull) _puzzlePanel.SetActive(false);
                _currentCount = 0;
                StartCoroutine(RunWinCycle());
            }
            if (_currentCount == _maxGuesses)
            {
                if (IsMovementNotNull) _movement.enabled = true;
                if (IsPuzzlePanelNotNull) _puzzlePanel.SetActive(false);
                _currentCount = 0;
                StartCoroutine(RunLoseCycle());
            }
            _currentCount++;
            _numberOfRight.text = "Correct: " + _howManyRight;
        }

        public void StartMastermind()
        {
            RandomColour();
            SetColours();
            _currentCount = 0;
            _puzzlePanel.SetActive(true);
        }

        private void UpdateColors()
        {
            for (var i = 0; i < _bookImages.Length; i++)
            {
                _bookImages[i].color = _colorOrder[_bookGuess[i]];
            }
        }

        private void RandomColour()
        {
            for (var i = 0; i < _book.Length; i++)
            {
                _book[i] = Random.Range(0, 4);
            }
        }

        private void SetColours()
        {
            for (int i = 0; i < _bookGuess.Length; i++)
            {
                _bookGuess[i] = 0;
            }
        }

        public void Interact(PlayerMovement playerInteraction)
        {
            _pageRender.PlayDialogue(_myDialogue);
        }

        private IEnumerator RunWinCycle()
        {
            _myEvent[1].Invoke();
            yield return null;
        }

        private IEnumerator RunLoseCycle()
        {
            _puzzlePanel.SetActive(false);
            yield return null;
        }
        
        public void SwapDialogue(Dialogue dialogueObject)
        {
            _myDialogue = dialogueObject;
        }
    }
}

