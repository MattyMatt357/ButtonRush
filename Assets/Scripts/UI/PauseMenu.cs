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

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        pauseMenuDisplayed = false;
        levellingSystem = FindObjectOfType<LevellingSystem>();

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
    }

    public void OnDisable()
    {
        PlayerMovement.displayPauseMenu -= SwitchingPauseMenu;
    }
}
