using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Button Damage Resistances", menuName = "Button Damage Resistance")]
public class ButtonDamageTypes: ScriptableObject
{
    [System.Serializable]
    public struct WeaknessesAndResistances
    {
        public ButtonDamageType buttonDamageType;
        public ButtonResistances buttonResistances;
    }

    public List<WeaknessesAndResistances> weaknessesAndResistances = new List<WeaknessesAndResistances>();

    public float CalculateButtonDamageResistance
        (float damage, ButtonDamageType buttonDamageTypes)
    {
        for (int i = 0; i < weaknessesAndResistances.Count; i++)
        {
            if (weaknessesAndResistances[i].buttonDamageType == buttonDamageTypes)
            {
                if (weaknessesAndResistances[i].buttonResistances == ButtonResistances.Neutral)
                {
                    return damage;
                }
                else if (weaknessesAndResistances[i].buttonResistances == ButtonResistances.Weak)
                {
                    return damage * 1.15f;
                }
                else if (weaknessesAndResistances[i].buttonResistances == ButtonResistances.Resist)
                {
                    return damage * 0.85f;
                }

            }
        }
        return 0;
    }
}
public enum ButtonDamageType
{
    Rocket = 0,
    Laser = 1,
    Lance= 2
}

//WeaknessAndResistances
public enum ButtonResistances
{ 
    Weak = 0,
    Neutral = 1,
    Resist = 2
}
