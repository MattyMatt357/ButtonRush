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
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, playerSightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, playerAttackRange, whatIsPlayer);
       

        if (!playerInSightRange) 
        {
            enemyState = EnemyState.Patrolling;
            detectedPlayer = false;
            detectingTime = 0.0f;
        }
        else if (playerInSightRange == true && !playerInAttackRange)
        {
            enemyState = EnemyState.Detecting;
        }

        else if (playerInAttackRange && playerInSightRange)
        {
            enemyState = EnemyState.Attacking;
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

    public void Patrol()
    {
        detectedPlayer = false;
        
        setWalkPoint = false;
        Vector3 distanceToWalkPoint = transform.position - walkingPoint;

         if (distanceToWalkPoint.magnitude < 1f)
         {
            StartCoroutine(EnemyWaiting());
           
         }

         if (setWalkPoint == false)
         {
              animator.SetBool("IsWalking", false);

              float randomZ = Random.Range(-walkingRange, walkingRange);
              float randomX = Random.Range(-walkingRange, walkingRange);

                walkingPoint = new Vector3(transform.position.x + randomX,
                    transform.position.y, transform.position.z + randomZ);

                if (Physics.Raycast(walkingPoint, Vector3.down, 7f, whatIsGround))
                {
                    setWalkPoint = true;
                }
         }
           
            if (setWalkPoint == true) 
            {
                animator.SetBool("IsWalking", true);
               // agent.enabled = true;
                agent.SetDestination(walkingPoint);
              //  transform.LookAt(walkingPoint);
            }

        
    }

    public void DetectingPlayer()
    {
        transform.LookAt(player.transform.position);
        
        detectedPlayer = true;

        if (detectedPlayer)
        {
            detectingTime += 1 * Time.deltaTime; 
            agent.SetDestination(transform.position);
            agent.isStopped = true;
        }

        if (detectingTime >= playerDetectedTime && !playerInAttackRange)
        {
            enemyState = EnemyState.Chasing;
        }
    }

    public void ChasingPlayer()
    {
        agent.SetDestination(player.position);
        agent.isStopped = false;
       
         
    }

    public void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        enemyAttacking = true; 

        if (enemyAttacking)
        {
            animator.SetTrigger("IsAttacking");
            
            StartCoroutine(EnemyAttackCooldown());
        }
       
    }

    public enum EnemyState
    {
        Patrolling,
        Detecting,
        Chasing,
        Attacking
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
}
