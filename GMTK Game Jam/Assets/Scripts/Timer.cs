using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign in Inspector
    public int totalTimeInSeconds = 300; // 5 minutes
    public GameObject Keys;
    public string sceneToLoad;

    private float remainingTime;
    private bool isRunning = false;

    void Start()
    {
        timerText.text = ""; // Hide text at the beginning
        if (Keys != null)
        {
            Keys.SetActive(false);
        }
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            isRunning = false;
            SceneManager.LoadScene(sceneToLoad);
        }

        UpdateTimerDisplay();
    }

    public void TimerStart()
    {
        remainingTime = totalTimeInSeconds;
        isRunning = true;
        if (Keys != null)
        {
            Keys.SetActive(true);
        }
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
