using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.Video;

[RequireComponent(typeof(BoxCollider2D))]
public class CageTrap : MonoBehaviour {
    [SerializeField] DeathMovement movement;

    [SerializeField] GameObject[] pages;

    CircleCollider2D myCollider;

    [TextArea] public string[] _textBox; // string array for inputting separate dialogue boxes

    [TextArea] public string[] _hiddenTextBox;

    [TextArea] public string[] _warningTextBox;

    [SerializeField] private RenderDialogue _pageRender;

    [SerializeField] GameObject rayCast;

    [SerializeField] GameObject dialogueBox;

    [SerializeField] VideoPlayer outro;

    BoxCollider2D _myCollider;

    private bool _warning = false;

    public PauseGame end;

    public void Start()
    {
        dialogueBox.SetActive(false);

        _myCollider = GetComponent<BoxCollider2D>();
    }

    public void Interact(DeathMovement playerInteraction)
    {
        int activateCount = 0;

        foreach (GameObject page in pages)
        {
            BoxCollider2D objectCollider = page.GetComponent<BoxCollider2D>();
            if (objectCollider.enabled == false) activateCount++;
        }

        if (activateCount == pages.Length)
        {
            if (_warning == true)
            {
                StartCoroutine(RunHiddenParagraphCycle());       
                //outro.Play();
                rayCast.SetActive(false);

            }
            else StartCoroutine(RunWarning());
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
        dialogueBox.SetActive(true);

        rayCast.SetActive(false);

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

        dialogueBox.SetActive(false);

        _myCollider.enabled = false;

        rayCast.SetActive(true);

        _myCollider.enabled = true;
            
        
    }

    public IEnumerator RunHiddenParagraphCycle()
    {
        movement.enabled = false;

        dialogueBox.SetActive(true);

        rayCast.SetActive(false);

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

        dialogueBox.SetActive(false);

        _myCollider.enabled = false;

        end._pauseState = true;
    }

    public IEnumerator RunWarning()
    {
        movement.enabled = false;

        dialogueBox.SetActive(true);

        rayCast.SetActive(false);

        int paragraphCounter = 0;

        Coroutine currentRoutine = null;

        while (paragraphCounter < _warningTextBox.Length)
        {
            if (currentRoutine != null) StopCoroutine(currentRoutine);

            currentRoutine = StartCoroutine(Play(_warningTextBox[paragraphCounter]));

            ++paragraphCounter;

            yield return new WaitForSeconds(0.0375f);

            while (!Input.GetKeyDown(KeyCode.E))
            {
                yield return null;
            }

            yield return null;
        }

        if (currentRoutine != null) StopCoroutine(currentRoutine);

        dialogueBox.SetActive(false);

        _myCollider.enabled = false;

        rayCast.SetActive(true);

        _myCollider.enabled = true;

        movement.enabled = true;

        _warning = true;
    }
}
