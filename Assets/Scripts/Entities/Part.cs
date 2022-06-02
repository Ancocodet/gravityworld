using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour {

    public float speed = -10f;
    private Rigidbody2D myRB;
   
    public GameObject self;
    public GameObject[] obstacles;
    
    private GameManager gameManager;

    void Start() 
    {
        myRB = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    void Update() 
    {   
        myRB.velocity = new Vector2(speed, 0f);
        if(!hasActiveObstacles())
        {
            self.SetActive(false);
        }
    }
    
    bool hasActiveObstacles()
    {
        int active = 0;
        foreach(GameObject obs in this.obstacles) 
        {
            if(obs.activeSelf)
            {
                active++;
            }
        }
        return active > 0;
    }
    
    public void updateSpeed(float speed) 
    {
        this.speed = speed;
        foreach(GameObject obs in this.obstacles) {
            Obstacle obstacle = obs.GetComponent<Obstacle>();
            // obstacle.speed = speed;
        }
    }
}