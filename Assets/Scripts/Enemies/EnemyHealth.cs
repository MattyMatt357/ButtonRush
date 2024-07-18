using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float enemyHealth;
    public LevellingSystem levellingSystem;
    public int expAmount;
    public Animator animator;
    public GameObject playerAmmoPickUp;
    public EnemyAI enemyAI;
    public bool canDamage;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        levellingSystem = FindObjectOfType<LevellingSystem>();
        enemyAI = GetComponent<EnemyAI>();
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.enemyState == EnemyAI.EnemyState.Death)
        {
            //StartCoroutine(EnemyDeathCooldown());
        }

        if (canDamage)
        {
            if (enemyHealth <= 0)
            {
                EnemyDeath();
            }
        }
    }

    public void ReceiveDamage(float damage)
    {
        if (canDamage)
        {
            enemyHealth -= damage;
            Debug.Log(enemyHealth);

            
        }
    }

    public void EnemyDeath()
    {
        canDamage = false;
        levellingSystem.AddExp(expAmount);
        GameObject pickUp1 = Instantiate(playerAmmoPickUp, transform.position, transform.rotation);
        GameObject pickUp2 = Instantiate(playerAmmoPickUp, transform.position, transform.rotation);
        animator.SetTrigger("DeathTrigger");
        
       // enemyAI.enemyState = EnemyAI.EnemyState.Death;

        //GetComponent<Animator>().Play("Death");
       // Destroy(gameObject);
    }

    
}
