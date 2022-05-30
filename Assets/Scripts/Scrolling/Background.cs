using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public float speed = 1f;
    public bool moving = true;

    private Vector2 offset = Vector2.zero;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = mat.GetTextureOffset("_MainTex");

        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
      
        transform.localScale = new Vector3(width, height - 2f, 0);
    }

    void Update()
    {
        if(moving)
        {
            offset.x += speed * Time.deltaTime;
            mat.SetTextureOffset("_MainTex", offset);
        }
    }
}
