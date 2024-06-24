using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceCollison : MonoBehaviour
{
    public float lanceDamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void OnTriggerEnter(Collider other)
    {
        int randomNumber = Random.Range(1, 100);
        int critChance = 77;

        

        if (other.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();

            
             if (randomNumber <= critChance)
             {
                 damageable.ReceiveDamage(lanceDamage * 1.5f);
                 Debug.Log("LanceDamage: " + lanceDamage * 1.5f);
             }

             else 
             {
                 damageable.ReceiveDamage(lanceDamage);
                 Debug.Log("LanceDamage: " + lanceDamage);
             } 
        }
    }

    
    
}
