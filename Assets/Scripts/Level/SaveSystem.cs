using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SavingAndLoadingLibrary;
using System;
using System.Threading;
using GameplayLibrary.ChangingLevels;

public class SaveSystem : MonoBehaviour
{
    [Header("GameState")]
    public GameState gameState = new GameState();
    [Header("Script References")]
    public SavingSystem savingSystem;

    public PlayerHealth playerHealth;
    public Transform playerTransform;

    public Transform playerCamera;

    public LevellingSystem levellingSystem;
    public UpgradeSystem upgradeSystem;
    public ButtonUiDisplay buttonUiDisplay;
    public GameFinished gameFinished;
    public PlayerMovement playerMovement;



    [Header("Button References")]
    public Buttons shieldButton;
    public Buttons laserButton;
    public Buttons lanceChargeButton;
    public Buttons rocketLauncherButton;
    public PlayerButtonInputs playerButtonInputs;

    //public int playerEquippedWeapon;

    [Header("Enemies")]
    public GameObject[] enemies;
    public EnemyAI[] enemyAIs;
    public EnemyHealth[] enemyHealth;
    //Events/Actions
    public delegate void DeactivateMenus();
    public static event DeactivateMenus deactivateMenus;
    public static event Action<int, int> onLoadCameraPriority;
    //public GameFinished gameFinished;
  

    //public AmmoPickUp[] ammoPickUpsArray;
    //public GameObject ammmoPickUpsPrefab;
    
    //public List<AmmoPickUp> ammoPickUps = new List<AmmoPickUp>();
    // Start is called before the first frame update
    void Start()
    {
        //References to objects or scripts
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerTransform = GameObject.Find("Player").transform;
        savingSystem = GetComponent<SavingSystem>();
        playerCamera = GameObject.Find("Main Camera").transform;
        levellingSystem = FindObjectOfType<LevellingSystem>();
        playerButtonInputs = FindObjectOfType<PlayerButtonInputs>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        upgradeSystem = FindObjectOfType<UpgradeSystem>();
        buttonUiDisplay = FindObjectOfType<ButtonUiDisplay>();
        gameFinished = FindObjectOfType<GameFinished>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        

        //Orders array
        Array.Sort(enemies, (a, b) => { return a.name.CompareTo(b.name); });
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyAIs[i] = enemies[i].GetComponent<EnemyAI>();
            enemyHealth[i] = enemies[i].GetComponent<EnemyHealth>();

        }

