using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayLibrary.CalculateChance;
//using CalculateDamage;

public class RocketCollusion : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionForce;

    //public Collider player;
    public Buttons rocketButton;
    public LayerMask enemyLayer;
    public ButtonDamageType rocketDamageType;
    public Rigidbody rocketRigidbody;
    public Transform rocketSpawn;
   // private Collider rocketCollider;
    void Start()
    {
       // player = GameObject.Find("Player").GetComponent<Collider>();
       // rocketCollider = this.GetComponent<Collider>();
       rocketRigidbody = GetComponent<Rigidbody>();
        rocketSpawn = GameObject.Find("RocketSpawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Physics.IgnoreCollision(player, rocketCollider, true);
        rocketRigidbody.AddForce(rocketSpawn.forward * 5, ForceMode.Impulse);
        //rocketRigidbody.velocity = Vector3.zero;

    }

    private void FixedUpdate()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.CompareTag("Enemy"))
        {
            //IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            RocketDamage(other.contacts[0].point);
            
        }
       gameObject.SetActive(false);

    }

    /// <summary>
    /// Detects colliders with an sphere around the rockets
    /// </summary>
    /// <param name="rocketPoint"></param>
    public void RocketDamage(Vector3 rocketPoint)
    {
        int randomNumber = Random.Range(1, 100);
        int criticalChance = 77;

       // float rocketDamage = 35f;

        Collider[] colliders = Physics.OverlapSphere(rocketPoint, 40f, enemyLayer);
        foreach (Collider collider in colliders) 
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
           

            if (collider.GetComponent<Rigidbody>() != null)
            {              
                collider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, rocketPoint, 10f, 0.5f, ForceMode.Impulse);

                
            }

            if (damageable != null)
            {
                bool checkForCriticalHit = PlayerCriticalChance.CheckForChance(randomNumber, criticalChance);
                float rocketCritDamage = PlayerCriticalChance.GiveNumberMultipliedIfBoolTrue
                    (checkForCriticalHit, rocketButton.buttonDamage, 2f);
                damageable.ReceiveDamage(rocketCritDamage, rocketDamageType, checkForCriticalHit);
            }
             
            IEffectable effectable = collider.gameObject.GetComponent<IEffectable>();
            effectable?.GiveStatusEffect();

        }
    }

   
}
