using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refocusirator_Dead_Zone : MonoBehaviour
{
    [SerializeField] private Health_Controller _SC_Dead;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Asteroids" || other.tag == "Enemy_Linkor" || other.tag == "Enemy_Fighter")
        {
            other.gameObject.SetActive(false);
        }
        

            
    }
}
