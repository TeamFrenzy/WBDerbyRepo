using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBox : MonoBehaviour
{
    [SerializeField] private GameObject globeObject;
    [SerializeField] private ConstantForce constantForce;

    private void RemoveGlobe()
    {
        globeObject.SetActive(false);
    }

    public void SetFallingSpeed(float fallSpeed)
    {
        constantForce.force = new Vector3(0, fallSpeed, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        RemoveGlobe();
        constantForce.force = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().useGravity = true;
    }
}
