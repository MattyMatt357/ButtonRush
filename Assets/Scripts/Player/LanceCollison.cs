using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CalculateDamage;

public class LanceCollison : MonoBehaviour
{
    public float lanceDamage;
    public Buttons lanceButton;
    public ButtonDamageType lanceDamageType;
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
            IEffectable effectable = other.gameObject.GetComponent<IEffectable>();
            if (damageable != null)
            {
               // float lanceCritDamage = PlayerCriticalChance.WeaponDamageChance(randomNumber, critChance, lanceButton.buttonDamage, 1.5f);
                //damageable.ReceiveDamage(lanceCritDamage);
            }
            effectable?.GiveStatusEffect();
           
            

            
           
        }
    }

    
    
}
