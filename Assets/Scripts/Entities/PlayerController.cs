using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    
    public float xPosition = -7.5f;
    
    private float gravityScale = 5.0f; 
    private Rigidbody2D body;
    
    private bool canSwitch = false;
    private ENextAction nextAction = ENextAction.NONE;
    
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = gravityScale;
        
        transform.position = new Vector2(xPosition, (Camera.main.orthographicSize * -1f) + (1f + transform.localScale.y/2)); 
    }
    
    void Update()
    {
        if(!canSwitch) return;
        
        if(nextAction != ENextAction.NONE)
        {
            bool moved = false;
            
            if(nextAction == ENextAction.UP  && body.gravityScale > 0f)
            {
                Debug.Log("Moving Up");
                moved = true;
                body.gravityScale = gravityScale * -1f;
                body.rotation = 180f;
            }
            else if(nextAction == ENextAction.DOWN  && body.gravityScale < 0f)
            {
                Debug.Log("Moving Down");
                moved = true;
                body.gravityScale = gravityScale;
                body.rotation = 0f;
            }
            else if(nextAction == ENextAction.SWITCH)
            {
                Debug.Log("Switching");
                moved = true;
                body.gravityScale *= -1f;
                body.rotation += body.rotation == 0f ? 180f : 0f;
            }
            
            nextAction = ENextAction.NONE;
            if(moved)
                canSwitch = false;
        }
    }
    
    public void SwitchGravity()
    {
        nextAction = ENextAction.SWITCH;
    }
    
    public void MoveUp()
    {
        if(body.gravityScale > 0f)
            nextAction = ENextAction.UP;
    }
    
    public void MoveDown()
    {
        if(body.gravityScale < 0f)
            nextAction = ENextAction.DOWN;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("Collided");
            if (!canSwitch)
                canSwitch = true;
        }
    }
    
}