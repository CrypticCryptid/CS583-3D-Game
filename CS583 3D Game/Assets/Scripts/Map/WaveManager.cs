using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float startTimer;
    float timer;

    public WaveTemplate[] templates; //pair indexes with waveIncrements
    public Transform[] spawnPoints;

    private List<GameObject> curEnemies = new List<GameObject>();
    
    [SerializeField]
    private int enemiesCap;
    WaveTemplate currTemp;

    [SerializeField]
    private int[] waveIncrements; //pair indexes with templates
    private int waveNum;

    [SerializeField]
    private GameObject spawnEffect;
    [SerializeField]
    private float effectLife;

    void Start()
    {
        currTemp = templates[0];
        waveNum = 0;

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

        for (int i = 0; i < waveIncrements.Length; i++)
        {
            if(waveNum >= waveIncrements[i])
            {
                currTemp = templates[i];
            }
        }
    }

    public void SpawnEnemies()
    {   
        if((enemiesCap - curEnemies.Count) < currTemp.enemiesAmount) return;
        
        for (int i = 0; i < currTemp.enemiesAmount; i++)
        {
            int ranPos = Random.Range(0, spawnPoints.Length);

            GameObject enemy = Instantiate(PickEnemy(), spawnPoints[ranPos].position, Quaternion.identity);
            enemy.GetComponent<EnemyStats>().SetManager(this);
            curEnemies.Add(enemy);

            GameObject effect = Instantiate(spawnEffect, spawnPoints[ranPos].position, Quaternion.identity);
            Destroy(effect, effectLife);
        }

        waveNum++;
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

    public void RemoveEnemy(GameObject enemy)
    {
        curEnemies.Remove(enemy);
    }
}
