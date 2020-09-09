using System;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzles
{
    public class KeycodeLock : MonoBehaviour
    {
        [SerializeField, Tooltip("Code solutions: Controls size of next two arrays")] private int[] _codes;
        [SerializeField] private Text[] _codeDisplay;
        [SerializeField] private int[] _currentCode;
        [SerializeField] private GameObject _display;
        [SerializeField] private DialogueObject _dialogueObject;
        [SerializeField] private int _dialogueEvent;

        private void OnValidate()
        {
            if (_codeDisplay.Length != _codes.Length) _codeDisplay = new Text[_codes.Length];
            if (_currentCode.Length != _codes.Length) _currentCode = new int[_codes.Length];
        }

        public void LockStart()
        {
            for (var i = 0; i < _currentCode.Length; i++)
            {
                _currentCode[i] = 0;
                _codeDisplay[i].text = _currentCode[i].ToString();
            }
        
            if (!_display.activeSelf) _display.SetActive(true);
        }

        public void Increase(int code)
        {
            if (_currentCode[code] == 9) _currentCode[code] = 0;
            else _currentCode[code]++;
            _codeDisplay[code].text = _currentCode[code].ToString();
        }

        public void Decrease(int code)
        {
            if (_currentCode[code] == 0) _currentCode[code] = 9;
            else _currentCode[code]--;
            _codeDisplay[code].text = _currentCode[code].ToString();
        }

        public void CheckPuzzle()
        {
            var count = 0;
            for (int i = 0; i < _codeDisplay.Length; i++)
            {
                if (_currentCode[i] == _codes[i]) count++;
            }
            if (count == _codes.Length) PlayWin();
        }

        private void PlayWin()
        {
            _dialogueObject.MyEvent[_dialogueEvent].Invoke();
        }

        public void EndPuzzle()
        {
            if (_display.activeSelf) _display.SetActive(false);
        }
    }
}
