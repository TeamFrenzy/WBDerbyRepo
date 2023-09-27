using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoxScript : MonoBehaviour
{
    public GameObject powerGlobeObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Debug.Log("TriggerBoxEntered");
            Destroy(powerGlobeObject);
        }
    }
}
