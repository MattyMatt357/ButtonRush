using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using GameplayLibrary.ChangingLevels;


public class MainMenuOptions : MonoBehaviour
{
    public LevelTransition levelTransition;
    public GameObject loadScreen;
    public Slider loadBar;
    public static bool isLoadedGame;
    public ChangeScenes changeScenes;
    // Start is called before the first frame update
    void Start()
    {
        levelTransition = GetComponent<LevelTransition>();
        loadScreen.SetActive(false);
        changeScenes = GetComponent<ChangeScenes>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        isLoadedGame = false;
        StartCoroutine(changeScenes.LevelChangeWithLoadingScreen("Level 1", loadScreen, loadBar));

    }

    public void LoadCurrentGame()
    {
        isLoadedGame = true;
        StartCoroutine(changeScenes.LevelChangeWithLoadingScreen("Level 1", loadScreen, loadBar));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
