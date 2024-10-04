using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            Vector3 cameraRotation = new Vector3(0, camera.transform.position.y,0);
            uiDamageTextObject.transform.rotation =
                
          Quaternion.LookRotation(uiDamageTextObject.transform.position - camera.transform.position);
            //uiDamageTextObject.transform.Rotate(0, uiDamageTextObject.transform.rotation.y,0);
        }
       
       
    }

    public void EnemyHealthBarDisplay(float currentHealth, float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
       
    }

    public void EnemyDamageTextDisplay(float damageToShow, Transform enemy)
    {
        if (uiDamageTextPrefab != null)
        {
           // Quaternion enemyRotation = enemy.rotation;
           // enemyRotation.x = 0;
            //enemyRotation.z = 0;
            uiDamageTextObject = Instantiate(uiDamageTextPrefab, enemy.position, Quaternion.identity);
           damageText = uiDamageTextObject.GetComponent<TextMeshPro>();
            damageText.text = damageToShow.ToString("F0");
        }
        
       
        //Destroy(uiDamageTextPrefab, 5f);
        
    }

    
}
