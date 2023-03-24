using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
  // Scene changer
  public void Difficulty()
  {
    SceneManager.LoadScene("TempDiffScene");
  }
  public void MainMenu()
  {
    SceneManager.LoadScene("TempMainMenu");
  }
  public void Settings()
  {
    SceneManager.LoadScene("Settings");
  }
    public void Easy()
    {
        SceneManager.LoadScene("25x25Scene");
        Time.timeScale = 1f;
    }
    public void Medium()
    {
        SceneManager.LoadScene("35x35Scene");
        Time.timeScale = 1f;
    }
    public void Hard()
    {
        SceneManager.LoadScene("50x50Scene");
        Time.timeScale = 1f;
    }


    // Buttons
    public void ExitGame()
  {
    Application.Quit();
  }

}
