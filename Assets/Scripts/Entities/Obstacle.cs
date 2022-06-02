using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float speed = -10f;
    private Rigidbody2D myRB;


    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    

    void Update()
    {
        myRB.velocity = new Vector2(speed, 0f);
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