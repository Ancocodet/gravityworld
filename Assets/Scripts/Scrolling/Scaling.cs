using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour {

    public bool scaleHeight = true;
    public bool scaleWidth = true;
    
    public float offsetHeight = 0f;
    public float offsetWidth = 0f;

    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
      
        transform.localScale = new Vector3(scaleWidth ? width - offsetWidth : 1f, scaleHeight ? height - offsetHeight : 1f, 0);
    }

}
