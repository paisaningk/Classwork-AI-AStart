using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject Pause;
    [SerializeField] private GameObject Cam;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Player Player;
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI CurrentFood;
    [SerializeField] private GameObject Win;
    [SerializeField] private TextMeshProUGUI WinScore;
    private bool isPause = false;
    

    public void Restart()
    {
        SceneManager.LoadScene("City");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == false)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }

        if (timeManager.IsTimeRanOut)
        {
            Time.timeScale = 0;
            Win.SetActive(true);
            WinScore.text = $"Your Score :{Player.Score}";
        }
        Score.text = $"Score : {Player.Score}";
        CurrentFood.text = $"Food Remain : {Player.CurrentFood}";
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Pause.SetActive(true);
        isPause = true;
        Cam.SetActive(false);
    }
    
    public void UnPauseGame()
    {
        Time.timeScale = 1;
        Pause.SetActive(false);
        isPause = false;
        Cam.SetActive(true);
    }
}
