using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private ConfigurableJoint configurableJoint;

    public GameObject car;
    public Rigidbody carRb;
    private SoftJointLimit _softJointLimit;

    public float minAnchorDistance;

    public float maxAnchorDistance;

    public float hingeVelocityTest;

    public float linealLimitUpSpeed;

    public float linealLimitDownSpeed;

    public float hitForce;
    public float hitUpForce;
    public float hitRadius;

    public float recoilForce;
    private SoftJointLimit temp;

    private Rigidbody rb;
       
    Vector3 lastPosition = Vector3.zero;
    public float currentRBSpeed;
    public float currentNonRBSpeed;
    
    public bool inHigh;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        configurableJoint = GetComponent<ConfigurableJoint>();
        temp = new SoftJointLimit();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void FixedUpdate()
    {
        
        
        currentRBSpeed = rb.velocity.magnitude;
        currentNonRBSpeed = Vector3.Distance(transform.position, lastPosition) / Time.fixedDeltaTime;
        lastPosition = transform.position;
        
        //temp.limit = rb.velocity.magnitude/20f;
        //configurableJoint.linearLimit = temp;
        
        //TODO: Find a way to artificially return the ball to its start place behind the car
        
        /*
        Vector3 angularMomentum = new Vector3(
            carRb.inertiaTensor.x * carRb.angularVelocity.x,
            carRb.inertiaTensor.y * carRb.angularVelocity.y,
            carRb.inertiaTensor.z * carRb.angularVelocity.z
        );
        hingeVelocityTest = angularMomentum.magnitude;

        
        if (angularMomentum.magnitude > 0.2f)
        {
            //Debug.Log("InUpSpeed");
            SoftJointLimit temp = new SoftJointLimit();
            temp.limit = configurableJoint.linearLimit.limit + linealLimitUpSpeed;
            configurableJoint.linearLimit = temp;
        }
        else if(angularMomentum.magnitude < 0.2f && configurableJoint.linearLimit.limit >= 0)
        {
            //Debug.Log("InDownSpeed");
            SoftJointLimit temp = new SoftJointLimit();
            //_softJointLimit.limit = configurableJoint.linearLimit.limit - linealLimitDownSpeed * Time.fixedTime;
            temp.limit = configurableJoint.linearLimit.limit - linealLimitDownSpeed;
            configurableJoint.linearLimit = temp;
        }
        */
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Car" || other.transform.tag == "Fence" || other.transform.tag == "PowerUpBox")
        {
            Debug.DrawLine(other.transform.position, other.transform.position-other.relativeVelocity, Color.red, 10f);
            Debug.DrawLine(transform.position, other.GetContact(0).point, Color.blue, 10f);
            //other.rigidbody.AddExplosionForce(other.relativeVelocity.magnitude*hitForce, other.GetContact(0).point, other.relativeVelocity.magnitude*hitRadius);
            if (car.GetComponent<ForceCarController>().isInSuperMode)
            {
                other.rigidbody.AddExplosionForce(other.relativeVelocity.magnitude*hitForce*1.5f, other.GetContact(0).point, other.relativeVelocity.magnitude*hitRadius, other.relativeVelocity.magnitude*hitUpForce);
                other.rigidbody.AddForce(-other.relativeVelocity*hitForce*3);
            }
            else
            {
                other.rigidbody.AddExplosionForce(other.relativeVelocity.magnitude*hitForce, other.GetContact(0).point, other.relativeVelocity.magnitude*hitRadius, other.relativeVelocity.magnitude*hitUpForce);
                other.rigidbody.AddForce(-other.relativeVelocity*hitForce);
            }
        }

        if (!car.GetComponent<ForceCarController>().isInSuperMode)
        {
            GetComponent<Rigidbody>().AddForce(other.relativeVelocity * recoilForce);
        }
    }
}
