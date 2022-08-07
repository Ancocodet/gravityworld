using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class TestManager : MonoBehaviour
{

    [SerializeField] private float minDistance = .2f;
    [SerializeField] private float maxTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = .9f;
    
    private Vector2 swipeStartPosition;
    private float swipeStartTime;

    public float score = -1f;

    [SerializeField] private Ground[] grounds;
    [SerializeField] private InputManager inputManager;
    
    [SerializeField] private UIController uiController;
    [SerializeField] private TestSpawner testSpawner;
    [SerializeField] private TestPlayerController testPlayer;
    
    private bool moved = true;
    private Vector2 currentPosition = Vector2.down;
    
    void OnEnable()
    {
        inputManager.OnTouchStart += StartSwipe;
        inputManager.OnTouchCancel += EndSwipe;
        
        testPlayer.OnQuarterReached +=  MoveObstacles;
        testPlayer.OnObstacleCollision +=  PlayerDied;
        
        testPlayer.OnGroundReached +=  GivePoint;
    }
    
    void OnDisable()
    {
        inputManager.OnTouchStart -= StartSwipe;
        inputManager.OnTouchCancel -= EndSwipe;
        
        testPlayer.OnQuarterReached -=  MoveObstacles;
        testPlayer.OnObstacleCollision -=  PlayerDied;
        
        testPlayer.OnGroundReached -=  GivePoint;
    }
    
    public void GivePoint()
    {
        score += 1f;
        uiController.UpdateScore(Mathf.Max(score, 0f));
    }
    
    public void PlayerDied()
    {
        inputManager.DisableGestures();
        uiController.Failed();
    }
    
    private void MoveObstacles()
    {
        if(!moved)
        {
            moved = true;
            testSpawner.Move(1f);
            foreach(Ground ground in this.grounds)
            {
                ground.Move(1f);
            }
        }
    }
    
    public void moveForward()
    {
        moved = !testPlayer.Move(currentPosition);
        Debug.Log(moved);
    }
    
    public void MoveUp()
    {
        if(!testPlayer.CanMove())
            return;
                    
        if(currentPosition != Vector2.up)
            currentPosition = Vector2.up;
            
        moveForward();
    }
    
    public void MoveDown()
    {
        if(!testPlayer.CanMove())
            return;
            
        if(currentPosition != Vector2.down)
            currentPosition = Vector2.down;
            
        moveForward();
    }
    
    public void StartSwipe(Vector2 position, float time)
    {
        swipeStartPosition = position;
        swipeStartTime = time;
    }   
    
    public void EndSwipe(Vector2 position, float time)
    {
        if(Vector3.Distance(swipeStartPosition, position) >= minDistance &&
            (time - swipeStartTime) <= maxTime){
                Vector3 direction = position - swipeStartPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                if(Vector2.Dot(Vector2.up, direction2D) > directionThreshold)
                {
                    MoveUp();
                }
                else if(Vector2.Dot(Vector2.down, direction2D) > directionThreshold)
                {
                    MoveDown();
                }
            }
    }   
}
