using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _fixedJoystick;

    [SerializeField] private float _maxMoveSpeed;
    
    [SerializeField] private float _acceleration;
    
    
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float directionTest;

    public float testVelocity = 0;
    
    
    // Start is called before the first frame update
    public Vector2 delta;

    private void FixedUpdate()
    {
        //_rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * _maxMoveSpeed, _rigidbody.velocity.y, _fixedJoystick.Vertical * _maxMoveSpeed);
        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            _rigidbody.AddForce(new Vector3(_fixedJoystick.Horizontal, _rigidbody.velocity.y, _fixedJoystick.Vertical).normalized * _acceleration);
        }

        if (_rigidbody.velocity.magnitude >= _maxMoveSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxMoveSpeed;
        }

        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            directionTest = Vector2.SignedAngle(_fixedJoystick.Direction, Vector2.right);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(directionTest, Vector2.up), _rotationSpeed);
        }

        testVelocity = _rigidbody.velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        //if(inputActions==null)
      //  {
            //playerMenuControls = new InputActionsB();
      //      inputActions = new InputActions();

     //       inputActions.Enable();
      //  }
    }

    public void PrintMovement(InputAction.CallbackContext context)
    {
        
        //Debug.Log(context.ToString());
    }
}
