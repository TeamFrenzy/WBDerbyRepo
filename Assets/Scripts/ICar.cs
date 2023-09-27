using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICar
{
    public string playerName { get; set; }
    public int carType { get; set; }
    public int carColor { get; set; }
    public int ballType { get; set; }

    public void Initialize();
}
