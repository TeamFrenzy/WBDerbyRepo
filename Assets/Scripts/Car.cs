using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour, IProduct
{
    //Parameters to set via Initialize
    public string playerName;
    public Image flag;
    public GameObject carType;
    public Color carColor;
    public GameObject ballType;
    public Color ballColor;
    public Transform ballParent;

    public FixedJoystick fixedJoystickTest;
    public ForceCarController controller;
    
    //Attributes
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private MeshRenderer bodyMeshRenderer;
    [SerializeField] private GameObject ball;
    [SerializeField] private Image flagDisplay;
    
    public string ProductName { get; set; }

    public void Initialize()
    {
        string colorTag = "_BaseColor";
        playerNameText.text = playerName;
        flagDisplay.sprite = flag.sprite;
        bodyMeshRenderer.material.SetColor(colorTag, carColor);
        //ball = Instantiate(ballType, ballParent);
        //ball.GetComponent<ConfigurableJoint>().connectedBody = GetComponent<Rigidbody>();
        ball.GetComponent<MeshRenderer>().material.SetColor(colorTag, ballColor);
        controller.fixedJoystick = fixedJoystickTest;
    }
}
