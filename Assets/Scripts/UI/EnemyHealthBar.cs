using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Camera camera;
    public Transform enemy;
    public GameObject uiDamageTextPrefab;
    private TextMeshPro damageText;
    Vector3 healthBarPositionOffset;
    GameObject uiDamageTextObject;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
       // healthBar.maxValue = maxHealthValue;
       //damageText = uiDamageTextPrefab.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        
        healthBarPositionOffset = new Vector3(0, 20, 0);
        transform.position = enemy.position + healthBarPositionOffset;
    }

    private void LateUpdate()
    {
        if (uiDamageTextObject != null)
        {
            //Vector3 cameraRotation = new Vector3(0, camera.transform.position.y,0);
           // uiDamageTextObject.transform.rotation = camera.transform.rotation;
                
          //Quaternion.LookRotation((uiDamageTextObject.transform.position - camera.transform.position).normalized);
            //uiDamageTextObject.transform.Rotate(0, uiDamageTextObject.transform.rotation.y,0);
        }
       
       
    }

    public void EnemyHealthBarDisplay(float currentHealth, float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
       
    }

    public void EnemyDamageTextDisplay(float damageToShow,
        ButtonDamageTypes buttonDamageTypes, ButtonDamageType damageType, bool isCritHit)
    {
        if (uiDamageTextPrefab != null)
        {
            // Quaternion enemyRotation = enemy.rotation;
            // enemyRotation.x = 0;
            //enemyRotation.z = 0;
            float randomX = Random.Range(15, 30);
            float randomY = Random.Range(15, 30);
            float randomZ = Random.Range(15, 20);
            uiDamageTextObject = Instantiate(uiDamageTextPrefab,
                enemy.position + new Vector3(randomX, randomY, randomZ), Quaternion.identity);
           damageText = uiDamageTextObject.GetComponent<TextMeshPro>();

            for (int i = 0; i< buttonDamageTypes.weaknessesAndResistances.Count; i++) 
            {
               if (buttonDamageTypes.weaknessesAndResistances[i].buttonDamageType == damageType)
                {
                    if(isCritHit == true)
                    {
                        damageText.color = Color.red;
                    }
                    else if (isCritHit == false)
                    {
                        damageText.color = Color.white;
                    }
                    if (buttonDamageTypes.weaknessesAndResistances[i].buttonResistances == ButtonResistances.Neutral)
                    {
                        damageText.text = damageToShow.ToString("F0");
                    }
                    else if (buttonDamageTypes.weaknessesAndResistances[i].buttonResistances == ButtonResistances.Weak)
                    {
                        damageText.text = "Weak " + damageToShow.ToString("F0");
                    }
                    else if (buttonDamageTypes.weaknessesAndResistances[i].buttonResistances == ButtonResistances.Resist)
                    {
                        damageText.text = "Resist " + damageToShow.ToString("F0");
                    }
                }


            }          
        }
        
       
        //Destroy(uiDamageTextPrefab, 5f);
        
    }

    public void EnemyDamageTextDisplay(float damageToShow, Transform enemy)
    {
        if (uiDamageTextPrefab != null)
        {
            float randomX = Random.Range(15,30);
            float randomY = Random.Range(15, 30);
            float randomZ = Random.Range(15, 20);
            // Quaternion enemyRotation = enemy.rotation;
            // enemyRotation.x = 0;
            //enemyRotation.z = 0;
            uiDamageTextObject = Instantiate(uiDamageTextPrefab,
                enemy.position + new Vector3(randomX, randomY, randomZ), Quaternion.identity);
            damageText = uiDamageTextObject.GetComponent<TextMeshPro>();

           
            damageText.text = damageToShow.ToString("F0");
        }
    }

    public void ShowStatusEffect(string statusEffect)
    {
        float randomX = Random.Range(15, 30);
        float randomY = Random.Range(15, 30);
        float randomZ = Random.Range(15, 20);
        uiDamageTextObject = Instantiate(uiDamageTextPrefab,
            enemy.position + new Vector3(randomX, randomY, randomZ),
            Quaternion.identity);

        damageText = uiDamageTextObject.GetComponent<TextMeshPro>();


        damageText.text = statusEffect;
    }
}


       

    



