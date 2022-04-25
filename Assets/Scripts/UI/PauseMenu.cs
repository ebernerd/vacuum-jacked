using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
  public static bool GamePaused = false;
  public GameObject pauseMenuUI;
  
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Debug.Log("Paused");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Scenes/StartScene");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
