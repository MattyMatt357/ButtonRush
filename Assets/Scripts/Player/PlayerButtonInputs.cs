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
    public Transform shieldSpawn;

    public Transform laserStartPoint;
    public LineRenderer lineRenderer;

    public float laserRange;

    public Buttons laserButton;

    private float laserEnergyRate;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonUse()
    {
        
    }

    public void OnUseButton1(InputAction.CallbackContext context)
    {
        Instantiate(rocket, RocketLauncherRocketSpawn.transform.position, Quaternion.identity);
    }

    public void OnUseButton2(InputAction.CallbackContext context)
    {
        Instantiate(shield, shieldSpawn.transform.position, Quaternion.identity);
    }

    public void OnUseButton3(InputAction.CallbackContext context)
    {
        if (context.performed && laserButton.currentEnergy >= 0f)
        {
            
            lineRenderer.enabled = true;
            Debug.Log("firingMyLaser!");

            laserButton.currentEnergy -= Time.deltaTime * 20f;

            lineRenderer.SetPosition(0, laserStartPoint.position);
            RaycastHit hit;
            if (Physics.Raycast(laserStartPoint.position, laserStartPoint.forward, out hit, laserRange))
            {
                lineRenderer.SetPosition(1, hit.point);
            }

            else
            {
                lineRenderer.SetPosition(1, laserStartPoint.position + (laserStartPoint.forward * laserRange));
            }
        }

        else if (context.canceled || laserButton.currentEnergy <= 0f)
        {
            lineRenderer.enabled = false;
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
    }

}
