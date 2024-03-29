﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour {

    public float speed;
    public float jumpForce = 4f;

    private bool canJump;
    private Rigidbody2D rbd2;
    
    private InGameUI ui;
    private GameManager manager;
    
    public GameObject particle;
    
    private bool sky = false;
    
    private bool switchDone = true;
    private BufferAction bufferedAction = BufferAction.NONE;

    void Start()
    {
        canJump = true;
        rbd2 = GetComponent<Rigidbody2D>();
        
        manager = FindObjectOfType<GameManager>();
        ui = FindObjectOfType<InGameUI>();
    }

    void Update()
    {
        if(manager.gameState != EGameState.PLAYING) return;
        
        if(canJump && bufferedAction != BufferAction.NONE)
        {
            if(bufferedAction == BufferAction.UP && !sky)
            {
                moveUp();
            }
            else if(bufferedAction == BufferAction.DOWN && sky)
            {
                moveDown();
            }
            else
            {
                switchGravity();
            }
            switchDone = true;
            bufferedAction = BufferAction.NONE;
        }
    }
        
    public void moveUp()
    {
        if(!sky && canJump)
        {
            particle.SetActive(false);
            canJump = false;
            rbd2.gravityScale = rbd2.gravityScale * -1;
            rbd2.rotation = 180f;
            sky = true;
        }
        else if(!sky && !canJump)
        {
            bufferedAction = BufferAction.UP;
        }
    }
    
    public void moveDown()
    {
        if(sky && canJump)
        {
            particle.SetActive(false);
            canJump = false;
            rbd2.gravityScale = rbd2.gravityScale * -1;
            rbd2.rotation = 0f;
            sky = false;
        }
        else if(sky && !canJump)
        {
            bufferedAction = BufferAction.DOWN;
        }
    }
    
    public void switchGravity()
    {
        if(!switchDone) return;
        switchDone = false;
        if(sky && canJump)
        {
            moveDown();
            bufferedAction = BufferAction.NONE;
        }
        else if(!sky && canJump)
        {
            moveUp();
            bufferedAction = BufferAction.NONE;
        }
        else if(!canJump)
        {
            if(sky && bufferedAction == BufferAction.NONE)
                bufferedAction = BufferAction.DOWN;
            if(!sky && bufferedAction == BufferAction.NONE)
                            bufferedAction = BufferAction.UP;
        }
        StartCoroutine(ReAllowSwitch());
        Debug.Log("Input Buffer: " + bufferedAction);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        particle.SetActive(false);
        
        if (other.gameObject.tag == "NewGround")
        {
            if (!canJump)
                canJump = true;
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            if (!canJump)
                canJump = true;
        }
        
        particle.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            ui.Failed();
        }
        else if (other.gameObject.tag == "Multiplier")
        {
            manager.increaseMultiplier();
        }
    }
    
    IEnumerator ReAllowSwitch()
    {
        yield return new WaitForSeconds(0.15f);
        if(!switchDone)
            switchDone = true;
    }
}
