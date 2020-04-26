using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    Animator m_animControl;
    float m_walkSpeed = 2000.0f;
    float m_RotateSpeed = 100f;
    public float jumpForce = 1000f;
    
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        
        m_animControl = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    public void OnMove(InputValue value)
    {

    }
    
    void Move()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            OnUp();
        }
        if (Keyboard.current.sKey.isPressed)
        {
            OnDown();
        }
        if (Keyboard.current.aKey.isPressed)
        {
            OnLeft();
        }
        if (Keyboard.current.dKey.isPressed)
        {
            OnRight();
        }
        if(Keyboard.current.spaceKey.isPressed)
        {
            OnJump();
        }
        
        if(!Keyboard.current.wKey.isPressed &&
            !Keyboard.current.sKey.isPressed &&
            !Keyboard.current.aKey.isPressed &&
            !Keyboard.current.dKey.isPressed)
            {
                m_animControl.SetBool("Walk", false);

            }          
    
        if(!Keyboard.current.spaceKey.isPressed)
        {
           m_animControl.SetBool("Jump", false); 
        }
 
        //movement.Normalize(); //If moving diagonally makes sure it doesnt move faster
 
        //transform.Translate(movement * movementSpeed * Time.deltaTime);
    }
    
    public void OnUp()
    {
        //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
        m_Rigidbody.AddForce(transform.forward * m_walkSpeed * Time.deltaTime, ForceMode.Force);
        m_animControl.SetBool("Walk", true);
    }
    
    public void OnDown()
    {
        //Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
        m_Rigidbody.AddForce(-transform.forward * m_walkSpeed * Time.deltaTime, ForceMode.Force);
        m_animControl.SetBool("Walk", true);
    }
    
    public void OnLeft()
    {
        //Rotate the sprite about the Y axis in the negative direction
        transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * m_RotateSpeed, Space.World);
        m_animControl.SetBool("Walk", true);
    }
    
    public void OnRight()
    {
        //Rotate the sprite about the Y axis in the positive direction
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * m_RotateSpeed, Space.World);
        m_animControl.SetBool("Walk", true);
    }
    
    public void OnJump()
    {
        m_Rigidbody.AddForce(0, jumpForce * Time.deltaTime, 0, ForceMode.VelocityChange);
        m_animControl.SetBool("Jump", true);
    }
}
