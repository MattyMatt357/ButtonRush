using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUiDisplay : MonoBehaviour
{
    public Buttons laserButton;
    public Slider laserSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LaserBarDisplay();
    }

    public void LaserBarDisplay()
    {
        laserSlider.minValue = 0;
        laserSlider.value = laserButton.currentEnergy;
        laserSlider.maxValue = laserButton.maxEnergy;
    }
}
