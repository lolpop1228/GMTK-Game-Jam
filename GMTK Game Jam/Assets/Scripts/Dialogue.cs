using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue")]
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float lineDelay = 1.5f;

    [Header("Choice Panel")]
    public GameObject choicesPanel;
    public string[] choice1ResponseLines;
    public string[] choice2ResponseLines;
    public string[] choice3ResponseLines;

    [Header("Transition")]
    public GameObject oldDialogue;
    public GameObject newDialogue;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip typingSound;

    [Header("Player Control")]
    public MouseLook mouseLookScript; // <-- Assign your MouseLook script here
    public PlayerMovement playerMovement;
    public NormalNPC npc;

    private int index;
    private bool isTyping = false;
    private bool isChoiceResponse = false;
    private Coroutine typingCoroutine;

    private string[] currentResponseLines = null;
    private int responseIndex = 0;
    private bool isInResponse = false;

    void OnEnable()
    {
        // Find MouseLook once at start instead of every OnEnable
        if (mouseLookScript == null)
            mouseLookScript = FindAnyObjectByType<MouseLook>();
        if (playerMovement == null)
            playerMovement = FindAnyObjectByType<PlayerMovement>();
        if (npc != null)
        {
            npc = FindAnyObjectByType<NormalNPC>();
        }

        InitializeDialogue();
        // Reset all state when dialogue is enabled again
        ResetDialogueState();
        
        if (mouseLookScript != null)
            mouseLookScript.enabled = false;
        if (playerMovement != null)
            playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // Stop current typing and show complete text
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                    typingCoroutine = null;
                }

                if (isInResponse)
                    textComponent.text = currentResponseLines[responseIndex];
                else
                    textComponent.text = lines[index];

                isTyping = false;
            }
            else
            {
                if (isInResponse)
                    NextResponseLine();
                else if (!isChoiceResponse)
                    NextLine();
            }
        }
    }

    void InitializeDialogue()
    {
        if (choicesPanel != null)
            choicesPanel.SetActive(false);

        textComponent.text = string.Empty;
        StartDialogue();
    }

    void ResetDialogueState()
    {
        // Clean up any running coroutines
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        // Reset all state variables
        index = 0;
        isTyping = false;
        isChoiceResponse = false;
        isInResponse = false;
        responseIndex = 0;
        currentResponseLines = null;

        // Reset UI
        if (choicesPanel != null)
            choicesPanel.SetActive(false);
        
        textComponent.text = string.Empty;
        
        // Start fresh dialogue
        StartDialogue();
    }

    public void StartDialogue()
    {
        index = 0;
        typingCoroutine = StartCoroutine(TypeLine(lines[index]));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c;

            if (typingSound != null && audioSource != null)
                audioSource.PlayOneShot(typingSound);

            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        typingCoroutine = null; // Clear reference when done
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine(lines[index]));
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        if (choicesPanel != null)
            choicesPanel.SetActive(true);

        if (oldDialogue != null)
            oldDialogue.SetActive(false);

        if (newDialogue != null)
            newDialogue.SetActive(true);
    }

    public void Choice1Answer()
    {
        HandleChoice(choice1ResponseLines);
    }

    public void Choice2Answer()
    {
        HandleChoice(choice2ResponseLines);
    }

    public void Choice3Answer()
    {
        HandleChoice(choice3ResponseLines);
    }

    void HandleChoice(string[] responseLines)
    {
        if (choicesPanel != null)
            choicesPanel.SetActive(false);

        isChoiceResponse = true;
        isInResponse = true;
        currentResponseLines = responseLines;
        responseIndex = 0;
        typingCoroutine = StartCoroutine(TypeLine(currentResponseLines[responseIndex]));
    }

    void NextResponseLine()
    {
        if (responseIndex < currentResponseLines.Length - 1)
        {
            responseIndex++;
            typingCoroutine = StartCoroutine(TypeLine(currentResponseLines[responseIndex]));
        }
        else
        {
            EndResponseDialogue();
        }
    }

    void EndResponseDialogue()
    {
        // Reset choice-related state
        isChoiceResponse = false;
        isInResponse = false;
        currentResponseLines = null;
        responseIndex = 0;

        gameObject.SetActive(false);

        if (oldDialogue != null)
            oldDialogue.SetActive(false);

        if (newDialogue != null)
            newDialogue.SetActive(true);

        if (mouseLookScript != null)
            mouseLookScript.enabled = true;
        if (playerMovement != null)
            playerMovement.enabled = true;

        if (npc != null)
        {
            npc.WalkToSecondDestination();
        }
    }

    private void OnDisable()
    {
        // Clean up when dialogue is disabled
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        if (audioSource != null)
            audioSource.Stop();
        
        Cursor.lockState = CursorLockMode.Locked;
    }
}