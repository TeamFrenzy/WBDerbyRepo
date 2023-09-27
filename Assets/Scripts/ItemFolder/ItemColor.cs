using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColor : Item
{
    [SerializeField] private Color itemColor;
    [SerializeField] private GameObject showCaseObject;
    
    public Color GetItemColor()
    {
        return itemColor;
    }
    
    public GameObject GetShowCaseObject()
    {
        return showCaseObject;
    }
}
