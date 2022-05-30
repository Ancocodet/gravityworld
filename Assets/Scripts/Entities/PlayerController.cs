using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    
    public float xPosition = -7.5f;
    
    void Awake()
    {
        transform.position = new Vector2(xPosition, (Camera.main.orthographicSize * -1f) + (1f + transform.localScale.y/2)); 
    }
    
}