using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ButtonUiDisplay : MonoBehaviour
{
    [Header("Laser button")]
    public Buttons laserButton;
    public Slider laserSlider;
    [Header("Shield button")]
    public Buttons shieldButton;
    public Slider shieldSlider;
 

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
    [Header("Levelling system UI")]
    public Slider expSlider;
    public TextMeshProUGUI playerLevelText;
    [Header("Button damage text")]
    public TextMeshProUGUI lanceDamageText;
    public TextMeshProUGUI laserDamageText;
    public TextMeshProUGUI rocketDamageText;
    [Header("Timer")]
    public float timeRemaining;
    public TextMeshProUGUI timerText;
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
        
        ShowPlayerLevel();
        DisplayTimer();

        DisplayButtonDamage(laserButton, laserDamageText, "Laser Damage: ");
        DisplayButtonDamage(rocketLauncherButton, rocketDamageText, "Rocket Damage: ");
        DisplayButtonDamage(lanceChargeButton, lanceDamageText, "Lance Damage: ");
    }

    private void FixedUpdate()
    {
        CrosshairDisplay();
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
            if (damageable == null) 
            {
                crosshair.color = Color.white;
            }
            else if (damageable != null)
            {
                crosshair.color = Color.red;
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

    public void DisplayTimer()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining < 0)
        {
            timeRemaining = 0;
        }

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
