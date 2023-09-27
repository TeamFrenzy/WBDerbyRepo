using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In case of specific future parameters that would only apply to balls
public class BallType : Item
{
    [SerializeField] private GameObject ballObject;

    public GameObject GetBallObject()
    {
        return ballObject;
    }
    
}
