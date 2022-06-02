using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject topGround;
    [SerializeField] private GameObject bottomGround;

    [SerializeField] private GameObject top;
    [SerializeField] private GameObject bottom;

    [SerializeField] private GameObject[] obstaclePrefabs;
    private List<GameObject> obstacles = new List<GameObject>();

    public float minTime = 0.75f;
    public float maxTime = 2.0f;
    public float timeChange = 0.05f;

    private bool nextTop = false;

    // Start is called before the first frame update
    void Awake()
    {   
        InitParts();
        transform.position = new Vector2( (Camera.main.orthographicSize * 2f) + (transform.localScale.x * 10f), 0f);
        top.transform.position = new Vector2(
            transform.position.x,
            topGround.transform.position.y - (topGround.transform.localScale.y + 0.02f)
        );
        bottom.transform.position = new Vector2(
            transform.position.x,
            bottomGround.transform.position.y + (bottomGround.transform.localScale.y + 0.02f)
        );
    }

    void Start()
    {
        StartCoroutine(SpawnPart());
    }

    private void InitParts()
    {
        int index = 0;
        for (int i = 0; i < obstaclePrefabs.Length * 50; i++)
        {
            GameObject obj = Instantiate(obstaclePrefabs[index], transform.position, Quaternion.identity);
            obj.SetActive(false);
            obstacles.Add(obj);

            index++;
            if (index == obstaclePrefabs.Length)
                index = 0;
        }
    }

    IEnumerator SpawnPart()
    {
        int index = Random.Range(0, obstacles.Count);
        
        while (true)
        {
            if (!obstacles[index].activeInHierarchy)
            {
                GameObject newObstacle = obstacles[index];
                Obstacle obstacleInstance = newObstacle.GetComponent<Obstacle>();
                
                newObstacle.SetActive(true);
                if(nextTop)
                {
                    newObstacle.transform.position = top.transform.position;
                    newObstacle.transform.rotation = new Quaternion(0f, 0f, 180f, 1);
                    nextTop = false;
                }
                else
                {
                    newObstacle.transform.position = bottom.transform.position;
                    newObstacle.transform.rotation = new Quaternion(0f, 0f, 0f, 1);
                    nextTop = true;
                }
                
                if (minTime > 0.2f)
                {
                    minTime -= timeChange;
                }
                if (maxTime > 0.4f)
                {
                    maxTime -= timeChange;
                }

                break;
            } else {
                index = Random.Range(0, obstacles.Count);
            }
        }
        
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        StartCoroutine(SpawnPart());
    }
}
