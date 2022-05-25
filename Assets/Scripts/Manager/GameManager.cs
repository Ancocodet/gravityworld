using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    public GameObject player;
    public GameObject spawner;

    public TMP_Text multiplierView;

    public float multiplier {get; private set; } = 1;
    public float multiplierDecrease = 2f;
    
    public float pointCooldown = 0.2f;

    void Start () {
        StartCoroutine(updateScore());
	}
	
	void Update () {
        multiplierView.text = "x " + multiplier.ToString();
	}

    public void increaseMultiplier()
    {
        multiplier += 1f;
        StartCoroutine(decreaseMultiplier());
    }

    IEnumerator updateScore()
    {
        yield return new WaitForSeconds(pointCooldown);
        ScoreManager.Instance.increaseScore(multiplier);
        StartCoroutine(updateScore());
    }
    
    IEnumerator decreaseMultiplier()
    {
        yield return new WaitForSeconds(multiplierDecrease);
        if(multiplier > 0f)
            multiplier -= 1f;
    }
}
