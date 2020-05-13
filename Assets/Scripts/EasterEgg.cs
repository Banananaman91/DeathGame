using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using DialogueScripts;
[RequireComponent(typeof(BoxCollider2D))]
public class EasterEgg : MonoBehaviour, IInteract 
{
    [SerializeField] private GameObject[] _raffleTickets;
    [SerializeField, TextArea] private string[] _textBox; // string array for inputting separate dialogue boxes
    [SerializeField, TextArea] private string[] _hiddenTextBox;
    [SerializeField] private RenderDialogue _pageRender;
    [SerializeField] private GameObject _rayCastObject;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _jackEgg;
    [SerializeField] private float _sentenceSpeed;
    private BoxCollider2D MyCollider => gameObject.GetComponent<BoxCollider2D>();
    private bool IsDialogueBoxNotNull => _dialogueBox != null;
    private bool IsJackEggNotNull => _jackEgg != null;
    private bool IsPageRendererNotNull => _pageRender != null;
    private bool IsRayCastObjectNotNull => _rayCastObject != null;
    
    public void Start()
    {
        if (IsDialogueBoxNotNull) _dialogueBox.SetActive(false);
        if (IsJackEggNotNull) _jackEgg.SetActive(false);
    }
    
    public void Interact(DeathMovement playerInteraction)
    {
        int activateCount = 0;
        foreach (GameObject item in _raffleTickets)
        {
            BoxCollider2D objectCollider = item.GetComponent<BoxCollider2D>();
            if (objectCollider.enabled == false) activateCount++;
        }
        if (activateCount == _raffleTickets.Length)
        {
            StartCoroutine(RunHiddenParagraphCycle());
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
            //if (IsPageRendererNotNull) _pageRender.RenderPageText(sb.ToString());
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
    }

    private IEnumerator RunHiddenParagraphCycle()
    {
        if (IsJackEggNotNull) _jackEgg.SetActive(true);
        if (IsDialogueBoxNotNull) _dialogueBox.SetActive(true);
        if (IsRayCastObjectNotNull) _rayCastObject.SetActive(false);
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
        MyCollider.enabled = false;
        if (IsRayCastObjectNotNull) _rayCastObject.SetActive(true);
        MyCollider.enabled = true;
    }
}
