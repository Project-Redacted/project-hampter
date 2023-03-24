using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          if (GameIsPaused == true)
          {
            Resume();
          } 
          else
            {
                Pause();
            }

           


            

        }
    }

    public void Resume ()
    {
      pauseMenuUI.SetActive(false);
      Time.timeScale = 1f;
      GameIsPaused = false;
      Cursor.lockState = CursorLockMode.Locked;
    }
    void Pause ()
    {
      pauseMenuUI.SetActive(true);
      Time.timeScale = 0f;
      GameIsPaused = true;
      Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
