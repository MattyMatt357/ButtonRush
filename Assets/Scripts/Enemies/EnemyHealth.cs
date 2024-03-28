using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public float enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        
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
            Destroy(gameObject);
        }
    }
}
