using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    public class RenderDialogue : MonoBehaviour
    {
        [SerializeField] private Text _pageName;
        [SerializeField] private Text _pageText;
        [SerializeField] private Image _characterImage;
        [SerializeField] private Image _characterBackgroundImage;
        [SerializeField] private float _sentenceSpeed;
        [SerializeField, Tooltip("Response buttons: Ensure there are not more responses in the dialogue than this!")] private Button[] _buttons;
        [SerializeField] private Image _dialogueBackground;
        [SerializeField] private GameObject _dialogueBox;
        [SerializeField] private NpcImages _npcImages;
        [SerializeField, Tooltip("Use this to disable any game objects while dialogue is playing")] private GameObject[] _disableObjects;
        private List<Button> _responseOptions = new List<Button>();
        private NpcBio _npcImageBio;
        private Sprite _newMoodImage;
        private Coroutine _currentRoutine;
        private Dialogue _npc;
        private DialogueObject _dialogueObject;
        private Message _npcMessage;
        private int _paragraphNumber;
        private bool IsCurrentRoutineNotNull => _currentRoutine != null;
        private bool IsNewMoodImageNotNull => _newMoodImage != null;

        private string _previousMoodName;

        [HideInInspector] public List<RenderDialogue> OtherDialogues = new List<RenderDialogue>();

        private void RenderPageText(string pageName, string pageText)
        {
            _pageName.text = pageName;
            _pageText.text = pageText;
        }

        //Play the dialogue text
        private IEnumerator Play(Message npcMessage)
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
            if (_npc.Messages[_paragraphNumber].Responses.Count != 0) GetResponse(_npc.Messages[_paragraphNumber]);
            else StartCoroutine(WaitForInput());
        }

        public void PlayDialogue(Dialogue npcDialogue)
        {
            foreach (var other in OtherDialogues.Where(other => other.gameObject.activeSelf))
            {
                other.EndDialogue();
            }

            foreach (var other in _disableObjects)
            {
                if (other.activeSelf) other.SetActive(false);
            }
            _npc = npcDialogue;
            _paragraphNumber = 0;
            PlayParagraphCycle(_paragraphNumber);
        }

        //Update paragraph to be displayed
        private void PlayParagraphCycle(int next)
        {
            _paragraphNumber = next;
            //exit condition
            if (_paragraphNumber < 0)
            {
                EndDialogue();
                return;
            }

            if (_responseOptions.Count != 0)
            {
                foreach (Button buttonObject in _responseOptions)
                {
                    buttonObject.onClick.RemoveAllListeners();
                    buttonObject.GetComponentInChildren<Text>().text = "";
                    buttonObject.interactable = false;
                }
                _responseOptions.Clear();
            }

            if (_npcImages != null)
            {
                //Build list of NPC images for correct NPC
                foreach (var npcImageMoods in _npcImages.NpcImage)
                {
                    var npcImageName = npcImageMoods.NpcName.ToLower();
                    if (npcImageName == _npc.Messages[_paragraphNumber].NpcName.ToLower())
                    {
                        _npcImageBio = npcImageMoods;
                    }
                }

                if (_npcImageBio != null)
                {
                    //Set the dialogue box backgorund to the npcs required background if one exists
                    if (_npcImageBio.DialogueBackgroundImage)
                        _dialogueBackground.sprite = _npcImageBio.DialogueBackgroundImage;

                    if (_npcImageBio.CharacterBackgroundImage)
                    {
                        _characterBackgroundImage.sprite = _npcImageBio.CharacterBackgroundImage;
                        if (!_characterBackgroundImage.gameObject.activeSelf) _characterBackgroundImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        if (_characterBackgroundImage.gameObject.activeSelf) _characterBackgroundImage.gameObject.SetActive(false);
                    }

                    _pageName.color = _npcImageBio.DialogueTextColour;
                    _pageText.color = _npcImageBio.DialogueTextColour;
                    _dialogueBackground.color = _npcImageBio.DialogueBackgroundColour;

                    //Set the dialogue box font to the npcs required font if one exists
                    if (_npcImageBio.DialogueTextFont)
                    {
                        _pageName.font = _npcImageBio.DialogueTextFont;
                        _pageText.font = _npcImageBio.DialogueTextFont;
                    }

                    if (_npc.Messages[_paragraphNumber].NpcMood != "")
                    {
                        //Set NPC mood image from previous list based on current mood
                        foreach (var npcMood in _npcImageBio.NpcMoodImages)
                        {
                            var npcMoodName = npcMood.NpcMoodImage.name.ToLower();
                            if (npcMoodName.Contains(_npc.Messages[_paragraphNumber].NpcMood.ToLower()))
                            {
                                _newMoodImage = npcMood.NpcMoodImage;
                                _previousMoodName = npcMoodName;
                            }
                        }
                    }
                }

                else _newMoodImage = null;
            }
            
            if (!_dialogueBox.activeSelf) _dialogueBox.SetActive(true);

            if (IsNewMoodImageNotNull) _characterImage.sprite = _newMoodImage;
            
            if (IsCurrentRoutineNotNull) StopCoroutine(_currentRoutine);
            _pageName.text = string.Empty;
            _pageText.text = string.Empty;
            if (_npc.Messages[_paragraphNumber].TriggerEvent) _dialogueObject.MyEvent[_npc.Messages[_paragraphNumber].EventNum].Invoke();
            _currentRoutine = StartCoroutine(Play(_npc.Messages[_paragraphNumber]));
        }

        private void GetResponse(Message npcResponses)
        {
            for (var index = 0; index < npcResponses.Responses.Count; index++)
            {
                var response = npcResponses.Responses[index];
                
                foreach (var npcImageMoods in from npcImageMoods in _npcImages.NpcImage let npcImageName = npcImageMoods.NpcName.ToLower() where npcImageName.Contains(_npc.Messages[_paragraphNumber].NpcName.ToLower()) select npcImageMoods)
                {
                    if (npcImageMoods.ButtonSprite) _buttons[index].GetComponent<Image>().sprite = npcImageMoods.ButtonSprite;
                }
                _buttons[index].GetComponentInChildren<Text>().text = response.Reply;
                _responseOptions.Add(_buttons[index]);
                _buttons[index].onClick.AddListener(() => PlayParagraphCycle(response.Next));
                _buttons[index].interactable = true;
                if (response.TriggerEvent) _buttons[index].onClick.AddListener(_dialogueObject.MyEvent[response.EventNum].Invoke);
            }
        }

        public void AssignResponseObject(DialogueObject dialogueObject)
        {
            _dialogueObject = dialogueObject;
        }

        private IEnumerator WaitForInput()
        {
            if (_paragraphNumber + 1 > _npc.Messages.Count) EndDialogue();
            while (!Input.anyKey)
            {
                yield return null;
            }
            if (_paragraphNumber + 1 > _npc.Messages.Count) EndDialogue();
            else PlayParagraphCycle(_npc.Messages[_paragraphNumber].NextMessage);
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
