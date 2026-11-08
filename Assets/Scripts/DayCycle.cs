using TMPro;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private TMP_Text dayLabel;
    [SerializeField] private float secondsPerDay = 60f;

    private static int day = 1;
    private static float timeLeft;
    private static bool timerStarted;

    private int lastDisplayedMinuteBlock = -1;

    private const int MinutesPerDay = 24 * 60;
    private const int GameMinutesPerStep = 5;

    private void Start()
    {
        if (!timerStarted)
        {
            day = 1;
            timeLeft = secondsPerDay;
            timerStarted = true;
        }

        UpdateLabelIfNeeded();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        while (timeLeft <= 0f)
        {
            day++;
            timeLeft += secondsPerDay;
        }

        UpdateLabelIfNeeded();
    }

    private void UpdateLabelIfNeeded()
    {
        float elapsedTime = secondsPerDay - timeLeft;

        int elapsedGameMinutes = Mathf.FloorToInt(
            elapsedTime / secondsPerDay * MinutesPerDay
        );

        int currentBlock = elapsedGameMinutes / GameMinutesPerStep;

        if (currentBlock != lastDisplayedMinuteBlock)
        {
            lastDisplayedMinuteBlock = currentBlock;
            UpdateLabel(currentBlock);
        }
    }

    private void UpdateLabel(int tenMinuteBlock)
    {
        int totalMinutes = tenMinuteBlock * GameMinutesPerStep;
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        dayLabel.SetText(
            "Day {0} · {1:00}:{2:00}",
            day,
            hours,
            minutes
        );
    }

    public static void ResetForNewGame()
    {
        day = 1;
        timeLeft = 0f;
        timerStarted = false;
    }
}