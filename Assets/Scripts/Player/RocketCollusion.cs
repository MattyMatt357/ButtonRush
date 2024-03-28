using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollusion : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionForce;

    //public PlayerHealth player;

    public LayerMask enemyLayer;
    void Start()
    {
       // player = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
       // Physics.IgnoreCollision(player.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>(), true);
        
    }

    public void OnCollisionEnter(Collision other)
    {
        /* int randomNumber = Random.Range(1, 100);
         int criticalChance = 77;

         float rocketDamage = 35f;

         if (other.gameObject.CompareTag("Enemy"))
         {
             IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
             if (randomNumber <= criticalChance)
             {
                 damageable.ReceiveDamage(rocketDamage * 2);
             }

             else if (randomNumber != criticalChance)
             {
                 damageable.ReceiveDamage(rocketDamage);
             } */
        if (other.gameObject.CompareTag("Enemy"))
        {
            //IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            RocketDamage(other.contacts[0].point);
            Destroy(gameObject);
        }
        
        //if (other.gameObject.CompareTag("Player"))
        {
            //Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), this.GetComponent<Collider>(), true);
          // Destroy(gameObject);
        }


    }

    /// <summary>
    /// Detects colliders with an sphere around the rockets
    /// </summary>
    /// <param name="rocketPoint"></param>
    public void RocketDamage(Vector3 rocketPoint)
    {
        int randomNumber = Random.Range(1, 100);
        int criticalChance = 77;

        float rocketDamage = 35f;

        Collider[] colliders = Physics.OverlapSphere(rocketPoint, 20f, enemyLayer);
        foreach (Collider collider in colliders) 
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
           

            if (collider.GetComponent<Rigidbody>() != null)
            {              
                collider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, rocketPoint, 10f, 0.5f, ForceMode.Impulse);

                
            }

            if (damageable != null)
            {

                if (randomNumber <= criticalChance)
                {
                    damageable.ReceiveDamage(rocketDamage * 2);
                }

                else if (randomNumber != criticalChance)
                {
                    damageable.ReceiveDamage(rocketDamage);
                }
            }

            
        }
    }
}
