//using System;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Flyter : MonoBehaviour
{
    public int[] Coliders_srabotaly;
    public bool prepydstvie;

    public bool inSpace = false;
    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform target;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationalDeep = 0.5f;
    [SerializeField] private float Distanc_Stop = 50f;
    
    //Speed
    [SerializeField] private float _speed_Forward_Back = 100;
    [SerializeField] private float _speed_Up_Down = 100;
    [SerializeField] private float _speed_Righ_Left = 100;
    
    private float Value_Position_X;
    private float Value_Position_Y;
    private float Value_Position_Z;

    private float Distanse_do_player;
    private bool Prepytstvie_not_Move;

    private void Start()
    {
        prepydstvie = false;
        Prepytstvie_not_Move = false;
        for (int i = 0; i < 12; i++)
        {
            Coliders_srabotaly[i] = 0;
        }

        _speed_Forward_Back = _speed_Forward_Back * 2;
        movementSpeed = movementSpeed * 2;
    }

    private void FixedUpdate()
    {
        Distanse_do_player = Vector3.Distance(target.position, transform.position);
        if (prepydstvie == true)
        {
            ObHod();
        }
        else if(Prepytstvie_not_Move==true)
        {
            Prepytstvie_not_Move = false;
            for (int i = 0; i < 12; i++)
            {
                Coliders_srabotaly[i] = 0;
            }
        }
        if (Prepytstvie_not_Move==false && Distanse_do_player > Distanc_Stop)
        {
            Move();
        }
        Turn();
        Animation_p();
        if (inSpace == false)
        {
            //Nad_Poverh();
        }
    }
    

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDeep * Time.deltaTime);
    }

    void Move()
    {
        Value_Position_X = 0;
        Value_Position_Y = 0;
        Value_Position_Z = _speed_Forward_Back;
        _rigidbody.AddRelativeForce(Value_Position_X, Value_Position_Y, Value_Position_Z);
    }

    void Animation_p()
    {
        _rigidbody.AddRelativeForce(0, Random.Range(-10, 10), 0);
    }

    private void Nad_Poverh()
    {
        if (transform.position.y < 0)
        {
            
            Value_Position_Z = movementSpeed;
            _rigidbody.AddForce(0, Value_Position_Y, 0);
        }
    }

    void ObHod()
    {
        if(Coliders_srabotaly[0]==1)
        {
            Value_Position_Z = -1 *  movementSpeed;
        }
        if(Coliders_srabotaly[1]==1)
        {
            Value_Position_Z = movementSpeed;
        }
        if(Coliders_srabotaly[2]==1)
        {
            Value_Position_Y = -1 * movementSpeed;
        }
        if(Coliders_srabotaly[3]==1)
        {
            Value_Position_Y = movementSpeed;
        }
        if(Coliders_srabotaly[4]==1)
        {
            Value_Position_X = -1 * movementSpeed;
        }
        if(Coliders_srabotaly[5]==1)
        {
            Value_Position_X =  movementSpeed;
        }
        if(Coliders_srabotaly[6]==1)
        {
            Value_Position_Y = -1 *  movementSpeed;
            Value_Position_Z = -1 *  movementSpeed/ 2;
            Prepytstvie_not_Move = true;
        }
        if(Coliders_srabotaly[7]==1)
        {
            Value_Position_Y = movementSpeed;
            Value_Position_Z = -1 *  movementSpeed/ 2;
            Prepytstvie_not_Move = true;
        }
        if(Coliders_srabotaly[8]==1)
        {
            Value_Position_X = -1 *  movementSpeed;
            Value_Position_Z = -1 *  movementSpeed/ 2;
            Prepytstvie_not_Move = true;
        }
        if(Coliders_srabotaly[9]==1)
        {
            Value_Position_X = movementSpeed;
            Value_Position_Z = -1 *  movementSpeed/ 2;
            Prepytstvie_not_Move = true;
        }
        _rigidbody.AddRelativeForce(Value_Position_X, Value_Position_Y, Value_Position_Z);
    }
}
