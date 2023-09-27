using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ForceCarController : MonoBehaviour {

    // Settings
    public float MoveSpeed = 1;
    public float rotationSpeed;

    [FormerlySerializedAs("_fixedJoystick")] [SerializeField] public FixedJoystick fixedJoystick;
    [SerializeField] private GameObject ball;
    [SerializeField] private ConfigurableJoint ballJoint;
    
    // Variables
    private Vector3 MoveForce;
    private Rigidbody rb;
    private ConstantForce _constantForce;

    private bool powerButton;
    public float minSteerThreshold;
    public float superModeTimer;
    public float superModeDuration;
    public bool isInSuperMode = false;

    public bool movable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _constantForce = GetComponent<ConstantForce>();
        if (movable)
        {
            fixedJoystick = FindObjectOfType<FixedJoystick>();
        }
    }

    private void Update()
    {
        if (superModeTimer > 0f)
        {
            superModeTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate() 
    {
        /*
        if (powerButton)
        {
            _constantForce.force = new Vector3(_fixedJoystick.Horizontal, 0f, _fixedJoystick.Vertical).normalized*MoveSpeed;
        }
        else
        {
            _constantForce.force = Vector3.zero;
        }
        */
        
        if (fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0 ) 
        {
            _constantForce.force = new Vector3(fixedJoystick.Horizontal, 0f, fixedJoystick.Vertical).normalized*MoveSpeed;
        }
        else
        {
            _constantForce.force = Vector3.zero;
        }

        //Rotation
        if (rb.velocity.magnitude > minSteerThreshold)
        {
            Vector3 fixedDirection = new Vector3(fixedJoystick.Direction.x, 0, fixedJoystick.Direction.y);
            Quaternion deltaQuat = Quaternion.FromToRotation(rb.transform.forward, fixedDirection);

            Vector3 axis;
            float angle;
            deltaQuat.ToAngleAxis(out angle, out axis);

            float dampenFactor = 0.8f;
            rb.AddTorque(-rb.angularVelocity * dampenFactor*rotationSpeed, ForceMode.Acceleration);

            float adjustFactor = 0.5f;
            rb.AddTorque(axis.normalized * angle * adjustFactor*rotationSpeed, ForceMode.Acceleration);
        }

       if (isInSuperMode && superModeTimer <= 0f)
       {
           ExitSuperMode();
       }
    }

    public void PushButton()
    {
        if (powerButton)
        {
            powerButton = false;
        }
        else if (!powerButton)
        {
            powerButton = true;
        }
    }

    private void EnterSuperMode()
    {
        ballJoint.connectedAnchor = new Vector3(0, -0, 0);
        ballJoint.angularYMotion = ConfigurableJointMotion.Free;
        //SoftJointLimit temp = new SoftJointLimit();
        //temp.limit = 0f;
        //ballJoint.angularYLimit = temp;
        ball.GetComponent<ConstantForce>().relativeForce = new Vector3(0.05f, 0, 0);
        isInSuperMode = true;
    }
    
    private void ExitSuperMode()
    {
        ballJoint.connectedAnchor = new Vector3(0, -0, -0.5f);
        ballJoint.angularYMotion = ConfigurableJointMotion.Limited;
        //SoftJointLimit temp = new SoftJointLimit();
        //temp.limit = 60f;
        //ballJoint.angularYLimit = temp;
        ball.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, 0);
        isInSuperMode = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerUpBox")
        {
            Debug.Log("PowerBoxFound");
            //other.gameObject.SetActive(false);
            superModeTimer = superModeDuration;
            EnterSuperMode();
        }
    }
}
