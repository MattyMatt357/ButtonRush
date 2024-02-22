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

    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("firingMyLaser!");
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, laserStartPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(laserStartPoint.position, laserStartPoint.forward, out hit))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    public void OnUseButton4(InputAction.CallbackContext context)
    {
        
    }


}
