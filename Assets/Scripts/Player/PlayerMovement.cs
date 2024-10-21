using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;
using Cinemachine;
using System;

public class PlayerMovement : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    public float playerSpeed;

    public Transform camera;

    float horizontalInput, verticalInput;

    public Rigidbody rigidbody;

    public Vector3 playerDirection;

    public Vector2 cameraRotation;

    public PauseMenu pauseMenu;

    public static event Action displayPauseMenu;

    public InputAction movement;
    public PlayerInputActions playerInputActions;

    public Transform enemyToLockOn;
    public CinemachineFreeLook cinemachineFreeLook;
    [Header("Lock On Camera")]
    public Transform cameraToLookAt;
    private bool isLockedOn =false;
    public GameObject cinemachineFollow;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject enemyLockOn;


    // Start is called before the first frame update
    void Start()
    {
        //Reference assignments
        rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
        pauseMenu = FindObjectOfType<PauseMenu>();
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        cameraToLookAt = GameObject.Find("CameraToLookAt").transform;
        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        //starting values
        SetCameraPriority(10,2);
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y,0);
        if(isLockedOn == true)
        {
            if (enemyToLockOn == null)
            {
                enemyLockOn = FindClosestEnemy();
            }
           
            if (enemyLockOn != null)
            {
                Debug.Log(enemyLockOn.transform.position);
                enemyToLockOn = enemyLockOn.transform;
                cinemachineFollow.transform.rotation = Quaternion.LookRotation
                    (enemyToLockOn.transform.position - transform.position);
                Debug.Log(enemyLockOn.transform.position);
            }
            else if (enemyToLockOn == null)
            {
                isLockedOn = false;
            }



        }

        else if (!isLockedOn)
        {
            cinemachineFollow.transform.rotation = Quaternion.Euler(0, 0, 0);
            enemyLockOn = null;
            enemyToLockOn = null;
        }
    }

    void FixedUpdate()
    {
        Vector3 playerMovement = new Vector3(playerDirection.x, 0.0f, playerDirection.y).normalized;
        float forward = 1.5f;
        playerMovement = (camera.forward * playerMovement.z * forward) + (camera.right * playerMovement.x);
        playerMovement.y = 0f;
        rigidbody.AddForce(playerMovement * playerSpeed * 1.5f * Time.deltaTime, ForceMode.Acceleration);
    }
   
    public void OnMove(InputAction.CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {   
        if(isLockedOn == false)
        {
            cameraRotation = context.ReadValue<Vector2>();
        }
       
    }

    public void OnDisplayPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            displayPauseMenu?.Invoke();
        }
    }

    public void OnLockOn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isLockedOn = true;
            SetCameraPriority(1, 10);
            Debug.Log("Locked on enemy");
           

        }
        else if (context.canceled)
        {
            isLockedOn = false;
            SetCameraPriority(10, 2);
        }
    }

    public GameObject FindClosestEnemy()
    {
       Vector3 position = transform.position;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");              
        GameObject closestEnemy = null;

        float distance = 100000;
        float infinity = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
          float distanceToEnemy = Vector3.SqrMagnitude(enemy.transform.position - position);
            if(distanceToEnemy <= distance && distanceToEnemy < infinity)
            {
               distance = distanceToEnemy;
               closestEnemy = enemy;
            }          
        }     
        return closestEnemy;

    }

    /// <summary>
    /// Sets the priority of the FreeLook and Virtual cameras
    /// </summary>
    /// <param name="freeLookCameraPriority"></param>
    /// <param name="virtualCameraPriority"></param>
    public void SetCameraPriority(int freeLookCameraPriority, int virtualCameraPriority)
    {
        cinemachineFreeLook.Priority = freeLookCameraPriority;
        virtualCamera.Priority = virtualCameraPriority;
    }
    void OnEnable()
    {
        SaveSystem.onLoadCameraPriority += SetCameraPriority;
        UpgradeSystem.playerSpeedUpgrade += IncreasePlayerSpeed;
    }
    public void OnDisable()
    {
        SaveSystem.onLoadCameraPriority -= SetCameraPriority;
        UpgradeSystem.playerSpeedUpgrade -= IncreasePlayerSpeed;
    }

    public void IncreasePlayerSpeed()
    {
        playerSpeed *= 1.1f;
    }
}
