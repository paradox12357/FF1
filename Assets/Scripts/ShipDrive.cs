using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using JetBrains.Annotations;
//ADDING A DRIFT MECHANIC WOULD MAKE TURNS MUCH MUCH EASIER TO PREPARE FOR AND INCREASE GAME DEPTH
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
    public Transform shipObj;
    //public double targetHigh = 2.06;
    //public double targetLow = 1.06;
    //bool goingUp;
    public float timer = 0f;
    //bool centerred = false;
    //private float bufferCheckDistance = 0.1f;
    //public Vector3 forwardDirection = Vector3.forward;
    public CheckpointCounter checkpointCounter;
    public string currentItem = "None";
    public Image itemImage;

    private FF1 ff1;
    private InputActionAsset inputActions;

    private Vector2 moveInput;
    public static int Players = 0;
    public int Player = 0;
    public Cinemachine.CinemachineVirtualCamera cam;
    [SerializeField] public Camera cam2;
    [SerializeField] public Camera minimapCam;

    //Sound stuff
    bool accelPressed;
    bool decelPressed;
    bool strafeRightPressed;
    bool strafeLeftPressed;
    public GameObject geminiShip;
    public GameObject virgoShip;
    public GameObject scorpioShip;
    public int shipSelect;

    //Ship Engine Effect Stuff
    public GameObject lightFive;
    public GameObject lightFour;
    public GameObject lightThree;
    public GameObject lightTwo;
    public GameObject lightOne;
    public GameObject lightScorpioOne;
    public GameObject lightScorpioTwo;
    public GameObject lightVirgo;
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject sphere3;
    public GameObject sphere4;

    //Item Stuff
    public GameObject rocket;
    //GameObject rocketClone;
    public GameObject rocketHoming;
    //GameObject rocketHomingClone;
    public int HP;
    //[SerializeField] private Transform rocketPf;
    public Transform itemSpawnPoint;
    bool itemUsed;
    //Transform spawnPoint;
    //int changingRot = 0;
    // Start is called before the first frame update
    void Start()
    {
        //spawnPoint = rb.transform;
        HP = 100;
        Cursor.visible = false;
        //geminiShip = GameObject.Find("/Speedship/Gemini");
        //virgoShip = GameObject.Find("/Speedship/Virgo");
        Players++;
        Player = Players;
        ff1 = new FF1();
        inputActions = GetComponent<PlayerInput>().actions;
        rb = GetComponent<Rigidbody>();
        ff1.Player.Enable();
        inputActions.Enable();
        itemUsed = false;
        // temp ship select
        shipSelect = Player - 1;
        if (shipSelect > 2)
        {
            shipSelect = 0;
        }
        switch (Player)
        {
            case 1:
                minimapCam.rect = new Rect(0.7f, 0, 0.3f, 0.3f);
                sphere1.SetActive(true);
                break;
            case 2:
                minimapCam.rect = new Rect(0.4f, 0, 0.2f, 0.2f);
                sphere2.SetActive(true);
                break;
            case 3:
                minimapCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                sphere3.SetActive(true);
                break;
            default:
                minimapCam.rect = new Rect(0.35f, 0.35f, 0.3f, 0.3f);
                sphere4.SetActive(true);
                break;
        }

        //Ship Select
        if(shipSelect == 0)//Gemini
        {
            shipObj = transform.Find("Gemini");
            if (shipObj == null)
            {
                print("BRUH!!!");
            }
            geminiShip.SetActive(true);
            gravMult = 100;
            turnSpeed = 25f;
            /*lightFive = GameObject.Find("geminiLightFive");
            lightFour = GameObject.Find("geminiLightFour");
            lightThree = GameObject.Find("geminiLightThree");
            lightTwo = GameObject.Find("geminiLightTwo");
            lightOne = GameObject.Find("geminiLightOne");*/
        }
        if (shipSelect == 1)//Virgo
        {
            shipObj = transform.Find("Virgo");
            if (shipObj == null)
            {
                print("BRUH!!!");
            }
            virgoShip.SetActive(true);
            gravMult = 110;
            turnSpeed = 22.5f;
        }
        if (shipSelect == 2)//Scorpio
        {
            shipObj = transform.Find("Scorpio");
            if (shipObj == null)
            {
                print("BRUH!!!");
            }
            scorpioShip.SetActive(true);
            gravMult = 90;
            turnSpeed = 27.5f;
        }

        // set cam to the layer for the player
        cam.gameObject.layer = Player + 5;
        cam2.cullingMask |= 1 << (Player + 5);


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
        //print("Torque: " + rb.GetAccumulatedTorque());
        if (checkpointCounter.hasFinished == false)
        {
            Accelerate();
            Turn();
            useItem();
        }
        Fall();
        Oscillate();
        if(HP <= 0)
        {
            FindObjectOfType<SoundEffectPlayer>().Play("deathSound");
            FindObjectOfType<SoundEffectPlayer>().Play("deathSoundCar");
            print("R I P");
            //Destroy(gameObject);
            transform.position = new Vector3(0, 0, 0);
            HP = 100;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Boost")
        {
            var Accelerate = inputActions["Accelerate"].ReadValue<float>();
            if (/*(UnityEngine.Input.GetKey(KeyCode.W) || */ /*ff1.Player.Accelerate.IsPressed()*/ Mathf.Approximately(Accelerate, 1f) && grounded == true)
            {
                rb.AddRelativeForce(new Vector3(0, 0, 100));
                //print("Velocity: " + rb.velocity.magnitude);
            }
        }
        if (other.gameObject.tag == "Offroad")
        {
            var Accelerate = inputActions["Accelerate"].ReadValue<float>();
            if (/*(UnityEngine.Input.GetKey(KeyCode.W) || */ /*ff1.Player.Accelerate.IsPressed()*/ Mathf.Approximately(Accelerate, 1f) && grounded == true)
            {
                rb.AddRelativeForce(new Vector3(0, 0, -50));
                //print("Velocity: " + rb.velocity.magnitude);
            }
        }
    }
    void Accelerate()
    {
        //moveInput = ff1.Player.Move.ReadValue<Vector2>();
        moveInput = inputActions["Move"].ReadValue<Vector2>();
        var Accelerate = inputActions["Accelerate"].ReadValue<float>();
        var Decelerate = inputActions["Decelerate"].ReadValue<float>();
        var StrafeRight = inputActions["StrafeRight"].ReadValue<float>();
        var StrafeLeft = inputActions["StrafeLeft"].ReadValue<float>();

        //Engine Fire
        if (Mathf.Approximately(Accelerate, 1f) && shipSelect == 0 && accelPressed == false)
        {
            
            lightOne.SetActive(true);
            
            lightTwo.SetActive(true);
            
            lightThree.SetActive(true);
            
            lightFour.SetActive(true);
            
            lightFive.SetActive(true);
            
        }

        if (Mathf.Approximately(Accelerate, 1f) && shipSelect == 2 && accelPressed == false)
        {

            lightScorpioOne.SetActive(true);
            lightScorpioTwo.SetActive(true);    

        }

        if (Mathf.Approximately(Accelerate, 1f) && shipSelect == 1 && accelPressed == false)
        {

            lightVirgo.SetActive(true);

        }
        //Sound Effects for Accelerating/Decelerating/Strafing
        if (Mathf.Approximately(Accelerate, 1f) && accelPressed == false)
        {
            FindObjectOfType<SoundEffectPlayer>().Play("engineStart");
            accelPressed = true;
        }
        if (Mathf.Approximately(Accelerate, 0f))
        {
            accelPressed = false;
            lightOne.SetActive(false);
            lightTwo.SetActive(false);
            lightThree.SetActive(false);
            lightFour.SetActive(false);
            lightFive.SetActive(false);
            lightScorpioOne.SetActive(false);
            lightScorpioTwo.SetActive(false);
            lightVirgo.SetActive(false);
        }

        if (Mathf.Approximately(Decelerate, 1f) && decelPressed == false)
        {
            FindObjectOfType<SoundEffectPlayer>().Play("brake");
            decelPressed = true;
        }
        if (Mathf.Approximately(Decelerate, 0f))
        {
            decelPressed = false;
        }

        if (Mathf.Approximately(StrafeRight, 1f) && strafeRightPressed == false)
        {
            FindObjectOfType<SoundEffectPlayer>().Play("strafeRight");
            strafeRightPressed = true;
        }
        if (Mathf.Approximately(StrafeRight, 0f))
        {
            strafeRightPressed = false;
        }

        if (Mathf.Approximately(StrafeLeft, 1f) && strafeLeftPressed == false)
        {
            FindObjectOfType<SoundEffectPlayer>().Play("strafeLeft");
            strafeLeftPressed = true;
        }
        if (Mathf.Approximately(StrafeLeft, 0f))
        {
            strafeLeftPressed = false;
        }
        //print("Move Input: " + moveInput.x + " " + moveInput.y);
        if (/*(UnityEngine.Input.GetKey(KeyCode.W) || */ /*ff1.Player.Accelerate.IsPressed()*/ Mathf.Approximately(Accelerate, 1f) && grounded == true)
        {
            rb.AddRelativeForce(new Vector3(0, 0, 100));
            //forward = true;
            //rb.drag += 1; 
            //print("Velocity: " + rb.velocity.magnitude);
        }
        else if (/*(UnityEngine.Input.GetKey(KeyCode.S) ||*/ Mathf.Approximately(Decelerate, 1f) && (grounded == true))
        {
            rb.AddRelativeForce(new Vector3(0, 0, -80));
            //forward = false;
            //print("Velocity: -" + rb.velocity.magnitude);
        }

        /*if (rb.velocity.magnitude > 0 && forward == true)//Gradually Slow the Speedship down (when it's going forwards)
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
            if (shipObj.localPosition.y > 1.25f)
            {
                shipObj.localPosition = new Vector3(shipObj.localPosition.x, shipObj.localPosition.y - 0.01f, shipObj.localPosition.z);
            }
            if (shipObj.localPosition.y < 1.25f)
            {
                shipObj.localPosition = new Vector3(shipObj.localPosition.x, shipObj.localPosition.y + 0.01f, shipObj.localPosition.z);
            }

        }*/
        if (rb.velocity.magnitude > 0 && forward == false)//Gradually Slow the Speedship down (when it's going backwards)
        {
            if (rb.velocity.magnitude > 50)
            {
                //rb.AddRelativeForce(new Vector3(0, 0, 0.8f * rb.velocity.magnitude));
                rb.drag = 0.8f;
            }
            else
            {
                //rb.AddRelativeForce(new Vector3(0, 0, 0.5f * rb.velocity.magnitude));
                rb.drag = 0.1f;
            }
            if (shipObj.localPosition.y > 1.25f)
            {
                shipObj.localPosition = new Vector3(shipObj.localPosition.x, shipObj.localPosition.y - 0.01f, shipObj.localPosition.z);
            }
            if (shipObj.localPosition.y < 1.25f)
            {
                shipObj.localPosition = new Vector3(shipObj.localPosition.x, shipObj.localPosition.y + 0.01f, shipObj.localPosition.z);
            }
        }

        Vector3 localVel = transform.InverseTransformDirection(rb.velocity);
        localVel.x = 0;
        rb.velocity = transform.TransformDirection(localVel);
        

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
            //shipObj.localRotation = Quaternion.Euler(new Vector3(shipObj.localRotation.x + 0.01f, shipObj.localRotation.y, shipObj.localRotation.z));
            //shipObj.transform.Rotate(new Vector3(1f, 0, 0));
            //print("LMAOAOAAOAOAOAOAOAOAO");
        }
        if (moveInput.x < 0 && grounded == true)
        {
            rb.AddTorque(-Vector3.up * turnSpeed);
        }
        if (moveInput.x > 0 && grounded == false)
        {
            rb.AddTorque(Vector3.up * turnSpeed * 0.5f);
            //print("LMAOAOAAOAOAOAOAOAOAO");
        }
        if (moveInput.x < 0 && grounded == false)
        {
            rb.AddTorque(-Vector3.up * turnSpeed * 0.5f);

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

    void Oscillate()
    {
        if (grounded && rb.velocity.magnitude < 0.01f)
        {
            //print("Ship normal y val: " + shipObj.localPosition.y);//1.56
            /*if(goingUp == false)
            {
                shipObj.localPosition = new Vector3(shipObj.localPosition.x, shipObj.localPosition.y - 0.01f, shipObj.localPosition.z);
                if((double)shipObj.localPosition.y == targetLow)
                {
                    goingUp = true;
                    print("LOLOLOL");
                }
            }
            else
            {
                shipObj.localPosition = new Vector3(shipObj.localPosition.x, shipObj.localPosition.y + 0.01f, shipObj.localPosition.z);
                if (shipObj.localPosition.y == targetHigh)
                {
                    goingUp = false;
                }
            }*/
            timer += Time.deltaTime;
            //float oscillatingValue = Mathf.Lerp(1.06f, 3.0f, Mathf.Sin(timer * 0.7f));
            shipObj.localPosition = new Vector3(shipObj.localPosition.x, Mathf.Sin(timer) * 0.4f + 1.25f, shipObj.localPosition.z);
        }
        else if (grounded && rb.velocity.magnitude > 0.01f)
        {
            //shipObj.localPosition = new Vector3(shipObj.localPosition.x, 1.1f, shipObj.localPosition.z);
            timer = 0.0f;
        }
    }
    void Fall()
    {

        if (grounded == false)
        {
            //rb.rotation.x = 0; 
            rb.AddForce(Vector3.down * gravMult);
            /*if(transform.forward.y > 0 && centerred == false)
            {
                transform.forward = new Vector3(transform.forward.x, transform.forward.y - 0.05f, transform.forward.z);
                //if(transform.forward.y == 0)
                //{
                //    centerred = true;
                //}
            }
            if (transform.forward.y < 0 && centerred == false)
            {
                transform.forward = new Vector3(transform.forward.x, transform.forward.y + 0.05f, transform.forward.z);
                //if (transform.forward.y == 0)
                //{
                //    centerred = true;
                //}
            }*/
            transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            //if(centerred == true)
            //{
            //    transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            //}

            //Quaternion targetRotation = Quaternion.Euler(new Vector3(moveInput.x, moveInput.y, 0));
            //rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 2f * Time.fixedDeltaTime));
            //Quaternion targetRotation = Quaternion.LookRotation(forwardDirection);
            //rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 1f * Time.fixedDeltaTime));
        }
        else if (grounded == true)
        {
            //centerred = false;
            rb.AddForce(Vector3.down * 15);
        }
    }

    void useItem()
    {   
        var ItemUse = inputActions["ItemUse"].ReadValue<float>();
        if (Mathf.Approximately(ItemUse, 1f) == true && itemUsed == false)
        {
            itemUsed = true;
            if (currentItem.Equals("rocket"))
            {
                FindObjectOfType<SoundEffectPlayer>().Play("rocketShoot");
                var itemShot = Instantiate(rocket, itemSpawnPoint.position, itemSpawnPoint.rotation);
                rocketWallHit rwh = itemShot.GetComponent<rocketWallHit>();
                rwh.setShotOwner(gameObject);
                itemShot.GetComponent<Rigidbody>().velocity = (rb.transform.forward * 15) + 2 * rb.velocity;
            }
            if (currentItem.Equals("rocketHoming"))
            {
                FindObjectOfType<SoundEffectPlayer>().Play("rocketShootHoming");
                var itemShot = Instantiate(rocketHoming, itemSpawnPoint.position, itemSpawnPoint.rotation);
                rocketWallHit rwh = itemShot.GetComponent<rocketWallHit>();
                rwh.setShotOwner(gameObject);
                itemShot.GetComponent<Rigidbody>().velocity = (rb.transform.forward * 15) + 3 * rb.velocity;
            }
            if (currentItem.Equals("boost"))
            {
                FindObjectOfType<SoundEffectPlayer>().Play("boostActivated");
                rb.AddRelativeForce(new Vector3(0, 0, 10000));
            }
            itemImage.sprite = null;
            currentItem = "None";
            
//itemSpawnPoint.forward * 10 
            print("UseItem");
        }
        if (Mathf.Approximately(ItemUse, 0f) == true && itemUsed == true)
        {
            itemUsed = false;
            print("UseItem");
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        //grounded = true;
        //print("HIT WALL LOLOLOLOL!!");
        if (col.gameObject.name.Contains("Wall"))
        {
            //print("WALLHIT!!!");
            //rb.AddRelativeForce(new Vector3(0, 10, -100));
            //transform.forward = new Vector3(-transform.forward.x, transform.forward.y, transform.forward.z);
            Vector3 reflectionDirection = Vector3.Reflect(rb.velocity, col.contacts[0].normal);
            //print("Old Vel = " + rb.velocity);
            //print("New Vel = " + reflectionDirection);
            //print("Normal of Col = " + col.contacts[0].normal);
            //Vector3 reflectionDirection = Vector3.Reflect(rb.velocity, col.GetContact(0).normal);
            //Vector3 reflectionDirection = Vector3.Reflect(rb.velocity, );

            rb.velocity = reflectionDirection;
            //Sound Effects for hitting walls
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                FindObjectOfType<SoundEffectPlayer>().Play("wallhitOne");
            }
            if (rand == 1)
            {
                FindObjectOfType<SoundEffectPlayer>().Play("wallhitTwo");
            }
            if (rand == 2)
            {
                FindObjectOfType<SoundEffectPlayer>().Play("wallhitThree");
            }
            //Destroy(col.gameObject);

            //rb.AddForce(Vector3.Reflect(rb.velocity.normalized, col.contacts[0].normal));

        }
        /*if (col.gameObject.tag == "rocket")
        {
            print("HIT BY ROCKET!!!");
            HP = HP - 20;
            if(HP <= 0)
            {
                Destroy(gameObject);
            }
        }*/
        //if (col.gameObject.name.Contains("Wall"))
        //{
        //    print("HIT WALL LOLOLOLOL!!");
        //}
    }

    /*private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }*/
}
