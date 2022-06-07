using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool IsGamePaused = false;

    [SerializeField] GameObject pauseMenu;
    public GameObject player;
    public GameObject weapons;
    

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
        player.GetComponent<PlayerController>().enabled = true;
        weapons.GetComponent<WeaponSwitching>().enabled = true;
        foreach (var x in weapons.GetComponentsInChildren<GunRayCast>())
        {
            x.enabled = true;
        }
        IsGamePaused = false;
    }
    void PauseGame()
    {
        pauseMenu.SetActive(true);
        player.GetComponent<PlayerController>().enabled = false;
        weapons.GetComponent<WeaponSwitching>().enabled = false;
        foreach ( var x in weapons.GetComponentsInChildren<GunRayCast>())
        {
            x.enabled = false;
        }

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
