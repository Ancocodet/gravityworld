using UnityEngine;

public class TestObstacle : MonoBehaviour
{
    
    private Vector2 targetPosition; 
    private bool shouldMove = false;
    
    private float speed = 7.5f;
    
    void Awake()
    {
        targetPosition = transform.position;
    }
    
    void OnDisable()
    {
        shouldMove = false;
    }
    
    void FixedUpdate()
    {
        if(!shouldMove) return;
        
        Vector2 current = transform.position;
        if(Vector2.Distance(targetPosition, current) > .1f)
        {
            current.x -= speed * Time.deltaTime;
            transform.position = current;
        }
        else if(Vector2.Distance(targetPosition, current) > 0f)
        {   
            transform.position = targetPosition;
        }
    }
    
    public void MoveForward(float offset)
    {
        shouldMove = true;
        Vector2 current = transform.position;
        current.x = Mathf.Round(current.x - offset);
        targetPosition = current;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {   
            transform.gameObject.SetActive(false);
        }
    }
    
}