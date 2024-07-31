using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameFinished : MonoBehaviour
{
    public GameObject gameFinishedScreen;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        gameFinishedScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameFinishedScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void DisplayGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnEnable()
    {
        PlayerHealth.playerDefeat += DisplayGameOver;
    }

    public void OnDisable() 
    {
        PlayerHealth.playerDefeat -= DisplayGameOver;
    }
}
