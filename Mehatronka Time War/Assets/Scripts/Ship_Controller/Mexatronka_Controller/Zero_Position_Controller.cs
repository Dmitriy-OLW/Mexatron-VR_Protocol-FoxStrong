using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zero_Position_Controller : MonoBehaviour
{
    public bool on_m_anytime = false;
    protected InputController input;
    
    protected virtual void OnEnable()
    {
        input.Enable();
    }


    protected virtual void OnDisable()
    {
        input.Disable();
    }

    protected void Awake()
    {
        input = new InputController();
    }
    
    void FixedUpdate()
    {
        if (on_m_anytime || input.Buttons.Share.ReadValue<float>()>0 )
        {
            Move_Position();
        }
    }

    void Move_Position()
    {
        if (input.Buttons.RightTurn.ReadValue<float>() > 0)
        {
            transform.Rotate(0f, 0.1f, 0f);
        }
        if (input.Buttons.LeftTurn.ReadValue<float>() > 0)
        {
            transform.Rotate(0f, -0.1f, 0f);
        }
        
        if (input.Buttons.Plus.ReadValue<float>() > 0)
        {
            transform.position += new Vector3(0, 0.01f, 0);
        }
        if (input.Buttons.Minus.ReadValue<float>() > 0)
        {
            transform.position += new Vector3(0, -0.01f, 0);
        }
        
        if (input.Buttons.RightBumper.ReadValue<float>() > 0)
        {
            transform.position += new Vector3(0, 0, 0.01f);
        }
        if (input.Buttons.LeftBumper.ReadValue<float>() > 0)
        {
            transform.position += new Vector3(0, 0, -0.01f);
        }
        if (input.Buttons.RightStick.ReadValue<float>() > 0)
        {
            transform.position += new Vector3(0.01f, 0, 0);
        }
        if (input.Buttons.LeftStick.ReadValue<float>() > 0)
        {
            transform.position += new Vector3(-0.01f, 0, 0);
        }
        
    }
}
