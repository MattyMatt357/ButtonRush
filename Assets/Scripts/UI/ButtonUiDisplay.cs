using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ButtonUiDisplay : MonoBehaviour
{
    public Buttons laserButton;
    public Slider laserSlider;

    public Buttons shieldButton;
    public Slider shieldSlider;

    public int rocketAmmo;
    public int LanceAmmo;

    public TextMeshProUGUI rocketAmmoDisplay;
    public TextMeshProUGUI lanceAmmoDisplay;
    public Buttons rocketLauncherButton;
    public Buttons lanceChargeButton;
    public TextMeshProUGUI playerHealthText;
    public PlayerHealth playerHealth;

    public Image crosshair;

    public LayerMask enemyLayer;

    public Camera camera;

    public LevellingSystem levellingSystem;

    public Slider expSlider;
    public TextMeshProUGUI playerLevelText;

    public TextMeshProUGUI lanceDamageText;
    public TextMeshProUGUI laserDamageText;
    public TextMeshProUGUI rocketDamageText;
   // public Sprite crosshair;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        levellingSystem = FindObjectOfType<LevellingSystem>();
        //camera = FindObjectOfType<Camera>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
        ButtonBarDisplay(laserSlider, laserButton);
        ButtonBarDisplay(shieldSlider, shieldButton);
        ButtonAmmoDisplay(rocketAmmoDisplay, rocketLauncherButton);
        ButtonAmmoDisplay(lanceAmmoDisplay, lanceChargeButton);
        playerHealthText.text = "Health: " + playerHealth.currentPlayerHealth.ToString("F0")
            + "/" + playerHealth.maxPlayerHealth.ToString("F0");
        CrosshairDisplay();
        ShowPlayerLevel();

        DisplayButtonDamage(laserButton, laserDamageText, "Laser Damage: ");
        DisplayButtonDamage(rocketLauncherButton, rocketDamageText, "Rocket Damage: ");
        DisplayButtonDamage(lanceChargeButton, lanceDamageText, "Lance Damage: ");
    }

    public void ButtonBarDisplay(Slider slider, Buttons button)
    {
        slider.minValue = 0;
        slider.value = button.currentEnergy;
        slider.maxValue = button.maxEnergy;
    }

    public void ButtonAmmoDisplay(TextMeshProUGUI ammoCount, Buttons button)
    {
        ammoCount.text = button.currentAmmo.ToString() + "/" + button.maxAmmo.ToString();
    }

    public void CrosshairDisplay()
    {
        

        Ray crosshairAim = new Ray(camera.transform.position, camera.transform.forward);

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 250, enemyLayer))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null) 
            {
                crosshair.color = Color.red;
            }
            else 
            {
                crosshair.color = Color.white;
            }
        }

        

        
    }

    public void ShowPlayerLevel()
    {
        playerLevelText.text = "Level: " + levellingSystem.level;
        expSlider.minValue = 0;
        expSlider.value = levellingSystem.currentExp;
        expSlider.maxValue = levellingSystem.maxExp;

    }

    public void DisplayButtonDamage(Buttons button, TextMeshProUGUI damageText, string buttonDamageText)
    { 
        damageText.text = buttonDamageText + button.buttonDamage.ToString();
    }
}
