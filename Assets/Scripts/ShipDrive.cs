using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class ShipDrive : MonoBehaviour
{
    //public float speed;
    public float turnSpeed;
    public float gravMult;
    //public float strafeVal;
    public bool grounded = false;
    private Rigidbody rb;
    public bool forward = false;
    public float groundedCheckDistance;
    //private float bufferCheckDistance = 0.1f;

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
        Accelerate();
        Turn();
        Fall();
        groundedCheckDistance = 1.1f;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -transform.up, out hit, groundedCheckDistance))
        {
            grounded = true;//ray hits the ground
        }
        else
        {
            grounded = false;//ray does not hit the ground
        }
        //print("Torque: " + rb.GetAccumulatedTorque());
    }
    void Accelerate()
    {
        //moveInput = ff1.Player.Move.ReadValue<Vector2>();
        moveInput = inputActions["Move"].ReadValue<Vector2>();
        //print("Move Input: " + moveInput.x + " " + moveInput.y);
        if ((UnityEngine.Input.GetKey(KeyCode.W) || ff1.Player.Accelerate.IsPressed()) && grounded == true)
        {
            rb.AddRelativeForce(new Vector3(0, 0, 100));
            forward = true;
            //print("Velocity: " + rb.velocity.magnitude);
        }
        else if ((UnityEngine.Input.GetKey(KeyCode.S) || ff1.Player.Decelerate.IsPressed()) && (grounded == true))
        {
            rb.AddRelativeForce(new Vector3(0, 0, -100));
            forward = false;
            //print("Velocity: -" + rb.velocity.magnitude);
        }

        if(rb.velocity.magnitude > 0 && forward == true)//Gradually Slow the Speedship down (when it's going forwards)
        {
            if (rb.velocity.magnitude > 100)
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
            if (rb.velocity.magnitude > 100)
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

        if((UnityEngine.Input.GetKey(KeyCode.R) || ff1.Player.StrafeRight.IsPressed()) && (UnityEngine.Input.GetKey(KeyCode.W) || ff1.Player.Accelerate.IsPressed()))//Strafing Right
        {
            rb.AddRelativeForce(new Vector3(15 * rb.velocity.magnitude, 0, 0));
        }

        if ((UnityEngine.Input.GetKey(KeyCode.Q) || ff1.Player.StrafeLeft.IsPressed()) && (UnityEngine.Input.GetKey(KeyCode.W) || ff1.Player.Accelerate.IsPressed()))//Strafing Left
        {
            rb.AddRelativeForce(new Vector3(-15 * rb.velocity.magnitude, 0, 0));
        }

    }
    void Turn()
    {

        if (/*UnityEngine.Input.GetKey(KeyCode.D)*/ moveInput.x > 0 && grounded == true)
        {
            rb.AddTorque(Vector3.up * turnSpeed);
        }
        if (/*UnityEngine.Input.GetKey(KeyCode.A)*/ moveInput.x < 0 && grounded == true)
        {
            rb.AddTorque(-Vector3.up * turnSpeed);

        }
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
