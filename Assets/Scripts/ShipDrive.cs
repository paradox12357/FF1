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
    public Vector3 currentEulerAngles;
    //private float bufferCheckDistance = 0.1f;
    //public Vector3 forwardDirection = Vector3.forward;

    private FF1 ff1;
    private InputActionAsset inputActions;

    private Vector2 moveInput;
    //private
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
        if(Physics.Raycast(transform.position, -transform.up, out hit, groundedCheckDistance))
        {
            grounded = true;//ray hits the ground
        }
        else
        {
            grounded = false;//ray does not hit the ground
        }
        //print("Torque: " + rb.GetAccumulatedTorque());
        Accelerate();
        Turn();
        Fall();
    }
    void Accelerate()
    {
        //moveInput = ff1.Player.Move.ReadValue<Vector2>();
        moveInput = inputActions["Move"].ReadValue<Vector2>();
        var Accelerate = inputActions["Accelerate"].ReadValue<float>();
        var Decelerate = inputActions["Decelerate"].ReadValue<float>();
        //print("Move Input: " + moveInput.x + " " + moveInput.y);
        if (/*(UnityEngine.Input.GetKey(KeyCode.W) || */ /*ff1.Player.Accelerate.IsPressed()*/ Mathf.Approximately(Accelerate, 1f) && grounded == true)
        {
            rb.AddRelativeForce(new Vector3(0, 0, 100));
            forward = true;
            //print("Velocity: " + rb.velocity.magnitude);
        }
        else if (/*(UnityEngine.Input.GetKey(KeyCode.S) ||*/ Mathf.Approximately(Decelerate, 1f) && (grounded == true))
        {
            rb.AddRelativeForce(new Vector3(0, 0, -80));
            forward = false;
            //print("Velocity: -" + rb.velocity.magnitude);
        }

        if(rb.velocity.magnitude > 0 && forward == true)//Gradually Slow the Speedship down (when it's going forwards)
        {
            if (rb.velocity.magnitude > 75)
            {
                rb.AddRelativeForce(new Vector3(0, 0, -0.8f * rb.velocity.magnitude));
                //print("SLOWWWWWWWW");
            }
            else
            {
                rb.AddRelativeForce(new Vector3(0, 0, -0.5f * rb.velocity.magnitude));
            }

        }
        if (rb.velocity.magnitude > 0 && forward == false)//Gradually Slow the Speedship down (when it's going backwards)
        {
            if (rb.velocity.magnitude > 25)
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
        var StrafeRight = inputActions["StrafeRight"].ReadValue<float>();
        var StrafeLeft = inputActions["StrafeLeft"].ReadValue<float>();

        if (/*(UnityEngine.Input.GetKey(KeyCode.R) ||*/ Mathf.Approximately(StrafeRight, 1f)/*(UnityEngine.Input.GetKey(KeyCode.W) ||*/)//Strafing Right
        {
            rb.AddRelativeForce(new Vector3((15 * rb.velocity.magnitude) - rb.velocity.x, 0, 0)); 
        }

        if (/*(UnityEngine.Input.GetKey(KeyCode.Q) ||*/ Mathf.Approximately(StrafeLeft, 1f) /*(UnityEngine.Input.GetKey(KeyCode.W) ||*/)//Strafing Left // Mathf.Approximately(Accelerate, 1f)
        {
            rb.AddRelativeForce(new Vector3((-15 * rb.velocity.magnitude) - rb.velocity.x, 0, 0));
        }

    }
    void Turn()
    {

        if (moveInput.x > 0 && grounded == true)
        {
            rb.AddTorque(Vector3.up * turnSpeed);
            //print("LMAOAOAAOAOAOAOAOAOAO");
        }
        if (moveInput.x < 0 && grounded == true)
        {
            rb.AddTorque(-Vector3.up * turnSpeed);

        }
        //currentEulerAngles += new Vector3(moveInput.x, 0, 0) * Time.deltaTime * 1.0f;
        //transform.eulerAngles = currentEulerAngles;

        //Quaternion deltaRotation = Quaternion.Euler(Vector3.up * moveInput.x * turnSpeed);
        //Quaternion deltaRotation = Quaternion.Euler(Vector3.up * moveInput.x * turnSpeed);
        //rb.MoveRotation(rb.rotation * deltaRotation);
        //rb.MoveRotation(rb.rotation * deltaRotation);
        //rb.rotation = deltaRotation; 
        //Vector3 driftForce = transform.right * moveInput.x * turnSpeed * 0.5f;
        //rb.AddForce(driftForce, ForceMode.Acceleration);
    }

    void Fall()
    {

        if (grounded == false)
        {
            //rb.rotation.x = 0; 
            rb.AddForce(Vector3.down * gravMult);
            //Quaternion targetRotation = Quaternion.Euler(new Vector3(moveInput.x, moveInput.y, 0));
            //rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 2f * Time.fixedDeltaTime));
            //Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);
            //rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 1f * Time.fixedDeltaTime));
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
