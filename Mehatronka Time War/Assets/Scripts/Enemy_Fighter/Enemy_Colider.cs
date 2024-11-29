using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Enemy_Colider : MonoBehaviour
{
    public int numer_col;
    [SerializeField] private Enemy_Flyter _SC_Move;
    private float _Time;

    private void FixedUpdate()
    {
        _Time += Time.deltaTime;
        if (_Time > 2)
        {
            _SC_Move.Coliders_srabotaly[numer_col] = 0;
            _SC_Move.prepydstvie = false;
            _Time = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.layer == 0 )
        {
            //Debug.Log(other.gameObject.name);
            _SC_Move.prepydstvie = true;
            _SC_Move.Coliders_srabotaly[numer_col] = 1;
        }
        
    }
}
