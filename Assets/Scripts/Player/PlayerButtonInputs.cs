using Cinemachine.Utility;
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

    public GameObject rocketLauncher;

    public float rocketForce= 10f;

    public bool dealingLaserDamage = false;

    public float laserCooldownTime;
    public float shieldCooldownTime;
    public float lanceCooldownTime;

   // public Transform rocketLauncher;
    public Transform player;

    public GameObject lance;
    public bool canLanceCharge;
    public Animator lanceAnimator;

    public EquippedButton equippedButton;
    public bool canUseButtons;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        shield.SetActive(false);
        shield.GetComponent<Collider>().enabled = false;
        lanceAnimator = lance.GetComponent<Animator>();
        canLanceCharge = true;
        equippedButton = EquippedButton.Laser;
        canUseButtons = true;
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

        if (dealingLaserDamage) 
        {
            RaycastHit hit;

            if (Physics.Raycast(laserStartPoint.position, laserStartPoint.forward, out hit, laserRange))
            {
                
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();


                if (damageable != null)
                {
                    damageable.ReceiveDamage(25f * Time.deltaTime);
                }
               // else if (damageable == null)
                {
                   // dealingLaserDamage = false;
                }

            }

            else
            {
                dealingLaserDamage = false;
            }
        }

         if (laserButton.currentEnergy <= 0f)
        {
            isLaserButtonHeld = false;
            lineRenderer.enabled = false;
            dealingLaserDamage = false;
            lineRenderer.SetPosition(1, laserStartPoint.position);
            StartCoroutine(LaserCooldown());
        }

         if (shieldButton.currentEnergy <= 0f)
        {
            StartCoroutine(ShieldCooldown());
        }

        rocketLauncherButton.currentAmmo = Mathf.Clamp(rocketLauncherButton.currentAmmo, 0, rocketLauncherButton.maxAmmo);
        lanceButton.currentAmmo = Mathf.Clamp(lanceButton.currentAmmo, 0, lanceButton.maxAmmo);

        if (Input.GetKeyDown(KeyCode.Return))
        { 
            rocketLauncherButton.currentAmmo = rocketLauncherButton.maxAmmo;
            laserButton.currentEnergy = laserButton.maxEnergy;
            shieldButton.currentEnergy = shieldButton.maxEnergy;
            lanceButton.currentAmmo = lanceButton.maxAmmo;
        }

        //rocketLauncher.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        //player.transform.rotation = 

        switch (equippedButton)
        {
            case EquippedButton.Laser:
                rocketLauncher.SetActive(false);
                lance.SetActive(false);
                break;
            case EquippedButton.Lance:
                lance.SetActive(true);
                rocketLauncher.SetActive(false);
                break;
            case EquippedButton.Shield:
                rocketLauncher.SetActive(false);
                lance.SetActive(false);
                break;
            case EquippedButton.RocketLauncher:
                rocketLauncher.SetActive(true);
                lance.SetActive(false);
                break;

        }
    }

    public void ButtonUse()
    {
        
    }

    public void OnUseButton1(InputAction.CallbackContext context)
    {
        if (canUseButtons)
        {
            equippedButton = EquippedButton.RocketLauncher;
            if (rocketLauncherButton.currentAmmo > 0f && context.performed)
            {

                GameObject rockets = Instantiate(rocket, RocketLauncherRocketSpawn.position, rocket.transform.rotation);
                //Debug.Log(RocketLauncherRocketSpawn.transform.rotation);
                //Debug.Log("Rockets:" + rockets.transform.rotation);
                rockets.transform.rotation = transform.rotation;
                rockets.GetComponent<Rigidbody>().AddForce(RocketLauncherRocketSpawn.forward * rocketForce, ForceMode.Impulse);
                rocketLauncherButton.currentAmmo--;
                // rockets.transform.rotation = Quaternion.LookRotation(player.transform.forward, player.transform.up);

            }
        }
    }

    public void OnUseButton2(InputAction.CallbackContext context)
    {
        if (canUseButtons)
        {
            equippedButton = EquippedButton.Shield;
            //Instantiate(shield, shieldSpawn.transform.position, Quaternion.identity);
            if (context.performed)
            {
                isShieldButtonHeld = true;
                shield.SetActive(true);
                shield.GetComponent<Collider>().enabled = true;
            }

            else if (context.canceled)
            {
                isShieldButtonHeld = false;
                shield.SetActive(false);
                shield.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void OnUseButton3(InputAction.CallbackContext context)
    {
        if (canUseButtons)
        {
            equippedButton = EquippedButton.Laser;
            if (context.performed && laserButton.currentEnergy >= 0f)
            {
                isLaserButtonHeld = true;


                lineRenderer.enabled = true;
                Debug.Log("firingMyLaser!");

                //laserButton.currentEnergy -= laserEnergyRate * Time.deltaTime;

                lineRenderer.SetPosition(0, laserStartPoint.position);
                RaycastHit hit;

                if (Physics.Raycast(laserStartPoint.position, laserStartPoint.forward, out hit, laserRange))
                {
                    lineRenderer.SetPosition(1, hit.point);



                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();


                    if (damageable != null)
                    {
                        //damageable.ReceiveDamage(2f * Time.deltaTime);
                        dealingLaserDamage = true;
                    }



                }

                else
                {

                    lineRenderer.SetPosition(1, laserStartPoint.position + (laserStartPoint.forward * laserRange));
                    dealingLaserDamage = false;
                }
            }

            else if (context.canceled)
            {
                isLaserButtonHeld = false;
                lineRenderer.enabled = false;
                dealingLaserDamage = false;
                lineRenderer.SetPosition(1, laserStartPoint.position);
            }
        }
       
    }

    public void OnUseButton4(InputAction.CallbackContext context)
    {
        if (canUseButtons)
        {
            equippedButton = EquippedButton.Lance;
            if (context.performed && lanceButton.currentAmmo > 0f)
            {
                if (canLanceCharge)
                {
                    canLanceCharge = false;
                    lanceAnimator.SetTrigger("LanceAttack");
                    StartCoroutine(LanceChargeCooldown());
                    --lanceButton.currentAmmo;
                }
            }
        }
    }

    public IEnumerator LaserCooldown()
    {
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(laserCooldownTime);
        lineRenderer.enabled = true;
        laserButton.currentEnergy = laserButton.maxEnergy;
    }

    public IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(shieldCooldownTime);
        shieldButton.currentEnergy = shieldButton.maxEnergy;
    }

    public IEnumerator LanceChargeCooldown()
    {
        yield return new WaitForSeconds(lanceCooldownTime);
        canLanceCharge = true;
    }


    public enum EquippedButton
    {
        Laser,
        RocketLauncher,
        Shield,
        Lance
    }

    public void OnEnable()
    {
        PlayerMovement.displayPauseMenu += EnableButtons;
    }

    public void OnDisable()
    {
        PlayerMovement.displayPauseMenu -= EnableButtons;
    }

    public void EnableButtons()
    {
        canUseButtons = !canUseButtons;
    }
}
