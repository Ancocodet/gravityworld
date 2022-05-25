using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InGameUI : MonoBehaviour {

    public static bool IsPaused = false;
    public static bool IsFailure = false;

    public PlayerInput playerInput;
    public EventSystem eventSystem;

    public GameObject scoreBlock;
    public GameObject highScore;
    
    public GameObject failureMenu;
    public GameObject pauseMenu;
    
    public GameObject resumeButton;
    public GameObject restartButton;
    
    public string menuScene = "Menu";
    
    public void switchPause()
    {
        if (!IsFailure)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if(IsPaused && !IsFailure)
        {
            IsPaused = false;
            
            pauseMenu.SetActive(false);
            playerInput.SwitchCurrentActionMap("Player");
            Time.timeScale = 1f;
            scoreBlock.SetActive(true);
        }
    }

    public void Pause()
    {
        if(!IsPaused && !IsFailure)
        {
            IsPaused = true;
            Time.timeScale = 0f;
            
            playerInput.SwitchCurrentActionMap("UI");
            eventSystem.SetSelectedGameObject(resumeButton);
            
            scoreBlock.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void Failed()
    {   
        IsFailure = true;
        IsPaused = true;
        
        playerInput.SwitchCurrentActionMap("UI");
        
        eventSystem.SetSelectedGameObject(restartButton);
                       
        scoreBlock.SetActive(false);
        failureMenu.SetActive(true);
        
        if(ScoreManager.Instance.isNewHighscore())
        {
            ScoreManager.Instance.checkScore();
            highScore.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(menuScene);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
