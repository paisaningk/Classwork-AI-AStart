using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("City");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
