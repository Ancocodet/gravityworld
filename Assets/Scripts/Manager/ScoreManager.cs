using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    public static ScoreManager Instance { get; private set; }
    
    public TMP_Text scoreView;
    public TMP_Text scoreViewTwo;
    
    public float Score { get; private set; }
    public bool newHighscore { get; private set; } = false;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void increaseScore(float amount)
    {
        Score += amount;
        
        scoreView.text = Score.ToString();
        scoreViewTwo.text = "Score: " + Score.ToString();
        
        if(Score >= getHighScore())
        {
            if(!newHighscore)
                newHighscore = true;
            saveScore();
        }
    }
    
    
    private void saveScore()
    {
        PlayerPrefs.SetFloat("highScore", Score);
        PlayerPrefs.Save();
    }
    
    public bool isNewHighScore()
    {
        return newHighscore;
    }
    
    public float getHighScore()
    {
        return PlayerPrefs.GetFloat("highScore");
    }
    
    
}