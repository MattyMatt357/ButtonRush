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
    public float playerDefense;
    
    // Start is called before the first frame update
    void Start()
    {
        playerButtonInputs = GetComponent<PlayerButtonInputs>();
       // enemyHealth = FindObjectOfType<EnemyHealth>();
       if(MainMenuOptions.isLoadedGame == false )
        {
            maxPlayerHealth = 100f;
            currentPlayerHealth = maxPlayerHealth;
            playerDefense = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    public void ReceiveDamage(float damage)
    {
        if(!playerButtonInputs.isShieldButtonHeld)
        {
            currentPlayerHealth -= damage * (100/(100+playerDefense));
        }

        if (currentPlayerHealth <= 0)
        {
            playerDefeat?.Invoke();
        }

    }

    public void OnEnable()
    {
        LevellingSystem.playerLevelUpStats += PlayerIncreaseHealth;
        UpgradeSystem.playerDefenseUpgrade += DefenseUpgrade;
    }
    public void OnDisable()
    {
        LevellingSystem.playerLevelUpStats -= PlayerIncreaseHealth;
        UpgradeSystem.playerDefenseUpgrade -= DefenseUpgrade;
    }

    public void PlayerIncreaseHealth()
    {
        maxPlayerHealth *= 1.2f;
        currentPlayerHealth = maxPlayerHealth;
    }

    public void ReceiveDamage(float damage, ButtonDamageType buttonDamageTypes, bool isCritHit)
    {
        throw new System.NotImplementedException();
    }

    public void DefenseUpgrade()
    {
        playerDefense += 2;
    }
}
