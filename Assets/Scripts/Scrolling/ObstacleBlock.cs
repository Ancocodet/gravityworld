using UnityEngine;

public class ObstacleBlock : MonoBehaviour
{
    
    public bool top = false;
    public bool right = false;
    
    void Awake()
    {
        float x = right ? (Camera.main.orthographicSize * 2) + transform.localScale.x/2 : (Camera.main.orthographicSize * -2f) - transform.localScale.x/2;
        float y = top ? Camera.main.orthographicSize - (transform.localScale.y/2) : (Camera.main.orthographicSize * -1f) + (transform.localScale.y/2);
        transform.position = new Vector2(x, y);
    }
    
}