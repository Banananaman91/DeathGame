using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
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
        [SerializeField] private NpcImages _npcImages;
        private List<Button> _responseOptions = new List<Button>();
        private Image _previousImage;
        private NpcBio _npcImageBio;
        private Image _newMoodImage;
        private Coroutine _currentRoutine;
        private Dialogue _npc;
        private DialogueObject _dialogueObject;
        private Message _npcMessage;
        private int _paragraphNumber;

        private bool IsPreviousImageNotNull => _previousImage != null;
        private bool IsCurrentRoutineNotNull => _currentRoutine != null;
        private bool IsNewMoodImageNotNull => _newMoodImage != null;

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
                    Destroy(buttonObject.gameObject);
                }
                _responseOptions.Clear();
            }

            if (_npcImages != null)
            {
                //Build list of NPC images for correct NPC
                foreach (var npcImageMoods in _npcImages.NpcImage)
                {
                    var npcImageName = npcImageMoods.NpcName.ToLower();
                    if (npcImageName.Contains(_npc.Messages[_paragraphNumber].NpcName.ToLower()))
                    {
                        _npcImageBio = npcImageMoods;
                    }
                }

                if (_npcImageBio != null)
                {
                    //Set the dialogue box backgorund to the npcs required background if one exists
                    if (_npcImageBio.DialogueBackgroundImage) _dialogueBackground.sprite = _npcImageBio.DialogueBackgroundImage;
 
                    _pageName.color = _npcImageBio.DialogueTextColour;
                    _pageText.color = _npcImageBio.DialogueTextColour;
                    _dialogueBackground.color = _npcImageBio.DialogueBackgroundColour;
                    
                    //Set the dialogue box font to the npcs required font if one exists
                    if (_npcImageBio.DialogueTextFont)
                    {
                        _pageName.font = _npcImageBio.DialogueTextFont;
                        _pageText.font = _npcImageBio.DialogueTextFont;
                    }

                    //Set NPC mood image from previous list based on current mood
                    foreach (var npcMood in _npcImageBio.NpcMoodImages)
                    {
                        var npcMoodName = npcMood.NpcMoodImage.name.ToLower();
                        if (npcMoodName.Contains(_npc.Messages[_paragraphNumber].NpcMood.ToLower()))
                        {
                            _newMoodImage = npcMood.NpcMoodImage;
                        }
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
            if (_npc.Messages[_paragraphNumber].TriggerEvent) _dialogueObject.MyEvent[_npc.Messages[_paragraphNumber].EventNum].Invoke();
            _currentRoutine = StartCoroutine(Play(_npc.Messages[_paragraphNumber]));
        }

        private void GetResponse(Message npcResponses)
        {
            for (var index = 0; index < npcResponses.Responses.Count; index++)
            {
                var response = npcResponses.Responses[index];
                var button = Instantiate(_option, _buttonPositions[index].transform.position,
                    _buttonPositions[index].transform.rotation);
                button.transform.SetParent(_dialogueBackground.transform);
                button.GetComponentInChildren<Text>().text = response.Reply;
                _responseOptions.Add(button);
                button.onClick.AddListener(() => PlayParagraphCycle(response.Next));
                if (response.TriggerEvent) button.onClick.AddListener(_dialogueObject.MyEvent[response.EventNum].Invoke);
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
