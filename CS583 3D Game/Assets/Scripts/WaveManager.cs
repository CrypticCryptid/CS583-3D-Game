using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float startTimer;
    float timer;

    public WaveTemplate[] templates;
    public Transform[] spawnPoints;
    
    WaveTemplate currTemp;

    void Start()
    {
        currTemp = templates[0];

        timer = startTimer;
    }

    void Update()
    {
        if(timer <= 0f)
        {
            //spawn wave
            SpawnEnemies();
            timer = startTimer;
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < currTemp.enemiesAmount; i++)
        {
            int ranPos = Random.Range(0, spawnPoints.Length);

            Instantiate(PickEnemy(), spawnPoints[ranPos].position, Quaternion.identity);
        }
    }

    public GameObject PickEnemy()
    {
        int ranNum = Random.Range(1, 21);

        if (ranNum >= currTemp.bounds.z)
        {
            //drop super rare enemy
            int ranEnemy = Random.Range(0, currTemp.srEnemies.Length);
            return currTemp.srEnemies[ranEnemy];
        }
        else if (ranNum >= currTemp.bounds.y && ranNum < currTemp.bounds.z)
        {
            //drop rare enemy
            int ranEnemy = Random.Range(0, currTemp.rEnemies.Length);
            return currTemp.rEnemies[ranEnemy];
        }
        else if (ranNum >= currTemp.bounds.x && ranNum < currTemp.bounds.y)
        {
            //drop uncommon enemy
            int ranEnemy = Random.Range(0, currTemp.uEnemies.Length);
            return currTemp.uEnemies[ranEnemy];
        }
        else if (ranNum < currTemp.bounds.x)
        {
            //drop common enemy
            int ranEnemy = Random.Range(0, currTemp.cEnemies.Length);
            return currTemp.cEnemies[ranEnemy];
        }

        return currTemp.cEnemies[0];
    }
}
