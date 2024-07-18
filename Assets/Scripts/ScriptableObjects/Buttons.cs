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
}
