using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevellingSystem : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int maxExp;
    public PlayerHealth playerHealth;

    //public delegate void PlayerLevelUpStats();
    public static event Action playerLevelUpStats;
    // Start is called before the first frame update
    void Start()
    {
        if (!MainMenuOptions.isLoadedGame)
        {
            level = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        level = Mathf.Clamp(level, 1, 15);
        
    }

    public void LevelUp()
    {
        if (level < 15)
        {
            level++;

            playerLevelUpStats?.Invoke();           
            maxExp = (int)(maxExp * 1.25);

        }
    }

    public void AddExp(int expAmount)
    {
       currentExp += expAmount;
       
       while (currentExp >= maxExp)
       {
            currentExp -= maxExp;
            LevelUp();
       }
    }
}
