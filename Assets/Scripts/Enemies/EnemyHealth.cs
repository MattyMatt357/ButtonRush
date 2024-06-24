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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        levellingSystem = FindObjectOfType<LevellingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(float damage)
    {
        enemyHealth -= damage;
        Debug.Log(enemyHealth);

        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        levellingSystem.AddExp(expAmount);
        Instantiate(playerAmmoPickUp);
        animator.SetTrigger("DeathTrigger");
        //GetComponent<Animator>().Play("Death");
       // Destroy(gameObject);
    }
}
