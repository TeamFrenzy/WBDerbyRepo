using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataContainerScript : SingletonPersistent<PlayerDataContainerScript>
{
    public string playerName;
    public int flag;
    public int carType;
    public int carColor;
    public int ballType;
    public int ballColor;
    public string currentHostingCode;

    public void SetData(string newPlayerName, int newFlag, int newCarType, int newCarColor, int newBallType, int newBallColor, string newCurrentHostingCode)
    {
        playerName = newPlayerName;
        flag = newFlag;
        carType = newCarType;
        carColor = newCarColor;
        ballType = newBallType;
        ballColor = newBallColor;
        currentHostingCode = newCurrentHostingCode;
    }
}
