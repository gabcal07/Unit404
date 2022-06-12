using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Random = UnityEngine.Random;

public class NewIA : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    Animator animator;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public int BaseDamage;
    public int BaseDamageBoss;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool Boss;

    private void Awake()
    {
        player = GameObject.Find("Player F(Clone)").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("updateAggro", 1f, 3f);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer(player.gameObject); 
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        animator.SetFloat("Speed", agent.velocity.magnitude);

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkpoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        //Walkpoint reached
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkpoint()
    {
        //we choose a random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer(GameObject player)
    {
        agent.SetDestination(player.transform.position);
        agent.transform.LookAt(player.transform.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        

        if (!alreadyAttacked)
        {
            int rand = Random.Range(0, 2);
            //Code de l'attaque qu'on veut
            if (rand == 0)
            {
                
                animator.SetBool("Punch", true);
                if (playerInAttackRange)
                {
                    if (Boss)
                    {
                        StartCoroutine(w(2));
                        player.GetComponentInChildren<PManager>().damage(BaseDamageBoss);

                    }
                    else
                    {
                        StartCoroutine(w(2));

                        player.GetComponentInChildren<PManager>().damage(BaseDamage);
                    }
                }
            }
            else
            {
                animator.SetBool("Kick", true);
                if (Boss)
                {
                    StartCoroutine(w(2));

                    player.GetComponentInChildren<PManager>().damage(BaseDamageBoss-3);
                }
                else
                {
                    StartCoroutine(w(2));

                    player.GetComponentInChildren<PManager>().damage(BaseDamage+5);
                }
            }
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        animator.SetBool("Punch", false);
        animator.SetBool("Kick", false);
        alreadyAttacked = false;
        
    }

    private void OnDrawGizmosSelected()
    {
        // afficher le perimetre de detection de l'IA par une sphere jaune
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        //afficher la portee d'attaque de l'IA par une sphere rouge
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private Transform FindClosestPlayer()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject go in gameObjects)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest.transform;
    }

    private void updateAggro()
    {
        player = FindClosestPlayer();

    }

    IEnumerator w(float i)
    {
        yield return new WaitForSeconds(i);
    }
}
