using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour {

    public float speed;
    public float jumpForce = 4f;

    private bool canJump;
    private Rigidbody2D rbd2;
    
    public GameObject particle;
    
    private bool sky = false;

    void Start()
    {
        canJump = true;
        rbd2 = GetComponent<Rigidbody2D>();
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
            FindObjectOfType<InGameUI>().Failed();
        }
        else if (other.gameObject.tag == "Multiplier")
        {
            FindObjectOfType<GameManager>().increaseMultiplier();
        }
    }
}
