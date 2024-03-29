﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    public bool sky = false;
    public bool moving = true;

    private Vector2 offset = Vector2.zero;
    private Material mat;
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        mat = GetComponent<Renderer>().material;
        offset = mat.GetTextureOffset("_MainTex");

        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        
        transform.localScale = new Vector3(width, 1, 0);
        
        transform.position = new Vector2(
            0f,
            (sky ? Camera.main.orthographicSize - (transform.localScale.y/2) : (Camera.main.orthographicSize * -1f) + (transform.localScale.y/2)) 
        );
    }

    void Update()
    {
        offset.x += gameController.GetSpeed() * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", offset);
    }
}
