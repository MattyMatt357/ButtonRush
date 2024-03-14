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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        ButtonBarDisplay(laserSlider, laserButton);
        ButtonBarDisplay(shieldSlider, shieldButton);
        ButtonAmmoDisplay(rocketAmmoDisplay, rocketLauncherButton);
        ButtonAmmoDisplay(lanceAmmoDisplay, lanceChargeButton);
    }

    public void ButtonBarDisplay(Slider slider, Buttons button)
    {
        slider.minValue = 0;
        slider.value = button.currentEnergy;
        slider.maxValue = button.maxEnergy;
    }

    public void ButtonAmmoDisplay(TextMeshProUGUI ammoCount, Buttons button)
    {
        ammoCount.text = button.currentAmmo.ToString();
    }
}
