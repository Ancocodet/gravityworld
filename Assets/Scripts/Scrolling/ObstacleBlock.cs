using UnityEngine;

public class ObstacleBlock : MonoBehaviour
{
    
    [SerializeField] private bool right = false;
    [SerializeField] private GameObject parentObject;
    
    void Awake()
    {
        float x = right ? (Camera.main.orthographicSize * 2) + transform.localScale.x/2 : (Camera.main.orthographicSize * -2f) - transform.localScale.x/2;
        float y = parentObject.transform.position.y;
        transform.position = new Vector2(x, y);
    }
    
}