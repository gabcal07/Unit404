using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IA_Basic_movement : MonoBehaviour
{

    //public Transform playerTransform;
    
    public NavMeshAgent agent;
    
	public Transform player;

	public LayerMask whatIsGround,whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
	//Attacking
	public float timeBetweenAttacks;
	bool alreadyAttacked;

    //States
    public float sightRange,attackRange;
    public bool playerInSightRange, playerInAttackRange;

	private void Awake()
	{
		player = GameObject.Find("Player").transform;
		agent = GetComponent<NavMeshAgent>();
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange,whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
        
    }
    
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkpoint();

        if (walkPointSet) agent.SetDestination(walkPoint);
        
        //Walkpoint reached
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f )
            walkPointSet = false;
        
    }

    private void SearchWalkpoint()
    {
        //we choose a random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint,-transform.up , 2f,whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
            
    }

	private void AttackPlayer()
	{
		agent.SetDestination(transform.position);
        
        transform.LookAt(player);

        if (!alreadyAttacked){

            //Code de l'attaque qu'on veut

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
	}

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        // afficher le perimetre de detection de l'IA sous forme de sphere
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
