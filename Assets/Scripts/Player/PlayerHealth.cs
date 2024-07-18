using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float currentPlayerHealth;
    public float maxPlayerHealth;

    public PlayerButtonInputs playerButtonInputs;
    
    // Start is called before the first frame update
    void Start()
    {
        playerButtonInputs = GetComponent<PlayerButtonInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
