using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float speed = -10f;
    private Rigidbody2D myRB;

    private GameManager gameManager;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }
    
    public void changeGravity()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
    
        body.gravityScale = body.gravityScale * -1;
        body.rotation = 180f;
    }
    
    public void resetGravity()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        
        if(body.gravityScale <= 0)
        {
            body.gravityScale = body.gravityScale * -1;
        }
        
        if(body.rotation > 0f)
        {
            body.rotation = 0f;
        }
         
    }

    void Update()
    {
        if(gameManager.gameState != EGameState.PLAYING) return;
        myRB.velocity = new Vector2(speed, 0f);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collector")
        {
            transform.gameObject.SetActive(false);
            resetGravity();
        }
        else if (other.tag == "Player" && gameObject.tag == "Multiplier")
        {
            transform.gameObject.SetActive(false);
            resetGravity();
        }
    }
}