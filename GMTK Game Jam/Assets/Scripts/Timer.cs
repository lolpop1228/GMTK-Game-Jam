using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign this in the Inspector
    public int totalTimeInSeconds = 300; // 5 minutes = 300 seconds

    private float remainingTime;
    private bool isRunning = true;

    void Start()
    {
        TimerStart();
    }

    public void TimerStart()
    {
        remainingTime = totalTimeInSeconds;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            isRunning = false;
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
