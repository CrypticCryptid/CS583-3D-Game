using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent; //reference to navmesh controller
    Transform player; //reference to player's location

    private Animator anim;
    private AngleToPlayer angleToPlayer;

    public float attackInterval = 1.0f;
    private float attackCooldown = 0f;
    private GameObject currentTarget = null;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; //uses tag to find the "Player"
        agent.speed = GetComponent<EnemyStats>().speed;

        anim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();
    }

    void Update()
    {   
        anim.SetFloat("spriteRot", angleToPlayer.lastIndex); //handles which animation to play based on direction facing player

        if(player != null)
        {
            agent.SetDestination(player.position); //used to track player
        }

        if (currentTarget != null)
        {
            attackCooldown -= Time.deltaTime;

            if (attackCooldown <= 0f)
            {
                ITakeDamage damageReceiver = currentTarget.GetComponent<ITakeDamage>();
                if (damageReceiver != null)
                {
                    damageReceiver.TakeDamage(GetComponent<EnemyStats>().damage);
                }

                attackCooldown = attackInterval;
            }
        }

        //animations called later will have correct index
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            currentTarget = other.gameObject;
            attackCooldown = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentTarget)
        {
            currentTarget = null;
            attackCooldown = 0f;
        }
    }
}
