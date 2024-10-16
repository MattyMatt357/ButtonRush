using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoPickUp : MonoBehaviour
{
    public int randomButtonAmmoSelecter;
    public Buttons laserButton;
    public Buttons rocketLauncherButton;
    public Buttons shieldButton;
    public Buttons lanceChargeButton;
    public PlayerHealth playerHealth;
    public LevellingSystem levellingSystem;
    //public int id;

    public static event Action<int> onPickUp;
    // Start is called before the first frame update
    void Start()
    {
       playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
        levellingSystem = GameObject.FindObjectOfType<LevellingSystem>();
        
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
                    laserButton.currentEnergy += 100 * levellingSystem.level;
                    break;
                
                case 1:
                    rocketLauncherButton.currentAmmo += 15 * levellingSystem.level;
                    break;
                case 2:
                    lanceChargeButton.currentAmmo += 15 * levellingSystem.level;
                    break;
                case 3:
                    shieldButton.currentEnergy += 15 * levellingSystem.level;
                    break;
                case 4:
                    playerHealth.currentPlayerHealth += 50 * levellingSystem.level;
                    break;
            }

            //onPickUp?.Invoke(id);
            Destroy(gameObject);
        }
    }
}
