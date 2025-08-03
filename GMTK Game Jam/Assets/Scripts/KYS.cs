using System.Collections;
using TMPro;
using UnityEngine;

public class KYS : MonoBehaviour
{
    public TextMeshProUGUI messageText1; // Assign in Inspector
    public TextMeshProUGUI messageText2; // Assign in Inspector
    public TextMeshProUGUI messageText3; // Assign in Inspector
    public ActivateDoor activateDoor;

    public string displayMessage1 = "Goodbye...";
    public string displayMessage2 = "You have been deleted.";
    public string displayMessage3 = "Farewell, cruel world.";

    public float fadeDuration = 1.5f;
    public float displayDuration = 1f;

    private bool isDying = false;

    void Start()
    {
        activateDoor = FindAnyObjectByType<ActivateDoor>();
    }

    public void Die()
    {
        if (isDying) return;
        isDying = true;

        SetupText(messageText1, displayMessage1);
        SetupText(messageText2, displayMessage2);
        SetupText(messageText3, displayMessage3);
        activateDoor.DoorActivate();

        if (messageText1 || messageText2 || messageText3)
            StartCoroutine(FadeAndDestroy());
        else
            Destroy(gameObject);
    }

    private void SetupText(TextMeshProUGUI text, string message)
    {
        if (text != null)
        {
            text.text = message;
            text.gameObject.SetActive(true);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        }
    }

    private IEnumerator FadeAndDestroy()
    {
        yield return StartCoroutine(FadeText(0f, 1f, fadeDuration)); // Fade in
        yield return new WaitForSeconds(displayDuration);            // Hold
        yield return StartCoroutine(FadeText(1f, 0f, fadeDuration)); // Fade out

        if (messageText1 != null) messageText1.gameObject.SetActive(false);
        if (messageText2 != null) messageText2.gameObject.SetActive(false);
        if (messageText3 != null) messageText3.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        Color c1 = messageText1 != null ? messageText1.color : Color.white;
        Color c2 = messageText2 != null ? messageText2.color : Color.white;
        Color c3 = messageText3 != null ? messageText3.color : Color.white;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);

            if (messageText1 != null)
                messageText1.color = new Color(c1.r, c1.g, c1.b, alpha);

            if (messageText2 != null)
                messageText2.color = new Color(c2.r, c2.g, c2.b, alpha);

            if (messageText3 != null)
                messageText3.color = new Color(c3.r, c3.g, c3.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (messageText1 != null)
            messageText1.color = new Color(c1.r, c1.g, c1.b, endAlpha);

        if (messageText2 != null)
            messageText2.color = new Color(c2.r, c2.g, c2.b, endAlpha);

        if (messageText3 != null)
            messageText3.color = new Color(c3.r, c3.g, c3.b, endAlpha);
    }
}
