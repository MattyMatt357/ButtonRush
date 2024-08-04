using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameFinished : MonoBehaviour
{
    public delegate void GameFinishedCallback();
    public static event GameFinishedCallback GameFinish;
    public delegate void GameOverWrongKills();
    public static event GameOverWrongKills GameOverWrongEnemyKills;
    public static int enemyKills;
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
