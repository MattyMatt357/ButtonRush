using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SavingAndLoadingLibrary;

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
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerTransform = GameObject.Find("Player").transform;
        savingSystem = GetComponent<SavingSystem>();
        playerCamera = GameObject.Find("Main Camera").transform;
        levellingSystem = FindObjectOfType<LevellingSystem>();
        playerButtonInputs = FindObjectOfType<PlayerButtonInputs>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemyAIs[i] = enemies[i].GetComponent<EnemyAI>();
            enemyHealth[i] = enemies[i].GetComponent<EnemyHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SaveGame();
        }

        if (Input.GetKey(KeyCode.Backspace))
        {
            LoadGame();
        }
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
        //Player level
        gameState.playerLevel = levellingSystem.level;
        gameState.maxEXP = levellingSystem.maxExp;
        gameState.currentEXP = levellingSystem.currentExp;
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
        gameState.playerEquippedButton = (int) playerButtonInputs.equippedButton;

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
            gameState.enemyStates[i] = (int) enemyAIs[i].enemyState;
            
        }
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
        //Enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyHealth[i].enemyHealth = gameState.enemyHealth[i];
            enemies[i].transform.position = gameState.enemyPosition[i];
            enemies[i].transform.rotation = gameState.enemyRotation[i];
            enemyAIs[i].enemyState = (EnemyAI.EnemyState) gameState.enemyStates[i];
            enemyAIs[i].isPatrolling = gameState.enemyPatrolling[i];
            enemyAIs[i].isChasing = gameState.enemyChasing[i];
        }


    }


}
