using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IEffectable
{
    public float enemyHealth;
    public LevellingSystem levellingSystem;
    public int expAmount;
    public Animator animator;
    public GameObject playerAmmoPickUp;
    public EnemyAI enemyAI;
    public bool canDamage;

    public bool isEffected;
    public StatusEffect statusEffect;

    public Rigidbody rigidbody;
    public float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        levellingSystem = FindObjectOfType<LevellingSystem>();
        enemyAI = GetComponent<EnemyAI>();
        canDamage = true;
        isEffected = false;
        rigidbody = GetComponent<Rigidbody>();
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


        {
            
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
    /// <summary>
    /// Will give EXP to player, spawn 2 pickups and trigger the enemy's death animation
    /// </summary>
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
    /// <summary>
    /// Checks if 'isEffected' is true or false and if true, will trigger the effects
    /// </summary>
    public void GiveStatusEffect()
    {
        if (!isEffected)
        {
            int randomNumber = Random.Range(0, 100);
            int effectChance = 10;
            isEffected = StatusEffectCheck(effectChance, randomNumber);
            switch (statusEffect)
            {
                case (StatusEffect.Frozen):
                    StartCoroutine(Frozen());
                    break;
                case (StatusEffect.Poison):
                    StartCoroutine(Poisoned());
                    break;

            }

           
            
        }

        
    }

    public static bool StatusEffectCheck(int effectChance, int randomNumber)
    {       
        if (randomNumber >= effectChance)
        {
            return true;
        }
        else
        {
            return false;
        }      
    }

    public enum StatusEffect
    {
        Poison,
        Frozen
    }

    public IEnumerator Frozen()
    {
        while (isEffected)
        {
            //transform.SetPositionAndRotation(transform.position, transform.rotation);
            //rigidbody.constraints = RigidbodyConstraints.FreezeAll;

            enemyAI.agent.speed = 0;
            yield return new WaitForSeconds(10);
            // rigidbody.constraints = RigidbodyConstraints.None;
            enemyAI.agent.speed = enemySpeed;

            isEffected = false;
        }
        
    }

   

    public IEnumerator Poisoned()
    {
        while (isEffected)
        {                       
            for (int i = 0; i < 5; i++)
            {
                ReceiveDamage(5);

                yield return new WaitForSeconds(3f);
            }
            isEffected = false;
            
        }
    }
}
