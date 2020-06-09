﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueTypes
{
    public class RenderDialogue : MonoBehaviour
    {
        [SerializeField] private Text _pageName;
        [SerializeField] private Text _pageText;
        [SerializeField] private GameObject _pageImagePosition;
        [SerializeField] private float _sentenceSpeed;
        [SerializeField] private Button _option;
        [SerializeField] private GameObject[] _buttonPositions;
        [SerializeField] private Image _dialogueBackground;
        [SerializeField] private GameObject _dialogueBox;
        private List<Button> _responseOptions = new List<Button>();
        private Image _previousImage;
        private NpcMoods _npcImageMoods;
        private Image _newMoodImage;
        private Coroutine _currentRoutine;
        private bool IsPreviousImageNotNull => _previousImage != null;
        private bool IsCurrentRoutineNotNull => _currentRoutine != null;
        private bool IsNewMoodImageNotNull => _newMoodImage != null;

        private void RenderPageText(string pageName, string pageText)
        {
            _pageName.text = pageName;
            _pageText.text = pageText;
        }

        //Play the dialogue text
        private IEnumerator Play(Dialogue npc, Message npcMessage, NpcImages npcImages, int paragraphNumber, DialogueObject dialogueObject)
        {
            var sb = new StringBuilder();
            var letters = npcMessage.MessageText.ToCharArray();
            foreach (var letter in letters)
            {
                sb.Append(letter);
                RenderPageText(npcMessage.NpcName, sb.ToString());
                yield return new WaitForSeconds(_sentenceSpeed);
            }
            yield return new WaitForSeconds(_sentenceSpeed);
            if (npc.Messages[paragraphNumber].Responses.Count != 0) GetResponse(npc, npc.Messages[paragraphNumber], npcImages, dialogueObject);
            else StartCoroutine(WaitForInput(npc, npcImages, paragraphNumber, dialogueObject));
        }

        //Update paragraph to be displayed
        public void PlayParagraphCycle(Dialogue npcDialogue, NpcImages npcImages, int paragraphNumber, DialogueObject dialogueObject)
        {
            //exit condition
            if (paragraphNumber < 0)
            {
                EndDialogue();
                return;
            }

            if (_responseOptions.Count != 0)
            {
                foreach (Button buttonObject in _responseOptions)
                {
                    Destroy(buttonObject.gameObject);
                }
                _responseOptions.Clear();
            }

            if (npcImages != null)
            {
                //Build list of NPC images for correct NPC
                foreach (var npcImageMoods in npcImages.NpcImage)
                {
                    var npcImageName = npcImageMoods.NpcName.ToLower();
                    if (npcImageName.Contains(npcDialogue.Messages[paragraphNumber].NpcName.ToLower()))
                    {
                        _npcImageMoods = npcImageMoods;
                    }
                }

                //Set NPC mood image from previous list based on current mood
                foreach (var npcMood in _npcImageMoods.NpcMoodImages)
                {
                    var npcMoodName = npcMood.name.ToLower();
                    if (npcMoodName.Contains(npcDialogue.Messages[paragraphNumber].NpcMood.ToLower()))
                    {
                        _newMoodImage = npcMood;
                    }
                }
            }
            
            if (!_dialogueBox.activeSelf) _dialogueBox.SetActive(true);
            
            if (IsPreviousImageNotNull) Destroy(_previousImage.gameObject);
            
            if (IsNewMoodImageNotNull)
            {
                var newImage = Instantiate(_newMoodImage, _pageImagePosition.transform);
                newImage.transform.SetParent(_dialogueBackground.transform);
                _previousImage = newImage;
            }

            if (IsCurrentRoutineNotNull) StopCoroutine(_currentRoutine);
            _pageName.text = string.Empty;
            _pageText.text = string.Empty;

            _currentRoutine = StartCoroutine(Play(npcDialogue, npcDialogue.Messages[paragraphNumber], npcImages, paragraphNumber, dialogueObject));

        }

        private void GetResponse(Dialogue npcMessage, Message npcResponses, NpcImages npcImages, DialogueObject dialogueObject)
        {
            for (var index = 0; index < npcResponses.Responses.Count; index++)
            {
                var response = npcResponses.Responses[index];
                var button = Instantiate(_option, _buttonPositions[index].transform.position,
                    _buttonPositions[index].transform.rotation);
                button.transform.SetParent(_dialogueBackground.transform);
                button.GetComponentInChildren<Text>().text = response.Reply;
                _responseOptions.Add(button);
                button.onClick.AddListener(() => PlayParagraphCycle(npcMessage, npcImages, response.Next, dialogueObject));
                if (response.TriggerEvent) button.onClick.AddListener(dialogueObject.ResponseTrigger);
            }
        }

        private IEnumerator WaitForInput(Dialogue npcDialogue, NpcImages npcImages, int paragraphNumber, DialogueObject dialogueObject)
        {
            if (paragraphNumber + 1 > npcDialogue.Messages.Count) EndDialogue();
            while (!Input.anyKey)
            {
                yield return null;
            }
            if (paragraphNumber + 1 > npcDialogue.Messages.Count) EndDialogue();
            else PlayParagraphCycle(npcDialogue, npcImages, npcDialogue.Messages[paragraphNumber].NextMessage, dialogueObject);
        }

        private void EndDialogue()
        {
            if (IsCurrentRoutineNotNull) StopCoroutine(_currentRoutine);
            _pageName.text = string.Empty;
            _pageText.text = string.Empty;
            _dialogueBox.SetActive(false);
        }
    }
}
