using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    
    public static ScoreManager Instance { get; private set; }
    
    private Highscore highscore;
    
    public TMP_Text scoreView;
    public TMP_Text scoreViewTwo;
    
    public float score { get; private set; }
    public bool newHighscore { get; private set; } = false;
    
    private void Awake()
    {
        Instance = this;
        highscore = LoadHighscoreFromJson();
    }
    
    public void increaseScore(float amount)
    {
        score += amount;
        
        scoreView.text = score.ToString();
        scoreViewTwo.text = "Score: " + score.ToString();
        
        if(score >= highscore.score)
        {
            if(!newHighscore)
                newHighscore = true;
        }
    }
    
    public void checkScore()
    {
        if(score >= highscore.score)
        {
            highscore.score = score;
            SaveHighscoreToJson();
        }
    }
    
    public bool isNewHighscore()
    {
        return newHighscore;
    }
    
    private Highscore LoadHighscoreFromJson()
    {
        var path = Application.persistentDataPath + "\\highscore.json";
        if (!File.Exists(path)) {
            return new Highscore() {
                score = 0.0f
            };
        }

        var json = File.ReadAllText(path, System.Text.Encoding.UTF8);
        return JsonUtility.FromJson<Highscore>(json);
    }
    
    private void SaveHighscoreToJson()
    {
        var path = Application.persistentDataPath + "\\highscore.json";
        File.WriteAllText(path, JsonUtility.ToJson(highscore));
    }
    
}