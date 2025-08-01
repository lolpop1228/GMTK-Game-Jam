using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float lineDelay = 1.5f;
    public GameObject oldDialogue;
    public GameObject newDialogue;
    public AudioSource audioSource;
    public AudioClip typingSound;

    private int index;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                // Finish current line immediately
                StopCoroutine(typingCoroutine);
                textComponent.text = lines[index];
                isTyping = false;
            }
            else
            {
                // Move to next line
                NextLine();
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;

            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);

        if (oldDialogue != null)
            oldDialogue.SetActive(false);

        if (newDialogue != null)
            newDialogue.SetActive(true);
    }

    private void OnDisable()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
