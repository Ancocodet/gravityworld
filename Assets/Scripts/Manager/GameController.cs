using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    [SerializeField] private float speed = 1f;
    [SerializeField] private float speedChange = 0.05f;
    
    [SerializeField] private float pointCooldown = 0.2f;
    private float currentScore = 0f;

    private UIController ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindObjectOfType<UIController>();
        StartCoroutine(updateScore());
    }

    // Update is called once per frame
    void Update()
    {
        ui.UpdateScore(currentScore);
    }
    
    IEnumerator updateScore()
    {   
        currentScore++;
        speed += speedChange;
        
        yield return new WaitForSeconds(pointCooldown);
        StartCoroutine(updateScore());
    }
    
    
    public float GetSpeed()
    {
        return speed;
    }
}
