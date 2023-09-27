using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.InputSystem;

public abstract class Item : MonoBehaviour
{
    private int itemID;

    public int GetID()
    {
        return itemID;
    }

}
