using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent; //reference to navmesh controller
    Transform player; //reference to player's location

    private Animator anim;
    private AngleToPlayer angleToPlayer;

    //[!!!] TEMPORARY!!!
    private Vector4 ranColor;
    SpriteRenderer sr;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; //uses tag to find the "Player"
        agent.speed = GetComponent<EnemyStats>().speed;

        anim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();

        //[!!!] TEMPORARY!!!
        ranColor = new Vector4(Random.value, Random.value, Random.value, 1f);
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = ranColor;
    }

    void Update()
    {   
        anim.SetFloat("spriteRot", angleToPlayer.lastIndex); //handles which animation to play based on direction facing player

        if(player != null)
        {
            agent.SetDestination(player.position); //used to track player
        }

        //animations called later will have correct index
    }
}
