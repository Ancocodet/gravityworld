using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class MainMenu : MonoBehaviour {

    public static PlayGamesPlatform platform;

    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate((result) =>{
                // TODO do something
            });
        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void PlayGameNew()
    {
        SceneManager.LoadScene(2);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
	
}
