using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Dead_Zone : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Asteroids" || other.tag == "Enemy_Linkor" || other.tag == "Enemy_Fighter")
        {
            other.gameObject.SetActive(false);
        }
    }
}
