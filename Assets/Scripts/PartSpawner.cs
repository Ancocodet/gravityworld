using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    
    public GameObject[] parts;
    private List<GameObject> partsToSpawn = new List<GameObject>();

    public int objectAmount = 50;

    public float maxTime = 2.5f;
    public float minTime = 1.5f;

    public float timeChange = 0.035f;
    public float speedChange = 0.075f;

    public float moveSpeed = 10f;
    
    private GameManager gameManager;

    void Awake()
    {
        initParts();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        StartCoroutine(spawnPart());
    }


    private void initParts()
    {
        int index = 0;
        for (int i = 0; i < parts.Length * 50; i++)
        {
            GameObject obj = Instantiate(parts[index], transform.position, Quaternion.identity);
            partsToSpawn.Add(obj);
            partsToSpawn[i].SetActive(false);

            index++;
            if (index == parts.Length)
                index = 0;
        }
    }

    IEnumerator spawnPart()
    {

        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        
        if(gameManager.gameState == EGameState.PLAYING)
        {
            int index = Random.Range(0, partsToSpawn.Count);
            
            while (true)
            {
                if (!partsToSpawn[index].activeInHierarchy)
                {
                    GameObject newObstacle = partsToSpawn[index];
                    Obstacle obstacleInstance = newObstacle.GetComponent<Obstacle>();
                    
                    obstacleInstance.resetGravity();
                    
                    newObstacle.SetActive(true);
                    newObstacle.transform.position = transform.position;
                    
                    if (minTime > 0.2f)
                    {
                        minTime -= timeChange;
                    }
                    if (maxTime > 0.4f)
                    {
                        maxTime -= timeChange;
                    }
    
                    moveSpeed += speedChange;
    
                    foreach (GameObject part in partsToSpawn)
                    {
                        Part script = part.GetComponent<Part>();
                        script.updateSpeed(moveSpeed * -1);
                    }
                    break;
                } else {
                    index = Random.Range(0, partsToSpawn.Count);
                }
            }
        }

        StartCoroutine(spawnPart());
    }
    
}