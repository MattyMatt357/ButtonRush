using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int randomButtonAmmoSelecter;
    public Buttons laserButton;
    public Buttons rocketLauncherButton;
    public Buttons shieldButton;
    public Buttons lanceChargeButton;
    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
       playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        randomButtonAmmoSelecter = Random.Range(0, 4);
        if (other.CompareTag("Player"))
        {
            switch (randomButtonAmmoSelecter)
            {
                case 0:
                    laserButton.currentEnergy += 100;
                    break;
                
                case 1:
                    rocketLauncherButton.currentAmmo += 15;
                    break;
                case 2:
                    lanceChargeButton.currentAmmo += 15;
                    break;
                case 3:
                    shieldButton.currentEnergy += 15;
                    break;
                case 4:
                    playerHealth.currentPlayerHealth += 50;
                    break;
            }

            Destroy(gameObject);
        }
    }
}
