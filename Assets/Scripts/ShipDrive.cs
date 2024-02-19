using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class ShipDrive : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public float gravMult;
    public float strafeVal;
    private Rigidbody rb;

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
    }
    void Accelerate()
    {
        //moveInput = ff1.Player.Move.ReadValue<Vector2>();
        moveInput = inputActions["Move"].ReadValue<Vector2>();
        print("Move Input: " + moveInput.x + " " + moveInput.y);
        if (/*UnityEngine.Input.GetKey(KeyCode.W)*/ moveInput.y > 0)
        {
            rb.AddRelativeForce(new Vector3(0, 0, 100));
            print("Velocity: " + rb.velocity);
            //rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed);
            //Vector3 forceToAdd = transform.forward;
            //forceToAdd.y = 0;
            //rb.AddForce(forceToAdd * speed * 10);
        }
        else if (/*UnityEngine.Input.GetKey(KeyCode.S)*/moveInput.y < 0)
        {
            //rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * -speed);
            rb.AddRelativeForce(new Vector3(0, 0, -100));
            //Vector3 forceToAdd = -transform.forward;
            //forceToAdd.y = 0;
            //rb.AddForce(forceToAdd * speed * 10);
        }
        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        localVel.x = 0;
        rb.velocity = transform.TransformDirection(localVel);
        //if(UnityEngine.Input.GetButtonDown("Fire1"))
        //{
        //    rb.AddRelativeForce(new Vector3(100, 0, -100));
        //}
        //if(UnityEngine.Input.GetKey(KeyCode.R))
        //{
        //    rb.AddRelativeForce(new Vector3(1000, 0, 0));
        //}
        //if (UnityEngine.Input.GetKey(KeyCode.Q))
        //{
        //    rb.AddRelativeForce(new Vector3(-1000, 0, 0));
        //}
        //rb.AddRelativeForce(new Vector3(0, -100, 0));
        
    }
    void Turn()
    {

        if (/*UnityEngine.Input.GetKey(KeyCode.D)*/ moveInput.x > 0)
        {
            rb.AddTorque(Vector3.up * turnSpeed);
            //yaw += turnSpeed * Time.deltaTime * (((UnityEngine.Input.GetAxis("Horizontal") < 0.25) && (UnityEngine.Input.GetAxis("Horizontal") > -0.25)) ? 0f : UnityEngine.Input.GetAxis("Horizontal"));//Turn if the joystick isn't in the deadzone
            //transform.rotation = Quaternion.Euler(0, yaw, 0);
            //speed += UnityEngine.Input.GetAxis("Horizontal");
            //transform.rotation = Quaternion.Euler(0, turnSpeed * speed, 0);
        }
        if (/*UnityEngine.Input.GetKey(KeyCode.A)*/ moveInput.x < 0)
        {
            rb.AddTorque(-Vector3.up * turnSpeed);
            //speed += UnityEngine.Input.GetAxis("Horizontal");
            //transform.rotation = Quaternion.Euler(0, turnSpeed * speed, 0);

        }
    }

    void Fall()
    {
        rb.AddForce(Vector3.down * gravMult);
    }
}
