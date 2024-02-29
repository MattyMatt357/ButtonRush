using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Button", menuName = "Button")]
public class Buttons : ScriptableObject
{
    public string ButtonName;
    public string ButtonDescription;
    public float currentEnergy;
    public float maxEnergy;
    public int currentAmmo;
    public int maxAmmo;
}
