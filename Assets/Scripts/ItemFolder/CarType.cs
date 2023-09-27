using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In case of specific future parameters that would only apply to cars
public class CarType : Item
{
    [SerializeField] private GameObject carObject;

    public GameObject GetCarObject()
    {
        return carObject;
    }
    
}
