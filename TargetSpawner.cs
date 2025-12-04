using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public Transform spawnPoint;
    public float respawnDelay = 1f;

    private GameObject currentTarget;
    private bool isRespawning = false;

    void Start()
    {
        SpawnTarget();
    }

    void Update()
    {
        // If there is no target and we're not already waiting to respawn one:
        if (currentTarget == null && !isRespawning)
        {
            StartCoroutine(RespawnAfterDelay());
        }
    }

    void SpawnTarget()
    {
        currentTarget = Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator RespawnAfterDelay()
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnDelay);
        SpawnTarget();
        isRespawning = false;
    }
}
