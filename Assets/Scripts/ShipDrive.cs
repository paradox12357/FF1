using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class ShipDrive : MonoBehaviour
{
    //public float speed;
    public float turnSpeed;
    public float gravMult;
    public float leftStrafeVal;
    public float rightStrafeVal;
    public bool grounded = false;
    private Rigidbody rb;
    public bool forward = false;
    public float groundedCheckDistance;
    //private float bufferCheckDistance = 0.1f;

    //variables for turning
    public float rotationSpeed = 5f;
    private Vector3 inputDirection;


    private FF1 ff1;
    private InputActionAsset inputActions;

    private Vector2 moveInput;
    //public int onGround = 0;
    // Start is called before the first frame update
    void Start()
    {
        ff1 = new FF1();
        inputActions = GetComponent<PlayerInput>().actions;
        rb = GetComponent<Rigidbody>();
        ff1.Player.Enable();
        inputActions.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        groundedCheckDistance = 1.1f;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundedCheckDistance))
        {
            grounded = true;//ray hits the ground
        }
        else
        {
            grounded = false;//ray does not hit the ground
        }
        Accelerate();
        Turn();
        Fall();
        
        //print("Torque: " + rb.GetAccumulatedTorque());
    }
    void Accelerate()
    {
        print("Velocity: " + rb.velocity.magnitude);
        //moveInput = ff1.Player.Move.ReadValue<Vector2>();
        moveInput = inputActions["Move"].ReadValue<Vector2>();
        //print("Move Input: " + moveInput.x + " " + moveInput.y);
        var Accelerate = inputActions["Accelerate"].ReadValue<float>();
        if (Mathf.Approximately(Accelerate, 1f))
        {
            rb.AddRelativeForce(new Vector3(0, 0, 75));
            forward = true;
            //print("Velocity: " + rb.velocity.magnitude);
        }
        var Decelerate = inputActions["Decelerate"].ReadValue<float>();
        if (Mathf.Approximately(Decelerate, 1f))
        {
            rb.AddRelativeForce(new Vector3(0, 0, -75));
            forward = false;
            //print("Velocity: " + rb.velocity.magnitude);
        }
        /*if ((UnityEngine.Input.GetKey(KeyCode.W) || ff1.Player.Accelerate.IsPressed()) && grounded == true)
        {
            rb.AddRelativeForce(new Vector3(0, 0, 75));
            forward = true;
            print("Velocity: " + rb.velocity.magnitude);
        }
        else if ((UnityEngine.Input.GetKey(KeyCode.S) || ff1.Player.Decelerate.IsPressed()) && (grounded == true))
        {
            rb.AddRelativeForce(new Vector3(0, 0, -75));
            forward = false;
            print("Velocity: -" + rb.velocity.magnitude);
        }*/

        if(rb.velocity.magnitude > 0 && forward == true)//Gradually Slow the Speedship down (when it's going forwards)
        {
            if (rb.velocity.magnitude > 75)
            {
                rb.AddRelativeForce(new Vector3(0, 0, -0.8f * rb.velocity.magnitude));
            }
            else
            {
                rb.AddRelativeForce(new Vector3(0, 0, -0.5f * rb.velocity.magnitude));
            }

        }
        if (rb.velocity.magnitude > 0 && forward == false)//Gradually Slow the Speedship down (when it's going backwards)
        {
            if (rb.velocity.magnitude > 75)
            {
                rb.AddRelativeForce(new Vector3(0, 0, 0.8f * rb.velocity.magnitude));
            }
            else
            {
                rb.AddRelativeForce(new Vector3(0, 0, 0.5f * rb.velocity.magnitude));
            }

        }

        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        localVel.x = 0;
        rb.velocity = transform.TransformDirection(localVel);

        var strafeRight = inputActions["StrafeRight"].ReadValue<float>();
        var strafeLeft = inputActions["StrafeLeft"].ReadValue<float>();
        if ((UnityEngine.Input.GetKey(KeyCode.R) || ff1.Player.StrafeRight.IsPressed()) && (UnityEngine.Input.GetKey(KeyCode.W) || ff1.Player.Accelerate.IsPressed()))//Strafing Right
        {
            if(rightStrafeVal < 15)
            {
                rightStrafeVal++;
            }
            rb.AddRelativeForce(new Vector3(rightStrafeVal * rb.velocity.magnitude, 0, 0));
        }
        if ((UnityEngine.Input.GetKey(KeyCode.Q) || ff1.Player.StrafeLeft.IsPressed()) && (UnityEngine.Input.GetKey(KeyCode.W) || ff1.Player.Accelerate.IsPressed()))//Strafing Left
        {
            if (leftStrafeVal < 15)
            {
                leftStrafeVal++;
            }
            rb.AddRelativeForce(new Vector3(-leftStrafeVal * rb.velocity.magnitude, 0, 0));
        }

        if(ff1.Player.StrafeLeft.WasReleasedThisFrame() || UnityEngine.Input.GetKeyUp(KeyCode.Q))
        {
            leftStrafeVal = 0;
            //print("LEFT STRAFE RESET!");
        }
        if (ff1.Player.StrafeRight.WasReleasedThisFrame() || UnityEngine.Input.GetKeyUp(KeyCode.R))
        {
            rightStrafeVal = 0;
            //print("RIGHT STRAFE RESET!");
        }
        /*while ((UnityEngine.Input.GetKey(KeyCode.R) || ff1.Player.StrafeRight.IsPressed()) && (UnityEngine.Input.GetKey(KeyCode.W) || ff1.Player.Accelerate.IsPressed()))//Strafing Right
        {
            if (strafeVal < 15f)
            {
                strafeVal += 1f;
            }
            rb.AddRelativeForce(new Vector3(strafeVal * rb.velocity.magnitude, 0, 0));
        }
        strafeVal = 0;
        while ((UnityEngine.Input.GetKey(KeyCode.Q) || ff1.Player.StrafeLeft.IsPressed()) && (UnityEngine.Input.GetKey(KeyCode.W) || ff1.Player.Accelerate.IsPressed()))//Strafing Left
        {
            if(strafeVal < 15f)
            {
                strafeVal += 1f;
            }
            rb.AddRelativeForce(new Vector3(-strafeVal * rb.velocity.magnitude, 0, 0));
        }
        strafeVal = 0;*/


    }
    void Turn()
    {

        if (grounded == true)
        {
            //rb.constraints = RigidbodyConstraints.None;
            //Quaternion deltaRotation = Quaternion.Euler(Vector3.up * moveInput.x * 5); //TAS TURNING
            //rb.MoveRotation(Quaternion.Lerp(rb.rotation, deltaRotation, Time.fixedDeltaTime * 5));
            //rb.MoveRotation(Quaternion.Lerp(rb.rotation, deltaRotation, Time.fixedDeltaTime * 5));
            //rb.MoveRotation(rb.rotation * deltaRotation); //TAS TURNING
            rb.AddTorque(Vector3.up * moveInput.x * turnSpeed);
        }
        if(grounded == false)
        {
            //rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0.1f, 0.1f, 0.1f)));
            //Quaternion deltaRotation = Quaternion.Euler(Vector3.up * moveInput.x * 5);
            //rb.MoveRotation(rb.rotation * deltaRotation);
            //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        
        //moveInput = inputActions["Move"].ReadValue<Vector2>();
        //float horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
        //float verticalInput = UnityEngine.Input.GetAxis("Vertical");
        /*inputDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        // Rotate towards input direction
        if (inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }*/
    }

    void Fall()
    {

        if (grounded == false)
        {
            //rb.rotation.x = 0; 
            rb.AddForce(Vector3.down * gravMult);
        }
        else if (grounded == true)
        {
            rb.AddForce(Vector3.down * 15);
        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }*/
}
