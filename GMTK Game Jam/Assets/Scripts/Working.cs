using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Working : MonoBehaviour, IInteractable
{
    public GameObject npcSpawnTrigger;
    public TextMeshProUGUI messageText; // Assign this in the inspector
    public string displayMessage = "Working...";
    public float fadeDuration = 1.5f;   // Duration for fade-in and fade-out
    public float displayDuration = 1f;  // How long to stay visible before fading out

    private bool hasInteracted = false;

    void Start()
    {
        if (npcSpawnTrigger != null)
            npcSpawnTrigger.SetActive(false);

        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
            messageText.color = new Color(messageText.color.r, messageText.color.g, messageText.color.b, 0f); // Start transparent
        }
    }

    public void Interact()
    {
        if (hasInteracted) return;

        if (npcSpawnTrigger != null)
            npcSpawnTrigger.SetActive(true);
        
        gameObject.layer = LayerMask.NameToLayer("Default");

        if (messageText != null)
        {
            messageText.text = displayMessage;
            messageText.gameObject.SetActive(true);
            StartCoroutine(FadeTextSequence());
        }

        hasInteracted = true;
    }

    private IEnumerator FadeTextSequence()
    {
        yield return StartCoroutine(FadeText(0f, 1f, fadeDuration)); // Fade in
        yield return new WaitForSeconds(displayDuration);            // Wait
        yield return StartCoroutine(FadeText(1f, 0f, fadeDuration)); // Fade out
        messageText.gameObject.SetActive(false);
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = messageText.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            messageText.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        messageText.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
