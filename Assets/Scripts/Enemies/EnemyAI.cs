using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public EnemyState enemyState;

    public Vector3 walkingPoint;
    public bool setWalkPoint;
    public float walkingRange;

    public bool playerInSightRange;
    public float playerSightRange;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public float detectingTime;
    public bool detectedPlayer;
    public float playerDetectedTime = 4f;

    public Animator animator;
    public bool playerInAttackRange;
    public float playerAttackRange;

    [HideInInspector]
    public bool enemyAttacking;

    public Transform target;
    public bool isChasing;

    public Transform sphere;

    public float timeBetweenAttacks;
    public bool isPatrolling;
    // public Transform enemyTransfrom;
    public Vector3 playerLocation;

    public EnemyHealth enemyHealth;
    [HideInInspector]
    public bool enemyDead;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerCube").transform;
        enemyState = EnemyState.Patrolling;
        detectedPlayer = false;
        detectingTime = 0.0f;
        animator = GetComponent<Animator>();
        enemyAttacking = false;
        isChasing = false;
        sphere = transform.Find("SpheresCentre");
        isPatrolling = true;
       // enemyTransfrom = GameObject.Find("oritentation").transform;
       enemyHealth = GetComponent<EnemyHealth>();
        enemyDead = false;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(sphere.transform.position, playerSightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(sphere.transform.position, playerAttackRange, whatIsPlayer);
       
        playerLocation = new Vector3(player.position.x, transform.position.y, player.position.z);

        if (!playerInSightRange && !playerInAttackRange)
        {
            isPatrolling = true;
            isChasing = false;
            if (isPatrolling && !isChasing)
            {
                enemyState = EnemyState.Patrolling;
            }
           
        }

        if (playerInSightRange && !playerInAttackRange && !isChasing)
        { 
            isPatrolling = false;
            enemyState = EnemyState.Detecting;
        }

        if (playerInSightRange && !playerInAttackRange && isChasing) 
        {
            enemyState = EnemyState.Chasing;
        }

        if (playerInSightRange && playerInAttackRange)
        {
            enemyState = EnemyState.Attacking;
        }

        if(!enemyHealth.canDamage)
        {
            enemyState = EnemyState.Dying;
        }

        if (enemyDead)
        {
            enemyState = EnemyState.Death;
        }

        switch (enemyState) 
          {
              case EnemyState.Patrolling:
                  Patrol();
                  break;
              case EnemyState.Detecting:
                  DetectingPlayer();
                  break;
              case EnemyState.Chasing:
                  ChasingPlayer();
                break;
              case EnemyState.Attacking:
                  AttackPlayer();
                  break;
              case EnemyState.Dying:
                StartCoroutine(EnemyDeathCooldown());
                  break;
              case EnemyState.Death:
                gameObject.SetActive(false);
                break;

          } 

       /* if (enemyState == EnemyState.Patrolling)
        {
            Patrol();
        }
        else if (enemyState == EnemyState.Detecting)
        {
            DetectingPlayer();
        } */

       // agent.SetDestination(player.transform.position);
    }
    /// <summary>
    /// Makes the enemy patrol
    /// </summary>
    public void Patrol()
    {
        isChasing = false;
        // if setWalkPoint == false, look for "WalkingPoint"
        if (!setWalkPoint)
        {
            SearchForWalkPoint();
        }
        
        //Makes enemy go to walking point
        if (setWalkPoint)
        {
            animator.SetBool("IsWalking", true);
            agent.SetDestination(walkingPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkingPoint;

        if (distanceToWalkPoint.magnitude < 1)
        {
            setWalkPoint = false;
        }
    }
    /// <summary>
    /// If the player is in the enemy's sight range, the enemy will detect the player over time
    /// </summary>
    public void DetectingPlayer()
    {
        isPatrolling = false;
       // transform.LookAt(enemyTransfrom.transform.position);
        transform.LookAt(playerLocation);
        detectedPlayer = true;

        if (detectedPlayer)
        {
            detectingTime += 1 * Time.deltaTime; 
            agent.SetDestination(transform.position);
            agent.isStopped = true;
        }

        if (playerInSightRange && detectingTime >= playerDetectedTime)
        {
            isChasing = true;
        }
    }
    /// <summary>
    /// Sets the enemy to chase the player
    /// </summary>
    public void ChasingPlayer()
    { 
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }
    /// <summary>
    /// The function to make the enemy attack the player
    /// </summary>
    public void AttackPlayer()
    {
       // enemyTransfrom.transform.LookAt(player.transform.position);
        transform.LookAt(playerLocation);
        
        agent.SetDestination(transform.position);
        enemyAttacking = true;
        animator.SetBool("IsWalking", false);
        agent.isStopped = true;

        if (enemyAttacking)
        {
            animator.SetTrigger("IsAttacking");

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
       
    }

    public enum EnemyState
    {
        Patrolling = 0,
        Detecting = 1,
        Chasing = 2,
        Attacking = 3,
        Dying = 4,
        Death = 5,
    }

    public IEnumerator EnemyWaiting()
    {
        yield return new WaitForSeconds(5);
        setWalkPoint = false;
    }

    public IEnumerator EnemyAttackCooldown()
    {
        enemyAttacking = false;
        yield return new WaitForSeconds(3f);
        enemyAttacking = true;

    }
    public void ResetAttack()
    {
        enemyAttacking = false;
    }

    public void SearchForWalkPoint()
    {
        animator.SetBool("IsWalking", false);

        float randomZ = Random.Range(-walkingRange, walkingRange);
        float randomX = Random.Range(-walkingRange, walkingRange);

        walkingPoint = new Vector3(transform.position.x + randomX,
            transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkingPoint, -transform.up, 7f, whatIsGround))
        {
            setWalkPoint = true;
        }
    }

   /* public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sphere.transform.position, playerSightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sphere.transform.position, playerAttackRange);
    } */

    public IEnumerator EnemyDeathCooldown()
    {
        yield return new WaitForSeconds(5f);
        enemyDead = true;
    }

    
}
