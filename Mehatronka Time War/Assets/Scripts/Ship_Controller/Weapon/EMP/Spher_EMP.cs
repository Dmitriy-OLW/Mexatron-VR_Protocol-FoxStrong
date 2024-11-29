using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spher_EMP : MonoBehaviour
{
    public GameObject Audio_Weapon;
    void Start()
    {
        Audio_Weapon.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy_Linkor" || other.tag == "Enemy_Fighter")
        {
            Audio_Weapon.SetActive(true);
            try
            {
                other.gameObject.GetComponent<Rigidbody>().angularDrag = 3;
                other.gameObject.GetComponent<Rigidbody>().drag = 3f;
                if (other.tag == "Enemy_Fighter")
                {
                    
                    other.gameObject.GetComponent<VSX.UniversalVehicleCombat.VehicleEngines3D>().enabled = false;
                }
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
    }
}
