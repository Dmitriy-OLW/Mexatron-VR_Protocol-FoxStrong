using System;
using System.Collections;
using System.Collections.Generic;
using _2DOF;

using UnityEngine;

public class _2DOF_Move_Platform_Controller : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Rigidbody _rigidbody;
    
    private SendingData _Sending;
    private ObjectTelemetryData _sendingobjectTelemetryData;

    private void Awake()
    {
        _Sending = new SendingData();
        _sendingobjectTelemetryData = _Sending.ObjectTelemetryData;
    }
        private void OnEnable()
    {
        _Sending.SendingStart();
    }
    private void OnDisable()
    {
        _Sending.SendingStop();
    }

    private void Update()
    {
        var rot = _transform.eulerAngles;
        rot.x = rot.x > 180 ? rot.x - 360 : rot.x;
        rot.y = rot.y > 180 ? rot.y - 360 : rot.y;
        rot.z = rot.z > 180 ? rot.z - 360 : rot.z;

        //rot.z = -rot.z;

        //Debug.Log(rot);
        
        if(Math.Abs(rot.x) > 15f)
        {
            if(rot.x < -30f)
            {
                rot.x = -1 * (15 - (Math.Abs(rot.x) / 12));
            }
            else if(rot.x > 30f)
            {
                rot.x = 15 - (rot.x / 12);
            }
            else if(rot.x < -15f)
            {
                rot.x = -15;
            }
            else if(rot.x > 15f)
            {
                rot.x = 15;
            }

        }
        if(Math.Abs(rot.y) > 15f)
        {
            
            if(rot.y < -30f)
            {
                rot.y = -1 * (15 - (Math.Abs(rot.y) / 12));
            }
            else if(rot.y > 30f)
            {
                rot.y = 15 - (rot.y / 12);
            }
            else if(rot.y < -15f)
            {
                rot.y = -15;
            }
            else if(rot.y > 15f)
            {
                rot.y = 15;
            }

        }
        if(Math.Abs(rot.z) > 15f)
        {
            if(rot.z < -30f)
            {
                rot.z = -1 * (15 - (Math.Abs(rot.z) / 12));
            }
            else if(rot.z > 30f)
            {
                rot.z = 15 - (rot.z / 12);
            }
            else if(rot.z < -15f)
            {
                rot.z = -15;
            }
            else if(rot.z > 15f)
            {
                rot.z = 15;
            }

        }

        _sendingobjectTelemetryData.Angles = rot;
        _sendingobjectTelemetryData.Velocity = _rigidbody.velocity;
    }
}

