using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool IsGamePaused = false;

    [SerializeField] GameObject pauseMenu;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        IsGamePaused = false;
    }
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        IsGamePaused = true;

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu"); // "scene du menu" entre guillemets
    }
    public void Settings()
    {
        
    }
    
}
