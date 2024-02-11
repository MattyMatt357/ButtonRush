using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;

    public Transform orientation;

    float horizontalInput, verticalInput;

    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMovementInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void MovingPlayer()
    {
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rigidbody.AddForce(moveDirection.normalized * playerSpeed, ForceMode.Force);
    }
}
