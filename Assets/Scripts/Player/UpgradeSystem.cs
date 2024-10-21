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
    public int playerDefenseIncreaseLimit;
    public int playerSpeedIncreaseLimit;
    public int playerDefenseUpgradeLevel;
    public int playerSpeedUpgradeLevel;
    public bool hasEnemyDebuffUpgrade;
    public int buttonsUpgradeLimit;
    public int buttonsUpgradeLevel;


    [Header("Upgrade Buttons And Toggles")]
    public Button playerDefenseUpgradeButton;
    public Button playerSpeedUpgradeButton;
    public Toggle enemyDefenseDebuffToggle;
    public Button backToPauseMenuButton;
    public Button buttonsUpgradeButton;

    //Actions
    public static event Action playerDefenseUpgrade;

    public static event Action<Boolean> enemyLowDefenseEffectUpgrade;

    public static event Action playerSpeedUpgrade;
    public static event Action backToPauseMenu;
    public static event Action buttonsUpgrade;


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
            enemyDefenseDebuffToggle.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {     
        levelPoints = Mathf.Clamp(levelPoints, 0, 15);
    }

    public void PlayerDefenseIncrease()
    {
        if (levelPoints >= 1 && playerDefenseUpgradeLevel < playerDefenseIncreaseLimit)
        {                           
            levelPoints--;
            playerDefenseUpgrade?.Invoke();
            playerDefenseUpgradeLevel++;

        }        
    }

    public void EnableLowEnemyDefenseEffect(bool hasDebuffUpgrade)
    {
        if(hasEnemyDebuffUpgrade == false)
        {
            if (levelPoints >= 2)
            {
                levelPoints -= 2;
                hasEnemyDebuffUpgrade = hasDebuffUpgrade;
                enemyLowDefenseEffectUpgrade?.Invoke(hasEnemyDebuffUpgrade);
            }
        }
    }

    public void ObtainLevelPoints()
    {
        levelPoints++;
    }

    public void PlayerSpeedIncrease()
    {
        if (levelPoints >= 1 && playerSpeedUpgradeLevel < playerSpeedIncreaseLimit)
        {
            levelPoints--;
            playerSpeedUpgrade?.Invoke();
            playerSpeedUpgradeLevel++;
        }      
    }

    public void BackToPauseMenu()
    {
        backToPauseMenu?.Invoke();
    }

    public void UpgradeButtons()
    {
        if (levelPoints >= 1 && buttonsUpgradeLevel < buttonsUpgradeLimit)
        {
            levelPoints--;
            buttonsUpgrade?.Invoke();
            buttonsUpgradeLevel++;
        }
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
