using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Maze size 25 by 25 tiles in size
    public void Play25()
    {
        Debug.Log("Load 25x25");
        SceneManager.LoadScene("25x25Scene");
    }
    
    // Maze size 50 by 50 tiles in size
    public void Play35()
    {
        Debug.Log("Load 35x35");
        SceneManager.LoadScene("35x35Scene");
    }
    
    // Temporary maze size 100 by 100 tiles in size
    public void Play50()
    {
        Debug.Log("Load 50x50");
        SceneManager.LoadScene("50x50Scene");
    }
    
    // This wont work until the game has been built
    // But this will work
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

}
