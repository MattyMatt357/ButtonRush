using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    [Header("Upgrade Points And Limits")]
    public int levelPoints;
    [FormerlySerializedAs("playerDefenseLimit")]
    public int playerDefenseIncreaseLimit;
    public int playerSpeedIncreaseLimit;
    public int playerDefenseUpgradeLevel;
    public int playerSpeedUpgradeLevel;
    public bool hasEnemyDebuffUpgrade;

    [Header("Upgrade Buttons And Toggles")]
    public Button playerDefenseUpgradeButton;
    public Button playerSpeedUpgradeButton;
    public Toggle enemyDefenseDebuffToggle;
    public Button backToPauseMenuButton;

    //Actions
    public static event Action playerDefenseUpgrade;

    public static event Action<Boolean> enemyLowDefenseEffectUpgrade;

    public static event Action playerSpeedUpgrade;
    public static event Action backToPauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        if(MainMenuOptions.isLoadedGame == false)
        {
            levelPoints = 0;
            playerDefenseUpgradeLevel = 0;
            playerSpeedUpgradeLevel = 0;
            enemyDefenseDebuffToggle.interactable = true;
            hasEnemyDebuffUpgrade = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // playerDefenseUpgradeButton
        if (playerDefenseUpgradeLevel == 4)
        {
            playerDefenseUpgradeButton.interactable = false;
        }
        else if(playerDefenseUpgradeLevel < 4)
        {
            playerDefenseUpgradeButton.interactable = true;
            //playerDefenseUpgradeButton.Select();
        }


        if (playerDefenseUpgradeLevel == 4)
        {
            playerSpeedUpgradeButton.interactable = false;
        }
        else if (playerSpeedUpgradeLevel < 4)
        {
            playerDefenseUpgradeButton.interactable = true;
        }

        if (hasEnemyDebuffUpgrade)
        {
            enemyDefenseDebuffToggle.interactable = false;
        }

       
        levelPoints = Mathf.Clamp(levelPoints, 0, 15);

    }

    public void PlayerDefenseIncrease()
    {
       
        {
            for ( playerDefenseUpgradeLevel =0; playerDefenseUpgradeLevel 
                < playerDefenseIncreaseLimit; playerDefenseUpgradeLevel++)
            {
                if (levelPoints >= 1)
                {
                    levelPoints--;
                    playerDefenseUpgrade?.Invoke();
                }
                    
            }
        }           
    }

    public void EnableLowEnemyDefenseEffect(bool hasDebuffUpgrade)
    {
        if (levelPoints >= 2)
        {
            levelPoints -= 2;
            hasEnemyDebuffUpgrade = hasDebuffUpgrade;
            enemyLowDefenseEffectUpgrade?.Invoke(hasEnemyDebuffUpgrade);
        }           
    }

    public void ObtainLevelPoints()
    {
        levelPoints++;
    }

    public void PlayerSpeedIncrease()
    {
        if (levelPoints >= 1)
        {
            levelPoints--;
            for (playerSpeedUpgradeLevel = 0; playerSpeedUpgradeLevel
                < playerSpeedIncreaseLimit; playerSpeedUpgradeLevel++)
            {
               
                playerSpeedUpgrade?.Invoke();
            }
        }         
    }

    public void BackToPauseMenu()
    {
        backToPauseMenu?.Invoke();
    }

    public void OnEnable()
    {
        LevellingSystem.playerLevelUpStats += ObtainLevelPoints;
    }

    public void OnDisable()
    {
        LevellingSystem.playerLevelUpStats -= ObtainLevelPoints;
    }
}
