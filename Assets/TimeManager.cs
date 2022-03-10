using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private bool timerIsRunning = false;
    [SerializeField] private float timeRemaining = 20;
    [SerializeField] private TextMeshProUGUI TimeText;
    public bool IsTimeRanOut;

    void Update()
    {

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                TimeRanOut();
            }
        }
    }
    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay);
        TimeText.text = $"Time Remaining : {seconds} Sec";
    }

    public void TimeRanOut()
    {
        IsTimeRanOut = true;
    }
}
