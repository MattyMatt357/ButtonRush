using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Button", menuName = "Button")]
public class Buttons : ScriptableObject
{
    public string ButtonName;
    public string ButtonDescription;
    public int currentEnergy;
    public int maxEnergy;
    public int currentAmmo;
    public int maxAmmo;
}
