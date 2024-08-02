using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevellingSystem : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int maxExp;
    public PlayerHealth playerHealth;

    public delegate void PlayerLevelUpStats();
    public static event PlayerLevelUpStats playerLevelUpStats;
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
        level++;

        playerLevelUpStats?.Invoke();

        //playerHealth.maxPlayerHealth *= 1.15f;
       // playerHealth.currentPlayerHealth = playerHealth.maxPlayerHealth;
       // currentExp = 0;
        maxExp = (int) (maxExp * 1.25);

        /*if (currentExp > maxExp)
        {
           // int expRem = currentExp - maxExp;
           // maxExp -= currentExp;
           int overMaxExp = currentExp - maxExp;
            currentExp = 0;
            currentExp = overMaxExp;
        } */
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
