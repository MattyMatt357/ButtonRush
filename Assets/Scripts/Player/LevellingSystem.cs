using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevellingSystem : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int maxExp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        level++;
    }

    public void AddExp(int expAmount)
    {
        currentExp += expAmount;
       
        if (currentExp >= maxExp)
        {
            currentExp -= maxExp;
        }
    }
}