        //If loading already started game from Main Menu
        if (MainMenuOptions.isLoadedGame == true)
        {
            LoadGame();

        }
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKey(KeyCode.Space))
         {
             SaveGame();
         }
 //
         if (Input.GetKey(KeyCode.Backspace))
         {
             LoadGame();
         }*/
    }

    public void SaveGame()
    {
        //Player health & position
        gameState.playerMaxHealth = playerHealth.maxPlayerHealth;
        gameState.playerCurrentHealth = playerHealth.currentPlayerHealth;
        gameState.playerPosition = playerTransform.position;
        gameState.playerRotation = playerTransform.rotation;
        gameState.cameraRotation = playerCamera.rotation;
        gameState.cameraPosition = playerCamera.position;
        //Player level and upgrade values
        gameState.playerLevel = levellingSystem.level;
        gameState.maxEXP = levellingSystem.maxExp;
        gameState.currentEXP = levellingSystem.currentExp;
        gameState.playerDefense = playerHealth.playerDefense;
        gameState.playerDefenseUpgradeLevel = upgradeSystem.playerDefenseUpgradeLevel;
        gameState.buttonsUpgradeLevel = upgradeSystem.buttonsUpgradeLevel;
        gameState.playerSpeedUpgradeLevel = upgradeSystem.playerSpeedUpgradeLevel;
        gameState.playerSpeed = playerMovement.playerSpeed;

        gameState.timeRemaining = buttonUiDisplay.timeRemaining;
        //Buttons
        //  gameState.lanceCurrentAmmo = lanceChargeButton.currentAmmo;
        //  gameState.lanceMaxAmmo = lanceChargeButton.maxAmmo;
        //  gameState.laserCurrentEnergy = laserButton.currentEnergy;
        //  gameState.laserMaxEnergy = laserButton.maxEnergy;
        //  gameState.rocketCurrentAmmo = rocketLauncherButton.currentAmmo;
        // gameState.rocketMaxAmmo = rocketLauncherButton.maxAmmo;
        // gameState.shieldCurrentEnergy = shieldButton.currentEnergy;
        // gameState.shieldMaxEnergy = shieldButton.maxEnergy;
        // playerButtonInputs.equippedButton = playerButtonInputs.EquippedButton;
        gameState.playerEquippedButton = (int)playerButtonInputs.equippedButton;

        savingSystem.SaveJson(laserButton, "/laserData");
        savingSystem.SaveJson(rocketLauncherButton, "/RocketData");
        savingSystem.SaveJson(lanceChargeButton, "/lanceButtonData");
        savingSystem.SaveJson(shieldButton, "/ShieldButtonData");

        //Enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            gameState.enemyHealth[i] = enemyHealth[i].enemyHealth;
            gameState.enemyPosition[i] = enemies[i].transform.position;
            gameState.enemyRotation[i] = enemies[i].transform.rotation;
            gameState.enemyDead[i] = enemyAIs[i].enemyDead;
            gameState.enemyStates[i] = (int)enemyAIs[i].enemyState;

            gameState.enemyPatrolling[i] = enemyAIs[i].isPatrolling;
            gameState.enemyChasing[i] = enemyAIs[i].isChasing;
            gameState.enemyBeenAttacked[i] = enemyAIs[i].hasBeenAttacked;
        }
        gameState.enemyKills = gameFinished.enemyKills;
        gameState.levelPoints = upgradeSystem.levelPoints;
        gameState.hasEnemyDebuffUpgrade = upgradeSystem.hasEnemyDebuffUpgrade;
        //gameState.hasEnemyDebuffUpgrade = EnemyHealth.isLowDefenseUpgradeOn;

        //Looks for ammo pick ups
        // ammoPickUpsArray = FindObjectsOfType<AmmoPickUp>();
        //ammoPickUps.
        /*  for (int j = 0; j < ammoPickUpsArray.Length; j++)
          {
              ammoPickUps.Clear();
              ammoPickUps.Add(ammoPickUpsArray[j]);
          }

          if (ammoPickUps != null)
          {
              gameState.savedAmmoPickUpsPositions.Clear();
              gameState.savedAmmoPickUpsRotations.Clear();

              for (int i = 0; i < ammoPickUpsArray.Length; i++)
              {
                  gameState.savedAmmoPickUpsPositions.Add(i, ammoPickUps[i].gameObject.transform.position);
                  gameState.savedAmmoPickUpsRotations.Add(i, ammoPickUps[i].gameObject.transform.rotation);
              }

              }
                   */
       
        
       



        savingSystem.SaveJson(gameState, "/GameData");


    }

    public void LoadGame()
    {
        savingSystem.LoadJson(gameState, "/GameData");
        //Player health & position
        playerTransform.position = gameState.playerPosition;
        playerTransform.rotation = gameState.playerRotation;
        playerHealth.maxPlayerHealth = gameState.playerMaxHealth;
        playerHealth.currentPlayerHealth = gameState.playerCurrentHealth;
        playerCamera.position = gameState.cameraPosition;
        playerCamera.rotation = gameState.cameraRotation;
        //Player level
        levellingSystem.level = gameState.playerLevel;
        levellingSystem.currentExp = gameState.currentEXP;
        levellingSystem.maxExp = gameState.maxEXP;
        playerHealth.playerDefense = gameState.playerDefense;
        upgradeSystem.levelPoints = gameState.levelPoints;
        upgradeSystem.playerDefenseUpgradeLevel = gameState.playerDefenseUpgradeLevel;
        upgradeSystem.playerSpeedUpgradeLevel = gameState.playerSpeedUpgradeLevel;
        upgradeSystem.buttonsUpgradeLevel = gameState.buttonsUpgradeLevel;
        playerMovement.playerSpeed = gameState.playerSpeed;

        //Buttons
        // lanceChargeButton.maxAmmo = gameState.lanceMaxAmmo;
        //lanceChargeButton.currentAmmo = gameState.lanceCurrentAmmo;
        // laserButton.currentEnergy = gameState.laserCurrentEnergy;
        // laserButton.maxEnergy = gameState.laserMaxEnergy;
        // shieldButton.currentEnergy = gameState.shieldCurrentEnergy;
        // shieldButton.maxEnergy = gameState.shieldMaxEnergy;
        // rocketLauncherButton.maxAmmo = gameState.rocketMaxAmmo;
        // rocketLauncherButton.currentAmmo = gameState.rocketCurrentAmmo;
        playerButtonInputs.equippedButton = (PlayerButtonInputs.EquippedButton)gameState.playerEquippedButton;
        savingSystem.LoadJson(laserButton, "/laserData");
        savingSystem.LoadJson(rocketLauncherButton, "/RocketData");
        savingSystem.LoadJson(lanceChargeButton, "/lanceButtonData");
        savingSystem.LoadJson(shieldButton, "/ShieldButtonData");

        gameFinished.enemyKills = gameState.enemyKills;
        //Enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyAIs[i].enemyState = (EnemyAI.EnemyState)gameState.enemyStates[i];
            enemyHealth[i].enemyHealth = gameState.enemyHealth[i];
            enemies[i].transform.position = gameState.enemyPosition[i];
            enemies[i].transform.rotation = gameState.enemyRotation[i];
            enemyAIs[i].enemyDead = gameState.enemyDead[i];
            
            enemyAIs[i].isPatrolling = gameState.enemyPatrolling[i];
            enemyAIs[i].isChasing = gameState.enemyChasing[i];
            enemyAIs[i].hasBeenAttacked = gameState.enemyBeenAttacked[i];
        }
        

        upgradeSystem.hasEnemyDebuffUpgrade = gameState.hasEnemyDebuffUpgrade;
        EnemyHealth.isLowDefenseUpgradeOn = gameState.hasEnemyDebuffUpgrade;
        buttonUiDisplay.timeRemaining = gameState.timeRemaining;
        deactivateMenus?.Invoke();

        /*  ammoPickUpsArray = FindObjectsOfType<AmmoPickUp>();
          if (ammoPickUps != null)
          {
              for (int i = 0; i < ammoPickUpsArray.Length; i++)
              {
                  Destroy(ammoPickUpsArray[i].gameObject);
              }
          }

          for (int i = 0; i < gameState.savedAmmoPickUpsPositions.Count; i++)
          {
              Instantiate(ammmoPickUpsPrefab, gameState.savedAmmoPickUpsPositions[i], gameState.savedAmmoPickUpsRotations[i]);
          } */

       


    }

    

}
