using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MainMenuOptions : MonoBehaviour
{
    public LevelTransition levelTransition;
    public GameObject loadScreen;
    public Slider loadBar;
    public static bool isLoadedGame;
    // Start is called before the first frame update
    void Start()
    {
        levelTransition = GetComponent<LevelTransition>();
        loadScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        isLoadedGame = false;
        StartCoroutine(levelTransition.LevelChange("Level 1", loadScreen, loadBar));
        
    }

    public void LoadCurrentGame()
    {
        isLoadedGame = true;
        StartCoroutine(levelTransition.LevelChange("Level 1", loadScreen, loadBar));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
