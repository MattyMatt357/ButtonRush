using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameFinished : MonoBehaviour
{
    public delegate void GameFinishedCallback();
    public static event GameFinishedCallback GameFinish;
    // Start is called before the first frame update
    void Start()
    {
        //gameFinishedScreen.SetActive(false);
       // gameOverScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameFinish?.Invoke();
        }
    }

    

   
}
