using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Debug.Log("Car Has Fallen Into Water");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Car")
        {
            Debug.Log("Car Has Fallen Into Water");
            other.gameObject.GetComponent<ForceCarController>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
