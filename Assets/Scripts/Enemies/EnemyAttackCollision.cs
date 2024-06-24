using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    public float enemyAttackDamage;
    public EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && enemyAI.enemyAttacking == true)
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable.ReceiveDamage(enemyAttackDamage);
        }
    }
}
