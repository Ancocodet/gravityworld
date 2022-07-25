using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject ingame;
    [SerializeField] private GameObject paused;
    [SerializeField] private GameObject failed;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private TMP_Text score;

    private EGameState gameState = EGameState.PLAYING;

    public void UpdateScore(float currentScore)
    {
        score.text = currentScore.ToString();
    }

    public void SwitchPause()
    {
        if(gameState == EGameState.FAILED) return;

        if(gameState == EGameState.PLAYING)
        {
            Time.timeScale = 0.0f;
            playerInput.SwitchCurrentActionMap("UI");

            gameState = EGameState.PAUSED;

            ingame.SetActive(false);
            paused.SetActive(true);
        }
        else if(gameState == EGameState.PAUSED)
        {
            Time.timeScale = 1.0f;
            playerInput.SwitchCurrentActionMap("Player");

            gameState = EGameState.PLAYING;

            ingame.SetActive(true);
            paused.SetActive(false);
        }
    }
    
    public void Failed()
    {
        if(gameState == EGameState.FAILED) return;
        
        Time.timeScale = 0.0f;
        playerInput.SwitchCurrentActionMap("UI");
        
        gameState = EGameState.FAILED;
        
        ingame.SetActive(false);
        failed.SetActive(true);
    }
    
    
    public void LoadMenu()
    {
        gameState = EGameState.PLAYING;
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        gameState = EGameState.PLAYING;
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
