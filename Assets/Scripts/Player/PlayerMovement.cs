using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    public float playerSpeed;

    public Transform orientation;

    float horizontalInput, verticalInput;

    public Rigidbody rigidbody;

    public Vector3 playerDirection;

    public Vector2 cameraRotation;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(playerDirection * playerSpeed * Time.deltaTime, ForceMode.Force);
    }

    

    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<Vector3>());
        playerDirection = context.ReadValue<Vector3>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        cameraRotation = context.ReadValue<Vector2>();
    }   
}
