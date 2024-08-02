using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float currentPlayerHealth;
    public float maxPlayerHealth;

    public PlayerButtonInputs playerButtonInputs;

    public delegate void PlayerDefeat();
    public static event PlayerDefeat playerDefeat;
   // public EnemyHealth enemyHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        playerButtonInputs = GetComponent<PlayerButtonInputs>();
       // enemyHealth = FindObjectOfType<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayerHealth <= 0)
        {
            playerDefeat?.Invoke();
        }

        
    }

    public void ReceiveDamage(float damage)
    {
        if(!playerButtonInputs.isShieldButtonHeld)
        {
            currentPlayerHealth -= damage;
        }
        
    }

    public void OnEnable()
    {
        LevellingSystem.playerLevelUpStats += PlayerIncreaseHealth;
    }
    public void OnDisable()
    {
        LevellingSystem.playerLevelUpStats -= PlayerIncreaseHealth;
    }

    public void PlayerIncreaseHealth()
    {
        maxPlayerHealth *= 1.15f;
        currentPlayerHealth = maxPlayerHealth;
    }
}
