using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] topPrefabs;
    [SerializeField] private GameObject[] botPrefabs;
    
    private float minX;
    
    private List<GameObject> topObstacles = new List<GameObject>();
    private List<GameObject> botObstacles = new List<GameObject>();
    
    private float topY;
    private float botY;
    
    private int topPlaced = 0;
    private int botPlaced = 0;
    
    void Awake()
    {   
        topY = (Camera.main.orthographicSize * -1f);
        botY = (Camera.main.orthographicSize);
        
        Vector3 right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f));
        Vector3 left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0f, 0f));
        
        minX = left.x-2f;
        transform.position = new Vector3(right.x + 1f, 0f, 0f);
        
        InitObstacles();
        SpawnStart();
    }
    
    private void SpawnStart()
    {
        for(float x = - (Camera.main.orthographicSize - 1); x < transform.position.x; x += 1f)
        {
            SpawnNewObstacle(x);
        }
    }
    
    public void Move(float offset)
    {
        foreach(GameObject obstacle in topObstacles)
        {
            if (!obstacle.activeInHierarchy) continue;
            
            if(obstacle.transform.position.x <= minX)
            {
                obstacle.SetActive(false);
                continue;
            }
            
            obstacle.GetComponent<TestObstacle>().MoveForward(offset);
        }
        
        foreach(GameObject obstacle in this.botObstacles)
        {
            if (!obstacle.activeInHierarchy) continue;
            
            if(obstacle.transform.position.x <= minX)
            {
                obstacle.SetActive(false);
                continue;
            }
            
            obstacle.GetComponent<TestObstacle>().MoveForward(offset);
        }
        
        SpawnNewObstacle(transform.position.x + 1f);
    }
    
    private void SpawnNewObstacle(float x)
    {
        if(NoSpawn()) return;
                    
        float random = Random.Range(0f, .9f);
        
        if(topPlaced >= 3 || (random < .5f && botPlaced < 3))
        {
            SpawnObstacle(new Vector3(x, botY, 0f));
            topPlaced = 0; botPlaced++;
        }
        else if(botPlaced >= 3 || (random >= .5f && topPlaced < 3))
        {
            SpawnObstacle(new Vector3(x, topY, 0f));
            topPlaced++; botPlaced = 0;
        }
    }
    
    private bool NoSpawn()
    {
        float random = Random.Range(0f, .9f);
        return random > .4f && random < .6f;
    }
    
    private void SpawnObstacle(Vector3 position)
    {
        List<GameObject> obstacles = botObstacles;
        if(position.y > 0f)
            obstacles = topObstacles;
            
        int index = Random.Range(0, obstacles.Count);
        while (true)
        {
            if (!obstacles[index].activeInHierarchy)
            {
                GameObject newObstacle = obstacles[index];
                
                newObstacle.SetActive(true);
                if(position.y > 0)
                {
                    position.y -= ((1f + newObstacle.transform.localScale.y/2) - 0.1f);
                    newObstacle.transform.position = position;
                }
                else
                {
                    position.y += ((1f + newObstacle.transform.localScale.y/2) - 0.1f);
                    newObstacle.transform.position = position;
                }
                
                break;
            } else {
                index = Random.Range(0, obstacles.Count);
            }
        }
    }
    
    private void InitObstacles()
    {
        int index = 0;
        for (int i = 0; i < topPrefabs.Length * 25; i++)
        {
            GameObject obj = Instantiate(topPrefabs[index], transform.position, Quaternion.identity);
            
            obj.transform.Rotate(new Vector3(0f, 0f, 180f));
            obj.SetActive(false);
            
            
            topObstacles.Add(obj);

            index++;
            if (index == topPrefabs.Length)
                index = 0;
        }
        
        index = 0;
        for (int i = 0; i < botPrefabs.Length * 25; i++)
        {
            GameObject obj = Instantiate(botPrefabs[index], transform.position, Quaternion.identity);
            obj.SetActive(false);
            botObstacles.Add(obj);

            index++;
            if (index == botPrefabs.Length)
                index = 0;
        }
    }
}