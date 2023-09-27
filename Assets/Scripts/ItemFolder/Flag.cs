using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : Item
{
    [SerializeField] private Image flagImage;

    public Image GetFlag()
    {
        return flagImage;
    }
}
