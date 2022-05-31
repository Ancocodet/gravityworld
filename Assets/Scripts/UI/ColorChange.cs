using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ColorChange : MonoBehaviour
{
 
    public Image image;
    public InputActionReference pointReference;
    
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    
    [SerializeField] private float speed = 0.1f;
    
    // Update is called once per frame
    void Update()
    {
        image.color = Color.Lerp(startColor, endColor, backgroundLoop(speed * Time.time));
        
        Vector2 mousePosition = pointReference.action.ReadValue<Vector2>();
        
        if(mousePosition.x <= Screen.width && mousePosition.y <= Screen.height 
            && mousePosition.x >= 0 && mousePosition.y >= 0)
        {
            GetComponent<RectTransform>().position = new Vector2(
                                (mousePosition.x / Screen.width) * -5,
                                (mousePosition.y / Screen.height) * -5
                            );
        }
    }
    
    private float backgroundLoop(float time) 
    {
         var v = Mathf.Repeat(time, 2);
         return time < 1 ? time : 2 - time;
    }
}
