using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refocusirator_EMP : MonoBehaviour
{
    
    [SerializeField] private Health_Controller _SC_Dead;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy_Linkor" || other.tag == "Enemy_Fighter")
        {
            try
            {
                other.gameObject.GetComponent<VSX.UniversalVehicleCombat.VehicleEngines3D>().enabled = false;
                other.gameObject.GetComponent<Rigidbody>().angularDrag = 3;
                other.gameObject.GetComponent<Rigidbody>().drag = 3f;
            }
            catch{}
        }
        if(other.name == "AutoTurret_Enemy_ProjectileGun(Clone)")
        {
            try
            {
                other.gameObject.SetActive(false);
            }
            catch{}
        }
        if (other.tag == "Player")
        {
            _SC_Dead.maxHealth -= 100;
        }
    }
}
