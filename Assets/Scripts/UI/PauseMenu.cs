using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    bool pauseMenuDisplayed;
    public TextMeshProUGUI playerExpDisplay;
    public LevellingSystem levellingSystem;
    public LevelTransition levelTransition;
    public GameObject loadScreen;
    public Slider loadBar;
    //GameFinished/Over screens
    public GameObject gameFinishedScreen;
    public GameObject gameOverScreen;
   // public Button SaveGameButton;
   // public Button LoadGameButton;
   public PlayerButtonInputs playerButtonInputs;
    public TextMeshProUGUI enemyKillsText;
    //Initial selected buttons
    public Button PauseMenuSaveButton;
    public Button GameOverScreenLoadButton;
    public Button GameFinishedGoToMainMenuButton;

    public GameObject upgradeMenu;
    [FormerlySerializedAs("firstUpgradeButton")]
    public Button playerDefenseUpgradeButton;
    public Button playerSpeedUpgradeButton;
    public Toggle enemyDebuffToggle;
    public Button buttonsUpgradeButton;
    public Button backToPauseMenuButton;
    public GameFinished gameFinished;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        pauseMenuDisplayed = false;
        levellingSystem = FindObjectOfType<LevellingSystem>();
        levelTransition = GetComponent<LevelTransition>();
        gameFinishedScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        // SaveGameButton.gameObject.SetActive(false);
        // LoadGameButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
        playerButtonInputs = FindObjectOfType<PlayerButtonInputs>();
        upgradeMenu.SetActive(false);
        gameFinished = FindObjectOfType<GameFinished>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyKillsText.text = "Enemy Kills: " + gameFinished.enemyKills.ToString();

        playerExpDisplay.text = levellingSystem.currentExp.ToString() + "/" + levellingSystem.maxExp.ToString();
    }

    public void DisplayingPauseMenu()
    {
        if (pauseMenuDisplayed)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
           // SaveGameButton.gameObject.SetActive(true);
           PauseMenuSaveButton.Select();
        }

        else if (!pauseMenuDisplayed)
        {
            pauseMenu.SetActive(false);
            upgradeMenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SwitchingPauseMenu()
    {
       /* if (pauseMenuDisplayed)
        { 
            pauseMenuDisplayed = false;
        }

        else if (!pauseMenuDisplayed)
        {
            pauseMenuDisplayed = true;
        } */

        pauseMenuDisplayed = !pauseMenuDisplayed;

        DisplayingPauseMenu();
    }

    public void OnEnable()
    {
        PlayerMovement.displayPauseMenu += SwitchingPauseMenu;
        PlayerHealth.playerDefeat += DisplayGameOver;
        GameFinished.GameFinish += DisplayGameFinished;
        SaveSystem.deactivateMenus += MenusDeactvated;
        GameFinished.GameOverWrongEnemyKills += DisplayGameOver;
        UpgradeSystem.backToPauseMenu += BackToPauseMenu;
        ButtonUiDisplay.runOutOfTime += DisplayGameOver;
    }

    public void OnDisable()
    {
        PlayerMovement.displayPauseMenu -= SwitchingPauseMenu;
        PlayerHealth.playerDefeat -= DisplayGameOver;
        GameFinished.GameFinish -= DisplayGameFinished;
        SaveSystem.deactivateMenus -= MenusDeactvated;
        GameFinished.GameOverWrongEnemyKills -= DisplayGameOver;
        UpgradeSystem.backToPauseMenu -= BackToPauseMenu;
        ButtonUiDisplay.runOutOfTime -= DisplayGameOver;
    }

    public void ExitToMainMenu()
    {
        StartCoroutine(levelTransition.LevelChange("Main Menu", loadScreen, loadBar));
    }

    

    public void DisplayGameOver()
    {
        pauseMenuDisplayed = true;
        DisplayingPauseMenu();
        gameOverScreen.SetActive(true);
        pauseMenu.SetActive(false);
        GameOverScreenLoadButton.Select();
    }

    public void DisplayGameFinished()
    {
        pauseMenuDisplayed = true;
        DisplayingPauseMenu();
        pauseMenu.SetActive(false);
        //SaveGameButton.gameObject.SetActive(false);
        
        gameFinishedScreen.SetActive(true);
        GameFinishedGoToMainMenuButton.Select();
    }

    public void MenusDeactvated()
    {
        gameFinishedScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseMenuDisplayed = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        playerButtonInputs.canUseButtons = true;
    }

    public void DisplayUpgradeMenu()
    {
        pauseMenu.SetActive(false);
        upgradeMenu.SetActive(true);
        if(playerDefenseUpgradeButton.interactable == true)
        {
            playerDefenseUpgradeButton.Select();
        }
        else if (playerDefenseUpgradeButton.interactable == false)
        {
            playerSpeedUpgradeButton.Select();
        }
        else if (playerSpeedUpgradeButton.interactable == false)
        {
            enemyDebuffToggle.Select();
        }
        else if (enemyDebuffToggle.interactable == false)
        {
            buttonsUpgradeButton.Select();
        }
        else if (buttonsUpgradeButton.interactable == false)
        {
            backToPauseMenuButton.Select();
        }

        
       
       // if ()

    }

    public void BackToPauseMenu()
    {
        pauseMenu.SetActive(true);
        upgradeMenu.SetActive(false);
        PauseMenuSaveButton.Select();
    }
}
