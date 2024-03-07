using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerButtonInputs : MonoBehaviour, ButtonInputActions.IButtonsActions
{
    public Transform RocketLauncherRocketSpawn;

    public GameObject rocket;

   // public Camera playerCamera;

    public GameObject shield;
    

    public Transform laserStartPoint;
    public LineRenderer lineRenderer;

    public float laserRange;

    public Buttons laserButton;

    private float laserEnergyRate = 20f;

    public bool isLaserButtonHeld = false;

    public bool isShieldButtonHeld = false;

    public Buttons shieldButton;
    public Buttons rocketLauncherButton;
    public Buttons lanceButton;

    public LayerMask enemyLayer;

    public Rigidbody rocketPhysics;

    public float rocketForce= 10f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaserButtonHeld)
        {
            laserButton.currentEnergy -= laserEnergyRate * Time.deltaTime;
            laserButton.currentEnergy = Mathf.Clamp(laserButton.currentEnergy, 0, laserButton.maxEnergy);
            lineRenderer.SetPosition(0, laserStartPoint.position);
        }

        if (isShieldButtonHeld)
        {
            shieldButton.currentEnergy -= 2 * Time.deltaTime;
            shieldButton.currentEnergy = Mathf.Clamp(shieldButton.currentEnergy, 0, shieldButton.maxEnergy);
        }
    }

    public void ButtonUse()
    {
        
    }

    public void OnUseButton1(InputAction.CallbackContext context)
    {
        if (rocketLauncherButton.currentAmmo >= 0f)
        {
            Instantiate(rocket, RocketLauncherRocketSpawn.transform.position, Quaternion.identity);
            rocketPhysics.AddForce(RocketLauncherRocketSpawn.forward * rocketForce);
            rocketLauncherButton.currentAmmo--;
        }
    }

    public void OnUseButton2(InputAction.CallbackContext context)
    {
        //Instantiate(shield, shieldSpawn.transform.position, Quaternion.identity);
        if (context.performed)
        {
            isShieldButtonHeld = true;
            shield.SetActive(true);
        }

        else if (context.canceled)
        {
            shield.SetActive(false);
        }
    }

    public void OnUseButton3(InputAction.CallbackContext context)
    {
        if (context.performed && laserButton.currentEnergy >= 0f)
        {
            isLaserButtonHeld = true;
            
            
            lineRenderer.enabled = true;
            Debug.Log("firingMyLaser!");

            //laserButton.currentEnergy -= laserEnergyRate * Time.deltaTime;

            lineRenderer.SetPosition(0, laserStartPoint.position);
            RaycastHit hit;
            if (Physics.Raycast(laserStartPoint.position, laserStartPoint.forward, out hit, laserRange, enemyLayer))
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            else
            {
                
                lineRenderer.SetPosition(1, laserStartPoint.position + (laserStartPoint.forward * laserRange));
            }
        }

        else if (context.canceled)
        {
            isLaserButtonHeld = false;
            lineRenderer.enabled = false;
        }

        if (laserButton.currentEnergy <= 0f && context.canceled)
        {
            isLaserButtonHeld = false;
            lineRenderer.enabled = false;
            StartCoroutine(LaserCooldown());
        }
    }

    public void OnUseButton4(InputAction.CallbackContext context)
    {
        
    }

    public IEnumerator LaserCooldown()
    {
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(1);
        lineRenderer.enabled = true;
        laserButton.currentEnergy = laserButton.maxEnergy;
    }

}
