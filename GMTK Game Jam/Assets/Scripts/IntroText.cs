using UnityEngine;

public class AppearAfterDelay : MonoBehaviour
{
    public float delayBeforeShowing = 2f; // Time before showing
    public float showDuration = 5f;       // Time to stay visible
    public GameObject gameObject;

    void Start()
    {
        gameObject.SetActive(false);  // Start hidden
        StartCoroutine(ShowTemporarily());
    }

    private System.Collections.IEnumerator ShowTemporarily()
    {
        yield return new WaitForSeconds(delayBeforeShowing);
        gameObject.SetActive(true);  // Show after delay

        yield return new WaitForSeconds(showDuration);
        gameObject.SetActive(false); // Hide again permanently
    }
}
