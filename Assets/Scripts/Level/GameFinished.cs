using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class GameFinished : MonoBehaviour
{
    public static event Action GameFinish;
    public static event Action GameOverWrongEnemyKills;
    public int enemyKills;
    // Start is called before the first frame update
    void Start()
    {
        //gameFinishedScreen.SetActive(false);
        // gameOverScreen.SetActive(false);
        if (MainMenuOptions.isLoadedGame == false)
        { 
            enemyKills = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }




    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyKills >= 30)
            {
                GameFinish?.Invoke();
            }
            else if (enemyKills < 30)
            {
                GameOverWrongEnemyKills?.Invoke();
            }
        }       
    }


}
