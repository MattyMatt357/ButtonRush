using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        pauseMenuDisplayed = false;
        levellingSystem = FindObjectOfType<LevellingSystem>();
        levelTransition = GetComponent<LevelTransition>();
        gameFinishedScreen.SetActive(false);
        gameOverScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        DisplayingPauseMenu();

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
        }

        else if (!pauseMenuDisplayed)
        {
            pauseMenu.SetActive(false);
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
    }

    public void OnEnable()
    {
        PlayerMovement.displayPauseMenu += SwitchingPauseMenu;
        PlayerHealth.playerDefeat += SwitchingPauseMenu;
        GameFinished.GameFinish += SwitchingPauseMenu;
    }

    public void OnDisable()
    {
        PlayerMovement.displayPauseMenu -= SwitchingPauseMenu;
        PlayerHealth.playerDefeat -= SwitchingPauseMenu;
        GameFinished.GameFinish -= SwitchingPauseMenu;
    }

    public void ExitToMainMenu()
    {
        StartCoroutine(levelTransition.LevelChange("Main Menu", loadScreen, loadBar));
    }

    

    public void DisplayGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisplayGameFinished()
    { 
        gameFinishedScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
