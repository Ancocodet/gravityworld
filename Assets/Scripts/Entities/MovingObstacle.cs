﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{

    private Rigidbody2D myRB;
    private GameManager gameManager;

    public bool elevation = false;
    
    public float rotationSpeed = 0.2f;
    public bool rotation = false;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }
    

    void Update()
    {
        if(gameManager.gameState != EGameState.PLAYING) return;
        if(rotation)
        {
            myRB.rotation += rotationSpeed;
        }
    }    
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(elevation)
        {
            if (other.gameObject.tag == "NewGround")
            {
                Rigidbody2D body = GetComponent<Rigidbody2D>();
                body.gravityScale = body.gravityScale * -1;
            }
        }
    }
}