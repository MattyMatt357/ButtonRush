using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Button", menuName = "Button")]
[System.Serializable]
public class Buttons : ScriptableObject
{
    public string ButtonName;
    public string ButtonDescription;
    public float buttonDamage;
    public float currentEnergy;
    public float maxEnergy;
    public int currentAmmo;
    public int maxAmmo;

    public void SetDamageForOperator(float damage)
    {
        buttonDamage = damage;              
    }

    

    /// <summary>
    /// For increasing damage and current ammo for rocket launcher button
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
     public static Buttons operator ++(Buttons button)
     {
         button.buttonDamage += 5;
       
        button.maxAmmo += 5;
        return button;
     }

    /// <summary>
    /// For increasing damage and current ammo for lance charge button
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public static Buttons operator -(Buttons button)
    {
        button.buttonDamage += 10;
        button.maxAmmo += 5;
        return button;
    }

    /// <summary>
    /// To increase the damage and max energy of the laser button
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public static Buttons operator --(Buttons button)
    {
        button.buttonDamage += 5;
        button.maxEnergy += 50;
        return button;
    }

  /* public static implicit operator Buttons(float floatToConvert)
    {
        Buttons button = CreateInstance<Buttons>();
        button.SetDamageForOperator(floatToConvert);
        return button;
    }*/

    public static Buttons operator +(Buttons button, float damage)
    {
        button.buttonDamage += damage;
        button.maxAmmo += 5;
        return button;
    }

    public static Buttons operator ^(Buttons button, float buttonMaxEnergy)
    {
        button.maxEnergy += buttonMaxEnergy;
        button.buttonDamage += 5;
        return button;
    }
}
