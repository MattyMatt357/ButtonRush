using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour, IDamageable, IEffectable
{

    public float enemyHealth;
    public float maxEnemyHealth;
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
    // public static int enemyKills;
    public ButtonDamageTypes damageTypes;

    public EnemyHealthBar enemyHealthBar;
    public static bool isLowDefenseUpgradeOn;
    public float enemyDefense;
    public GameFinished gameFinished;
    public void OnEnable()
    {
        UpgradeSystem.enemyLowDefenseEffectUpgrade += EnemyLowDefenseEffectUpgrade;
    }

    public void OnDisable()
    {
        UpgradeSystem.enemyLowDefenseEffectUpgrade -= EnemyLowDefenseEffectUpgrade;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        levellingSystem = FindObjectOfType<LevellingSystem>();
        enemyAI = GetComponent<EnemyAI>();
        canDamage = true;
        isEffected = false;
        rigidbody = GetComponent<Rigidbody>();
        enemyHealthBar = GetComponentInChildren<EnemyHealthBar>();
       // enemyHealthBar.maxHealthValue = maxEnemyHealth;
        enemyHealthBar.EnemyHealthBarDisplay(enemyHealth, maxEnemyHealth);
        gameFinished = FindObjectOfType<GameFinished>();
        if (MainMenuOptions.isLoadedGame == false)
        {
            isLowDefenseUpgradeOn = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        

        //if (canDamage)
        {
            
        }
    }

    public void ReceiveDamage(float damage)
    {
        if (canDamage)
        {
            enemyHealth -= damage;
            Debug.Log(enemyHealth);
            enemyHealthBar.EnemyHealthBarDisplay(enemyHealth, maxEnemyHealth);
            enemyHealthBar.EnemyDamageTextDisplay(damage, transform);
            if (enemyHealth <= 0)
            {
                EnemyDeath();
            }
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
        gameFinished.enemyKills++;
        Debug.Log("Enemy Kills: " + gameFinished.enemyKills);

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
            int effectChance = 55;
            isEffected = StatusEffectCheck(effectChance, randomNumber);
            switch (statusEffect)
            {
                case (StatusEffect.Frozen):
                    StartCoroutine(Frozen());
                    enemyHealthBar.ShowStatusEffect("Frozen");
                    break;
                case (StatusEffect.Poison):
                    StartCoroutine(Poisoned());
                    enemyHealthBar.ShowStatusEffect("Poisoned");
                    break;
            }

            if (isLowDefenseUpgradeOn && statusEffect == StatusEffect.DefenseDebuff)
            {
                StartCoroutine(EnemyDefenseDebuff());
                enemyHealthBar.ShowStatusEffect("Low Defense");
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
        Poison =1,
        Frozen= 2,
        DefenseDebuff= 3
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
                float poisonDamage = maxEnemyHealth * 0.05f;
                ReceiveDamage(poisonDamage);

                yield return new WaitForSeconds(3f);
            }
            isEffected = false;

        }
    }
    //public event Action<int, float, float> onEnemyHealthBarDisplay;

    public void ReceiveDamage(float damage, ButtonDamageType buttonDamageType, bool isCritHit)
    {
        if (canDamage)
        {
            enemyHealth -= damageTypes.CalculateButtonDamageResistance(damage, buttonDamageType)
                * (100/(100+enemyDefense));
            Debug.Log(enemyHealth);
            enemyAI.hasBeenAttacked = true;
            enemyHealthBar.EnemyHealthBarDisplay(enemyHealth,maxEnemyHealth);

          
                enemyHealthBar.EnemyDamageTextDisplay
                (damageTypes.CalculateButtonDamageResistance
                (damage, buttonDamageType), damageTypes, buttonDamageType, isCritHit);
            if (enemyHealth <= 0)
            {
                EnemyDeath();
            }

        }
    }

    public void EnemyLowDefenseEffectUpgrade(bool enemyDefenseDebuffUpgradeOn)
    {
        isLowDefenseUpgradeOn = enemyDefenseDebuffUpgradeOn;
    }

    public IEnumerator EnemyDefenseDebuff()
    {
        
        while(isEffected)
        {
            enemyDefense *= 0.5f;
            yield return new WaitForSeconds(20);
            enemyDefense *= 2;
            isEffected = false;
        }
       
    }
}
