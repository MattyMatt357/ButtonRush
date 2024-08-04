using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
        pauseMenu = FindObjectOfType<PauseMenu>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, camera.transform.eulerAngles.y,0);
        Vector3 playerMovement = new Vector3(playerDirection.x, 0.0f, playerDirection.z).normalized;
        float forward = 1.5f;
        playerMovement = (camera.forward * playerMovement.z * forward) + camera.right * playerMovement.x;
        rigidbody.AddForce(playerMovement * playerSpeed * 1.5f * Time.deltaTime, ForceMode.Acceleration);
    }
   
    public void OnMove(InputAction.CallbackContext context)
    {      
        playerDirection = context.ReadValue<Vector3>();     
    }

    public void OnLook(InputAction.CallbackContext context)
    {      
        cameraRotation = context.ReadValue<Vector2>();
    }   

    public void OnDisplayPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            displayPauseMenu?.Invoke();
        }
    }
}
