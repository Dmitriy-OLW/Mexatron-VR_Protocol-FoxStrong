using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class CarFighter_Controller : MonoBehaviour
{
    protected InputController input;
    
    [SerializeField] private GameObject Engine_SFX;
    [SerializeField] private GameObject Gaze_Eyes;
    
    //Spedometr
    [SerializeField] private TextMeshProUGUI _Text_speed;
    [SerializeField] private Image _Speed_Barr;

    //Speed
    private float _speed_Forward_Back = 1000;
    private float _speed_Y = 2000;
    private float _speed_Righ_Left = 2000;
    private float _speed_Rotate = 1000;
    

    private float Spedometr_Speed;
    
    private float Value_Position_Z;
    private float Value_Position_Y;
    private float Value_Position_X;
    private float Value_Rotation_Y;
    
    private float Tormas = 0;


    private Rigidbody _rigidbody;

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
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
        Tormas = 0;
    }

    private void Start()
    {
        Gaze_Eyes.SetActive(false);
        Engine_SFX.SetActive(false);
    }


    private void FixedUpdate()
    {
       // Debug.Log(input.Steeringwheel.Steering_Steering.ReadValue<float>());
        //Debug.Log(input.Steeringwheel.Steering_Stick.ReadValue<float>());//это оно

        //Debug.Log(input.Transmission.Shifter7.ReadValue<float>());
        
        if (input.Buttons.North.ReadValue<float>() > 0)
        {
            Gaze_Eyes.SetActive(true);
        }
        else
        {
            Gaze_Eyes.SetActive(false);
        }
        
        
        if (input.Pedals.Brake.ReadValue<float>() > 0)
        {
            Value_Position_Z = 0;
            Value_Position_Y = 0;
            Value_Position_X = 0;
            Tormas += Time.deltaTime / 2;
            _rigidbody.drag = input.Pedals.Brake.ReadValue<float>() * 10 + Tormas;
        }
        else
        {
            _rigidbody.drag = 3f;
            Tormas = 0;
        }
        
        
        
        if (input.Transmission.Shifter1.ReadValue<float>() > 0)
        {
            if (input.Pedals.Clutch.ReadValue<float>() > 0)
            {
                _rigidbody.AddRelativeForce(0, 0, input.Pedals.Clutch.ReadValue<float>()*_speed_Forward_Back * -1);
            }

            if (input.Pedals.Throttle.ReadValue<float>()>0)
            {
                _rigidbody.AddRelativeForce(0, 0, input.Pedals.Throttle.ReadValue<float>()*_speed_Forward_Back);
            }
                
        }
        else if (input.Transmission.Shifter2.ReadValue<float>() > 0)
        {
            if (input.Pedals.Clutch.ReadValue<float>() > 0)
            {
                _rigidbody.AddRelativeForce(0, 0, input.Pedals.Clutch.ReadValue<float>()*2*_speed_Forward_Back * -1);
            }

            if (input.Pedals.Throttle.ReadValue<float>()>0)
            {
                _rigidbody.AddRelativeForce(0, 0, input.Pedals.Throttle.ReadValue<float>()*2*_speed_Forward_Back);
            }
                
        }
        else if (input.Transmission.Shifter3.ReadValue<float>() > 0)
        {
            if (input.Pedals.Clutch.ReadValue<float>() > 0)
            {
                _rigidbody.AddRelativeForce(0, 0, input.Pedals.Clutch.ReadValue<float>()*3* _speed_Forward_Back * -1);
            }

            if (input.Pedals.Throttle.ReadValue<float>()>0)
            {
                _rigidbody.AddRelativeForce(0, 0, input.Pedals.Throttle.ReadValue<float>()*3*_speed_Forward_Back);
            }
            
        }
        else if (input.Transmission.Shifter4.ReadValue<float>() > 0)
        {
            if (input.Pedals.Throttle.ReadValue<float>() > 0 && input.Pedals.Clutch.ReadValue<float>() > 0)
            {
                _rigidbody.AddRelativeForce(0, _speed_Y*2, 7*_speed_Forward_Back );
            }

            else if (input.Pedals.Throttle.ReadValue<float>()>0)
            {
                _rigidbody.AddRelativeForce(0, 0, input.Pedals.Throttle.ReadValue<float>()*5*_speed_Forward_Back);
            }
            else if (input.Pedals.Clutch.ReadValue<float>() > 0)
            {
                _rigidbody.AddRelativeForce(0, 0, input.Pedals.Clutch.ReadValue<float>()*3*_speed_Forward_Back * -1);
            }
            
            
        }
        else if (input.Transmission.Shifter5.ReadValue<float>() > 0)
        {
            if (input.Pedals.Clutch.ReadValue<float>() > 0)
            {
                _rigidbody.AddRelativeForce(0, input.Pedals.Clutch.ReadValue<float>()*_speed_Y*-1, 0);
            }

            if (input.Pedals.Throttle.ReadValue<float>()>0)
            {
                _rigidbody.AddRelativeForce(0, input.Pedals.Throttle.ReadValue<float>()*_speed_Y, 0);
            }
        }
        else if (input.Transmission.Shifter6.ReadValue<float>() > 0)
        {
            if (input.Pedals.Clutch.ReadValue<float>() > 0)
            {
                _rigidbody.AddRelativeForce(input.Pedals.Clutch.ReadValue<float>()*_speed_Righ_Left * -1, 0, 0);
            }

            if (input.Pedals.Throttle.ReadValue<float>()>0)
            {
                _rigidbody.AddRelativeForce(input.Pedals.Throttle.ReadValue<float>()*_speed_Righ_Left, 0, 0);
            }
        }
        else if (input.Transmission.Shifter7.ReadValue<float>() > 0)
        {
            if (input.Pedals.Clutch.ReadValue<float>() > 0)
            {
                _rigidbody.AddRelativeForce(0, input.Pedals.Clutch.ReadValue<float>()*_speed_Y*2, input.Pedals.Clutch.ReadValue<float>()*5*_speed_Forward_Back * -1);
            }

            if (input.Pedals.Throttle.ReadValue<float>()>0)
            {
                _rigidbody.AddRelativeForce(0, input.Pedals.Throttle.ReadValue<float>()*_speed_Y*3, input.Pedals.Throttle.ReadValue<float>()*9*_speed_Forward_Back);
            }
        }
        
        if (input.Handbrake.Handbrake.ReadValue<float>() > 0)
        {
            
            Value_Position_Y =  3 * _speed_Y * (input.Handbrake.Handbrake.ReadValue<float>());
            _rigidbody.AddRelativeForce(0, Value_Position_Y, 0);
        }

        
        Value_Rotation_Y = _speed_Rotate * input.Steeringwheel.Steering_Stick.ReadValue<float>();
        _rigidbody.AddRelativeTorque(0, Value_Rotation_Y, 0);
        //TrustUpdate();
        SpedometrUpdate();
    }


    private void SpedometrUpdate()
    {
        Spedometr_Speed = _rigidbody.velocity.magnitude;
        _Text_speed.text = Convert.ToString((int)Spedometr_Speed);
        _Speed_Barr.fillAmount = Spedometr_Speed/300;
        if(Spedometr_Speed > 100)
        {
            Engine_SFX.SetActive(true);
            Engine_SFX.GetComponent<AudioSource>().volume = (Spedometr_Speed - 100) / 100;
        }
        else
        {
            Engine_SFX.GetComponent<AudioSource>().volume = 0;
            Engine_SFX.SetActive(false);
        }
    }
    
}









/*if (input.Pedals.Clutch.ReadValue<float>() > 0)
        {
            if (input.Transmission.Shifter1.ReadValue<float>() > 0)
            {
                Rezim_Rabot = 0;
                Revers = false;
            }
            if (input.Transmission.Shifter2.ReadValue<float>() > 0)
            {
                Rezim_Rabot = 2;
                Revers = false;
            }
            if (input.Transmission.Shifter3.ReadValue<float>() > 0)
            {
                Rezim_Rabot = 4;
                Revers = false;
            }
            if (input.Transmission.Shifter4.ReadValue<float>() > 0)
            {
                Rezim_Rabot = 6;
                Revers = false;
            }
            if (input.Transmission.Shifter7.ReadValue<float>() > 0)
            {
                Rezim_Rabot = 0;
                Revers = true;
            }
        }*/










