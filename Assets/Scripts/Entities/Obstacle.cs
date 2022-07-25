using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private Rigidbody2D myRB;
    private GameController gameController;

    void Start()
    {  
        gameController = FindObjectOfType<GameController>();
        myRB = GetComponent<Rigidbody2D>();
    }
    

    void Update()
    {
        Vector2 current = transform.position;
        current.x -= gameController.GetSpeed() * Time.deltaTime;
        transform.position = current;
    }
    

    public void changeGravity() {}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collector")
        {
            transform.gameObject.SetActive(false);
        }
        else if (other.tag == "Player" && gameObject.tag == "Multiplier")
        {
            transform.gameObject.SetActive(false);
        }
    }
}