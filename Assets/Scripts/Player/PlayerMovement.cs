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

    public delegate void DisplayPauseMenu();
    public static event DisplayPauseMenu displayPauseMenu;

    public InputAction movement;
    public PlayerInputActions playerInputActions;

    public Transform enemyToLockOn;
    public CinemachineFreeLook cinemachineFreeLook;
    public Transform cameraToLookAt;
    private bool isLockedOn =false;
    public GameObject cinemachineFollow;
    void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        movement = playerInputActions.Player.Move;
        playerInputActions.Player.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
        pauseMenu = FindObjectOfType<PauseMenu>();
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        cameraToLookAt = GameObject.Find("CameraToLookAt").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //camera.transform.LookAt(Vector3.zero);
        transform.rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y,0);
        if(isLockedOn == true)
        {

           
            // camera.transform.LookAt
            if(isLockedOn)
            {
                cameraRotation = new Vector2(0f,0f);
                GameObject enemyLockOn = FindClosestEnemy();
                enemyToLockOn = enemyLockOn.transform;
                cinemachineFollow.transform.rotation = Quaternion.LookRotation
                    (enemyToLockOn.transform.position - transform.position);

            }
           
        }
       
    }

    void FixedUpdate()
    {
        Vector3 playerMovement = new Vector3(playerDirection.x, 0.0f, playerDirection.y).normalized;
        float forward = 1.5f;
        playerMovement = (camera.forward * playerMovement.z * forward) + (camera.right * playerMovement.x);
        playerMovement.y = 0f;
        rigidbody.AddForce(playerMovement * playerSpeed * 1.5f * Time.deltaTime, ForceMode.Acceleration);

        
       // rigidbody.AddForce(playerDirection, ForceMode.Impulse);
       // playerDirection = Vector3.zero;

       

    }
   
    public void OnMove(InputAction.CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>();
       // playerDirection = context.ReadValue<Vector2>().y * CameraRight() * playerSpeed;
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
            Debug.Log("Locked on enemy");

            {
                //cinemachineFreeLook.LookAt = enemyToLockOn.transform;
                // cinemachineFreeLook.m_YAxis.m_InputAxisValue = enemyToLockOn.transform.position.z;
                //cinemachineFreeLook.m_XAxis.m_InputAxisValue = enemyToLockOn.transform.position.x;
            }


            // StartCoroutine(LookAtTarget());
            /* Vector3 lockOnPosition = new Vector3(FindClosestEnemy().transform.position.x,
                 0, FindClosestEnemy().transform.position.z);
             enemyToLockOn.position = lockOnPosition;

            

             
         }*/

            if (context.canceled)
            {
                //cinemachineFreeLook.LookAt = cameraToLookAt;
                isLockedOn = false;
            }
        }
    }

    public GameObject FindClosestEnemy()
    {
        Vector3 position = transform.position;
        return GameObject.FindGameObjectsWithTag("Enemy").
            OrderBy(o => (o.transform.position-position).sqrMagnitude).FirstOrDefault();
    }

    IEnumerator LookAtTarget()
    {
        GameObject enemyLockOn = FindClosestEnemy();
        camera.transform.LookAt(enemyLockOn.transform);
       // Vector3 target = transform.position + Vector3.up;
       // Vector3 pivotPoint = Vector3.MoveTowards
          //  (transform.position, target, Vector3.Distance(camera.transform.position, target) * 5);
       // camera.position = pivotPoint;
        //camera.transform.LookAt((enemyLockOn.transform.position + transform.position)/2);
        //camera.position -= camera.transform.forward * 2;
        
       // Vector3 lockOnDirection = camera.position - enemyLockOn.transform.position;
       // camera.rotation = Quaternion.LookRotation(lockOnDirection);
        yield return null;
    }
}
