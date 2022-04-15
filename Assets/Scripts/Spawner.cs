using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] obstacles;
    private List<GameObject> obstaclesToSpawn = new List<GameObject>();

    public int objectAmount = 50;

    public float maxTime = 2.5f;
    public float minTime = 1.5f;

    public float timeChange = 0.035f;
    public float speedChange = 0.075f;

    public float obstacleSpeed = 10f;

    private bool up = false;
    private int timeUntilSwitch = 3;

    void Awake()
    {
        initObstacles();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(spawnObstacle());
    }


    private void initObstacles()
    {
        int index = 0;
        for (int i = 0; i < obstacles.Length * 50; i++)
        {
            GameObject obj = Instantiate(obstacles[index], transform.position, Quaternion.identity);
            obstaclesToSpawn.Add(obj);
            obstaclesToSpawn[i].SetActive(false);

            index++;
            if (index == obstacles.Length)
                index = 0;
        }
    }

    IEnumerator spawnObstacle()
    {

        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        int index = Random.Range(0, obstaclesToSpawn.Count);

        while (true)
        {
            if (!obstaclesToSpawn[index].activeInHierarchy)
            {
                GameObject newObstacle = obstaclesToSpawn[index];
                Obstacle obstacleInstance = newObstacle.GetComponent<Obstacle>();
                
                obstacleInstance.resetGravity();
                
                newObstacle.SetActive(true);
                newObstacle.transform.position = transform.position;
                
                if(timeUntilSwitch == 0)
                {
                    timeUntilSwitch = 3;
                    up = !up;
                }

                if(Random.value >= 0.7)
                {
                    if(!up)
                    {
                        obstacleInstance.changeGravity();
                    }
                }
                else if(up)
                {
                    obstacleInstance.changeGravity();
                }
                
                
                
                if (minTime > 0.2f)
                {
                    minTime -= timeChange;
                }
                if (maxTime > 0.4f)
                {
                    maxTime -= timeChange;
                }

                obstacleSpeed += speedChange;

                foreach (GameObject obstacle in obstaclesToSpawn)
                {
                    Obstacle script = obstacle.GetComponent<Obstacle>();
                    script.speed = obstacleSpeed * -1;
                }
                
                timeUntilSwitch--;
                break;
            } else {
                index = Random.Range(0, obstaclesToSpawn.Count);
            }
        }

        StartCoroutine(spawnObstacle());
    }
}
