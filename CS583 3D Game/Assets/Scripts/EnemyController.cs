using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent; //reference to navmesh controller
    Transform player; //reference to player's location

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; //uses tag to find the "Player"
    }

    void Update()
    {
        if(player != null)
        {
            agent.SetDestination(player.position); //used to track player
        }
    }
}
