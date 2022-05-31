using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject ingame;
    [SerializeField] private GameObject paused;
    [SerializeField] private GameObject failed;

    [SerializeField] private PlayerInput playerInput;

    private EGameState gameState = EGameState.PLAYING;

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
}
