using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class EasterEgg : MonoBehaviour {
    [SerializeField] GameObject[] raffleTickets;

    [TextArea] public string[] _textBox; // string array for inputting separate dialogue boxes

    [TextArea] public string[] _hiddenTextBox;

    [SerializeField] private RenderDialogue _pageRender;

    [SerializeField] GameObject rayCast;

    [SerializeField] GameObject dialogueBox;

    BoxCollider2D _myCollider;

    [SerializeField] GameObject jackEgg;

    public void Start()
    {
        dialogueBox.SetActive(false);

        _myCollider = GetComponent<BoxCollider2D>();

        jackEgg.SetActive(false);
    }

    public void Interact(DeathMovement playerInteraction)
    {
        int activateCount = 0;

        foreach (GameObject page in raffleTickets)
        {
            BoxCollider2D objectCollider = page.GetComponent<BoxCollider2D>();
            if (objectCollider.enabled == false) activateCount++;
        }

        if (activateCount == raffleTickets.Length)
        {
            StartCoroutine(RunHiddenParagraphCycle());
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
        jackEgg.SetActive(true);

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

        rayCast.SetActive(true);

        _myCollider.enabled = true;

    }
}
