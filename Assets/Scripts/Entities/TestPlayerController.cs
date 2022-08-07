using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    public delegate void GroundReached();
    public event GroundReached OnGroundReached;
    
    public delegate void QuarterReached();
    public event QuarterReached OnQuarterReached;
    
    public delegate void ObstacleCollision();
    public event ObstacleCollision OnObstacleCollision;
    
    [SerializeField] private float switchTimeout = 0.25f;
    private float lastSwitch = 0.0f;
    
    [SerializeField] private float gravityScale = 5f;

    private Rigidbody2D playerBody;
    private Vector2 currentPosition = Vector2.down;
    
    private bool IsOnGround = true;
    
    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerBody.gravityScale = gravityScale * (currentPosition.y * -1f);
        
        transform.position = new Vector2(-Camera.main.orthographicSize, (Camera.main.orthographicSize * -1f) + (1f + transform.localScale.y/2));
    }
    
    void FixedUpdate()
    {
        if(transform.position.y >= Camera.main.orthographicSize/-2 
            && currentPosition == Vector2.up){
            if(OnQuarterReached != null)
                OnQuarterReached();
        }
        else if(transform.position.y <= Camera.main.orthographicSize/2 
             && currentPosition == Vector2.down){
             if(OnQuarterReached != null)
                OnQuarterReached();
         }
    
        Debug.Log(transform.eulerAngles);
    
        if(transform.position.y > 0
            && transform.eulerAngles.z < 180f)
        {
            transform.Rotate(new Vector3(0f, 180f, 180f));
        }
        else if(transform.position.y < 0 
            && transform.eulerAngles.z > 0f)
        {
            transform.Rotate(new Vector3(0f, -180f, -180f));
        }    
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Obstacle")
        {
            IsOnGround = true;
            if(OnGroundReached != null)
                OnGroundReached();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if(OnObstacleCollision != null)
                OnObstacleCollision();
        }
    }
    
    public bool CanMove()
    {
        return !(!IsOnGround || lastSwitch + switchTimeout >= Time.time);
    }
    
    public bool Move(Vector2 direction)
    {
        if(!IsOnGround || lastSwitch + switchTimeout >= Time.time) 
            return false;
            
        lastSwitch = Time.time;
        
        if(currentPosition != direction)
        {
            IsOnGround = false;
                    
            currentPosition = direction;
            playerBody.gravityScale = gravityScale * (currentPosition.y * -5f);
            
            return true;
        }
        
        if(OnGroundReached != null)
            OnGroundReached();
        return true;
    }

}