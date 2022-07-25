using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour {

    public float speed = 1f;

    private Vector2 offset = Vector2.zero;
    private Material mat;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        mat = GetComponent<Renderer>().material;
        offset = mat.GetTextureOffset("_MainTex");

        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        if (transform.tag == "Background")
        {
            transform.localScale = new Vector3(width, height, 0);
        }
        else if (transform.tag == "NewGround")
        {
            transform.localScale = new Vector3(width * 2.5f, 2, 0);
        }
        else if (transform.tag == "Sides")
        {
            transform.localScale = new Vector3(height * 0.7f, 2, 0);
        }
        else
        {
            transform.localScale = new Vector3(width, 1, 0);
        }
    }

    void Update()
    {
        offset.x += speed * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", offset);
    }
}
