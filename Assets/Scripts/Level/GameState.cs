using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public float playerCurrentHealth;
    public float playerMaxHealth;
    public Vector3 cameraPosition;
    public Quaternion cameraRotation;
    public int playerLevel;
    public int currentEXP;
    public int maxEXP;
    //Buttons
   /* public float laserCurrentEnergy;
    public float laserMaxEnergy;
    public int lanceMaxAmmo;
    public int lanceCurrentAmmo;
    public int rocketMaxAmmo;
    public int rocketCurrentAmmo;
    public float shieldMaxEnergy;
    public float shieldCurrentEnergy; */
    public int playerEquippedButton;
    // Enemies
    public float[] enemyHealth;
    public Vector3[] enemyPosition;
    public Quaternion[] enemyRotation;
    public int[] enemyStates;
    public bool[] enemyPatrolling;
    public bool[] enemyChasing;
    public int enemyKills;
   // public Buttons laserButton;
   // public Buttons lanceButton;
   // public Buttons rocketButton;
   // public Buttons shieldButton;
}

