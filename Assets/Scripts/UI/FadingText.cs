using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadingText : MonoBehaviour
{

    private TMP_Text text;
    [SerializeField] private float speed = 0.1f;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.color = Color.Lerp(new Color(1f, 1f, 1f, 0.75f), new Color(1f, 1f, 1f, 1f), backgroundLoop(speed * Time.time));
    }
    
    private float backgroundLoop(float time) 
    {
         var v = Mathf.Repeat(time, 2);
         return time < 1 ? time : 2 - time;
    }
}
