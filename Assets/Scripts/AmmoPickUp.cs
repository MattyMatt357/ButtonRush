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
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        randomButtonAmmoSelecter = Random.Range(0, 3);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (randomButtonAmmoSelecter)
            {
                case 0:
                    laserButton.currentEnergy += 100;
                    break;
                
                case 1:
                    rocketLauncherButton.currentAmmo += 5;
                    break;
                case 2:
                    lanceChargeButton.currentAmmo += 5;
                    break;
                case 3:
                    shieldButton.currentEnergy += 15;
                    break;
            }

           // Destroy(gameObject);
        }
    }
}
